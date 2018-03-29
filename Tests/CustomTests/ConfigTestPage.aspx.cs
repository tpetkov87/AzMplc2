using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Modules.News.Configuration;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Scheduling;

namespace SitefinityWebApp
{
    public partial class ConfigTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
                Response.Redirect("~/Sitefinity/Login?ReturnUrl=" + Request.Url.AbsolutePath);

            SchedulingManager.RescheduleNextRun();
        }

        protected void GetConfig_Click(object sender, EventArgs e)
        {
            var config = Config.Get<ProjectConfig>();
            ph.Controls.Add(new LiteralControl(config.ProjectName));
        }

        protected void SaveConfig_Click(object sender, EventArgs e)
        {
            var manager = MetadataManager.GetManager();
            manager.SaveChanges();
        }

        protected void RemoveAdd_Click(object sender, EventArgs e)
        {
            var newsConfig = Config.Get<NewsConfig>();

            var backend = newsConfig.ContentViewControls["NewsBackend"];
            var view = backend.ViewsConfig["NewsBackendEdit"] as ContentViewDetailElement;
            var fields = view.Sections["TaxonSection"].Fields;

            FieldDefinitionElement tags = null;
            if (fields.ContainsKey("Tags"))
            {
                tags = fields["Tags"];
                fields.Remove("Tags");
                fields.Add("Tags", tags);
            }

            ConfigManager.GetManager().SaveSection(newsConfig);
        }
    }
}