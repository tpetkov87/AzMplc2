using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Abstractions;

namespace SitefinityWebApp.CustomTests
{
    public partial class MultisiteTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateNewSite_Click(object sender, EventArgs e)
        {
            var siteInitializer = SiteInitializer.GetInitializer();
            var siteMapRootNode = siteInitializer.CreateSiteRoot("site2", "Site 2");
            siteMapRootNode.Description = "Test site 2";

            var manager = MultisiteManager.GetManager(null, siteInitializer.TransactionName);
            var site = manager.CreateSite();
            site.Name = "site2";
            site.StagingUrl = "site2.test.com:4444";
            site.DomainAliases.Add("site222.test.com:4444");
            site.SiteMapRootNodeId = siteMapRootNode.Id;

            siteInitializer.SaveChanges();
            
        }
    }
}