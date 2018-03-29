using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.OpenAccess;

namespace SitefinityWebApp.Tests.OpenAccessTests
{
    public partial class FetchStrategyTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void TestBtn_Click(object sender, EventArgs e)
        {
            var pageManager = PageManager.GetManager();

            Guid parentId = SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId;

            var pageNodes = (from p in pageManager.GetPageNodes()
                                                 .Include<PageNode>(p => p.Permissions)
                                                 .Include<PageNode>(p => p.Attributes)
                                                 .Include<PageNode>(p => p.Urls)
                             where p.ParentId == parentId
                             orderby p.Ordinal
                             select p);
            var result = pageNodes.ToList();
        }
    }
}