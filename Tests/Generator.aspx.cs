using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Modules.News.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Web.Services;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestUI.Arrangements;

namespace SitefinityWebApp.Tests
{
    public partial class Generator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GenerateProviders_Click(object sender, EventArgs e)
        {
            var providersCount = int.Parse(this.ProvidersCount.Text);
            var configManager = ConfigManager.GetManager();
            var config = configManager.GetSection<NewsConfig>();
            var defaultProviderKey = config.DefaultProvider;

            var providersToRemove = config.Providers.Keys.Where(k => k != defaultProviderKey).ToArray();
            foreach (var provider in providersToRemove)
            {
                config.Providers.Remove(provider);
            }

            var defaultProvider = config.Providers[defaultProviderKey];

            for (int i = 0; i < providersCount; i++)
            {
                var name = "provider" + i;
                var parameters = new System.Collections.Specialized.NameValueCollection(defaultProvider.Parameters);
                parameters["applicationName"] = name;
                config.Providers.Add(
                    new DataProviderSettings(config.Providers)
                    {
                        Name = name,
                        ProviderTypeName = defaultProvider.ProviderTypeName,
                        Enabled = true,
                        Parameters = parameters
                    });
            }

            configManager.SaveSection(config);

            SystemManager.RestartApplication(string.Format("Generate {0}", providersCount));
        }

        protected void GenerateSites_Click(object sender, EventArgs e)
        {
            var sitesCount = MultisiteManager.GetManager().GetSites().Count();
            this.Context.Server.ScriptTimeout = 600;
            var service = new MultisiteService();

            var count = int.Parse(this.SitesCount.Text);

            for (int i = 0; i < count; i++)
            {
                var siteName = "site" + (sitesCount + i);
                var site = new SiteConfigurationViewModel();
                site.Name = siteName;
                site.LiveUrl = siteName + ".test.com";
                site.DataSources = service.GetNewSiteSourcesConfiguration(siteName).ToList();
                foreach (var ds in site.DataSources)
                {
                    ds.AllowMultipleProviders = true;
                    ds.IsChecked = true;
                    if (ds.Links.Count == 0)
                        ds.Links.Add(ds.SampleLink);
                }

                // site.SourcePagesSiteId = SystemManager.CurrentContext.CurrentSite.Id;
                service.SaveSite(Guid.Empty.ToString(), site);
                SystemManager.ClearCurrentTransactions();
            }
        }

        protected void ImportDefaultModules_Click(object sender, EventArgs e)
        {
            CustomDynamicModules.EnsureAllModulesImported();
        }
    }
}