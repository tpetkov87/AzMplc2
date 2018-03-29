using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Workflow;

namespace SitefinityWebApp.Tests.OpenAccessTests
{
    public partial class MetadataAggregationPerformance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.DropDownListTestMethods.DataSource = Enum.GetNames(typeof(TestMethod));
                this.DropDownListTestMethods.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var numberOfTests = int.Parse(this.NumberOfTestsText.Text);

            SystemManager.ClearCurrentTransactions();

            TestMethod testMethod = (TestMethod)Enum.Parse(typeof(TestMethod), this.DropDownListTestMethods.SelectedValue);

            var metrics = new List<Metric>();
            for (var i = 0; i < numberOfTests; i++)
            {
                var startTime = DateTime.UtcNow;
                switch (testMethod)
                { 
                    case TestMethod.RestartApplication :
                        SystemManager.RestartApplication("Test");
                        break;
                    case TestMethod.RestartAppWithResetModel:
                        SystemManager.RestartApplication("Test", SystemRestartFlags.ResetModel);
                        break;
                    case TestMethod.ResetModelSkipSfAggregation:
                        var reason = OperationReason.FromKey("Test");
                        reason.AddInfo("SkipMappingAggregation");
                        OpenAccessConnection.ResetModel(reason);
                        break;
                    default :
                        OpenAccessConnection.ResetModel(OperationReason.FromKey("Test"));
                        break;
                }
                ForceMetadataSourceAggregation();
                var endTime = DateTime.UtcNow;
                metrics.Add(new Metric() { Interval = (endTime - startTime) });
            }

            TimeSpan max = metrics.Max(m => m.Interval);
            TimeSpan min = metrics.Min(m => m.Interval);
            long sum = 0;
            foreach (var metric in metrics)
            {
                sum += metric.Interval.Ticks;
            }
            TimeSpan avg = new TimeSpan(sum/(metrics.Count));

            LabelMax.Text = max.ToString();
            LabelMin.Text = min.ToString();
            LabelAvg.Text = avg.ToString();

            var method = typeof(OpenAccessConnection).GetMethod("GetConnections", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var connection = ((IEnumerable<OpenAccessConnection>)method.Invoke(null, null)).First();
            var field = typeof(OpenAccessConnection).GetField("registeredModules", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            LabelRegModules.Text = ((HashSet<string>)field.GetValue(connection)).Count.ToString();
        }

        private void ForceMetadataSourceAggregation()
        {
            ISet<Type> managerTypes = new HashSet<Type>();
            var modules = SystemManager.ApplicationModules.Values;
            foreach (var module in modules)
            {
                var managerCollection = module.Managers;
                if (managerCollection != null)
                    foreach (var type in managerCollection)
                        managerTypes.Add(type);
            }

            managerTypes.Add(typeof(SecurityManager));
            managerTypes.Add(typeof(UserProfileManager));
            managerTypes.Add(typeof(RoleManager));
            managerTypes.Add(typeof(UserManager));
            managerTypes.Add(typeof(WorkflowManager));
            managerTypes.Add(typeof(PageManager));
            managerTypes.Add(typeof(ContentLinksManager));
            managerTypes.Add(typeof(TaxonomyManager));

            foreach (var managerType in managerTypes)
            {
                if (managerType.GetConstructor(new Type[] { }) != null)
                {
                    if (typeof(IManager).IsAssignableFrom(managerType))
                    {
                        try 
                        {
                            var manager = ManagerBase.GetManager(managerType);
                            foreach (var provider in manager.Providers)
                            {
                                if (provider is IOpenAccessMetadataProvider)
                                {
                                    using (var context = OpenAccessConnection.GetContext((IOpenAccessMetadataProvider)provider, "Sitefinity"))
                                    { }
                                }
                            }

                        }
                        catch (Exception) { }
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var culturesToAdd = int.Parse(NumCulturesText.Text);
            var resConfig = Config.Get<ResourcesConfig>();
            if (resConfig.Cultures.Count < culturesToAdd)
            {
                var configManager = ConfigManager.GetManager();
                resConfig = configManager.GetSection<ResourcesConfig>();
                var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

                foreach (var culture in allCultures)
                {
                    var key = CulturesConfig.GenerateCultureKey(culture, culture);
                    if (resConfig.Cultures.ContainsKey(key))
                        continue;

                    var cultureElement = new CultureElement(resConfig.Cultures)
                    {
                        Key = key,
                        Culture = culture.Name,
                        UICulture = culture.Name
                    };
                    if (culture.Name == "id")
                        cultureElement.FieldSuffix = "iddd";
                    resConfig.Cultures.Add(cultureElement);
                    
                    if (resConfig.Cultures.Count >= culturesToAdd)
                        break;
                }
                configManager.SaveSection(resConfig, true);
                SystemManager.RestartApplication(true);
                string redirectUrl = this.Request.Url.AbsoluteUri;
                this.Page.Response.Redirect(redirectUrl, true);

            }
        }

        class Metric
        {
            public TimeSpan Interval { get; set; }
        }

        private enum TestMethod
        { 
            ResetModel,
            RestartApplication,
            RestartAppWithResetModel,
            ResetModelSkipSfAggregation
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
        }
    }

    public abstract class DummyDynamicMetadataSourceBase : SitefinityMetadataSourceBase
    {
        public DummyDynamicMetadataSourceBase(string typePrefix)
            : base(null)
        {
            this.typePrefix = typePrefix;
        }

        protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
        {
            var sitefinityMappings = new List<IOpenAccessFluentMapping>();
            sitefinityMappings.Add(new Telerik.Sitefinity.Security.Model.PermissionsFluentMapping(this.Context) { });
            sitefinityMappings.Add(new CommonFluentMapping(this.Context));
            sitefinityMappings.Add(new DynamicModuleFluentMapping(this.Context));
            return sitefinityMappings;
        }

        protected override IList<MappingConfiguration> PrepareMapping()
        {
            var mappings = new List<MappingConfiguration>();
            foreach (var m in this.CustomMappings)
            {
                var configs = m.GetMapping();
                if (configs != null)
                {
                    foreach (var config in configs)
                    {
                        mappings.Add(config);
                        Type type = config.ConfiguredType;
                        if (type is DynamicContent)
                        {

                        }
                    }
                }
            }

            return mappings;
        }

        private string typePrefix;
        private int numberOfTypes = 20;
        private int numberOfFields = 40;
    }
}