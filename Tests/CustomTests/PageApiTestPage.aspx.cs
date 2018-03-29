using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.CustomTests
{
    public partial class PageApiTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Guid parentId = SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId;
            using (var fluent = App.WorkWith())
            {
                Guid pageId;
                var pageFacade = fluent.Page();
                pageFacade
                    .CreateNewPageGroup()
                        .Do(p =>
                            {
                                parentId = p.Id;
                                p.Title = GetUniqueTitleFromId("Group Page", parentId);
                            })
                        .Done()
                    .CreateNewStandardPage(parentId, Guid.Empty)
                        .Do(p =>
                            {
                                pageId = p.Id;
                                p.Title = GetUniqueTitleFromId("Standard Page", pageId);
                            })
                        .Publish()
                        .Done()
                    .SaveChanges();
            }
        }

        private string GetUniqueTitleFromId(string name, Guid id)
        { 
            return String.Concat(name, " ",  id.GetHashCode().ToString());
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var rootId = SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId;
            var manager = PageManager.GetManager();
            var nodesQuery = manager.GetPageNodes();
            nodesQuery = nodesQuery.Where(p => p.RootNodeId == rootId);
            //nodesQuery = nodesQuery.Where(p => p.Page.IncludeScriptManager );
            //var items = nodesQuery.ToList();

            var culture = CultureInfo.GetCultureInfo("bg").Name;

            var pageDataQuery = manager.GetPageDataList();

            var query = from pn in nodesQuery
                        join pd in pageDataQuery on pn equals pd.NavigationNode into p
                        from pages in p.DefaultIfEmpty()
                        where (pages == null || ((pages.Culture == string.Empty || pages.Culture == culture) && pages.PublishedTranslations.Contains(culture)))
                        select pn;
            var items = query.ToList();
            Response.Write(items.Count());
        }
    }
}