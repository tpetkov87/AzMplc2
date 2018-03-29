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
using Telerik.Sitefinity.Data;

namespace SitefinityWebApp.CustomTests
{
    public partial class Create100MultisiteTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void CreateNewSites_Click(object sender, EventArgs e)
        {
            string name = "testSite";

            var transaction = Guid.NewGuid().ToString();
            var manager = MultisiteManager.GetManager(null, transaction);
            var pageManager = PageManager.GetManager(null, transaction);
            

            Site site = null;
            for (int i = 0; i < 100; i++)
            {
                var frontEndPageId = Guid.NewGuid();
                site = manager.CreateSite();
                site.Name = name + i.ToString();
                site.StagingUrl = name + i.ToString() + ".test.com";
                site.DomainAliases.Add(name + i.ToString() + ".test.com");
                site.SiteMapRootNodeId = frontEndPageId;
                site.LiveUrl = i.ToString() + "test.com";

                this.CreateFrontEndRoot(frontEndPageId, pageManager);

                if (i % 20 == 0)
                    TransactionManager.FlushTransaction(manager.TransactionName);
            }
            TransactionManager.CommitTransaction(manager.TransactionName);
            //manager.SaveChanges();
            
        }

        protected void DeleteSites_Click(object sender, EventArgs e)
        {
            string name = "testSite";
            for (int i = 0; i < 100; i++)
            {
                this.DeleteSite(name + i.ToString());
            }
        }

        public void DeleteSite(string siteName)
        {
            var msm = MultisiteManager.GetManager();
            var site = msm.GetSites().Where(s => s.Name == siteName).SingleOrDefault();
            if (site == null)
            {
                return;
            }
            var manager = PageManager.GetManager();
            manager.GetPageNode(site.SiteMapRootNodeId);
            manager.SaveChanges();

            msm.Delete(site);
            msm.SaveChanges();
        }

        private void CreateFrontEndRoot(Guid id, PageManager manager)
        {
            var root = manager.CreatePageNode(id);
           // manager.SaveChanges();
        }
    }
}