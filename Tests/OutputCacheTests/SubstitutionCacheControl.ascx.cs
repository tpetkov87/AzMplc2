using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace SitefinityWebApp.Tests.OutputCacheTests
{
    public partial class SubstitutionCacheControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.time.Text = DateTime.Now.ToLongTimeString();
            this.site.Text = SystemManager.CurrentContext.CurrentSite.Name;
            var pageNode = SitefinitySiteMap.GetCurrentNode();
            if (pageNode != null)
            {
                var cacheProfileName = pageNode.OutputCacheProfile;
                var config = Config.Get<SystemConfig>();
                if (cacheProfileName.IsNullOrEmpty())
                    cacheProfileName = config.CacheSettings.DefaultProfile;

                var cacheProfile = config.CacheSettings.Profiles[cacheProfileName];
                var strBuilder = new StringBuilder();
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(typeof(IOutputCacheProfile)))
                {
                    strBuilder.AppendFormat("{0} = {1}; ", prop.Name, prop.GetValue(cacheProfile));
                }
                this.CacheProfile.Text = strBuilder.ToString();
            }
        }

        public static string GetInformation(HttpContext context)
        {
            return string.Format("Time: {0}",
            DateTime.Now.ToLongTimeString());
        }

    }
}