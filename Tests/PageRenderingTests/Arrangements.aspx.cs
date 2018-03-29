using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;

namespace SitefinityWebApp.Tests.PageRenderingTests
{
    public partial class Arrangements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var action = this.Request.QueryString["action"];
                if (!string.IsNullOrEmpty(action))
                {
                    SystemManager.RunWithElevatedPrivilege(p => ExecuteAction(new object[] { action, this.Request.QueryString["indexes"] }));
                }
            }
        }

        void ExecuteAction(object[] parameters)
        {
            var action = (string)parameters[0];
            var indexes = (string)parameters[1];
            var prefix = "page";
            switch (action)
            {
                case "create":
                    this.CreatePages(prefix, this.GetIndexes(indexes));
                    break;
                case "update":
                    this.UpdatePages(prefix, this.GetIndexes(indexes));
                    break;
                case "delete":
                    this.DeletePages(prefix, this.GetIndexes(indexes));
                    break;
                default:
                    throw new InvalidOperationException("The action '{0}' is not valid!".Arrange(action));
            }
        }

        private int[] GetIndexes(string indexesString)
        {
            if (string.IsNullOrEmpty(indexesString))
                indexesString = "1-10";

            if (indexesString.Contains(","))
                return indexesString.Split(',').Select(p => int.Parse(p)).ToArray();

            if (indexesString.Contains("-"))
            {
                var list = new List<int>();
                var rangePair = indexesString.Split('-').Select(p => int.Parse(p)).ToArray();
                var from = Math.Min(rangePair[0], rangePair[1]);
                var to = Math.Max(rangePair[0], rangePair[1]);
                do
                {
                    list.Add(from++);
                }
                while (from <= to);
                return list.ToArray();
            }

            return new int[] { int.Parse(indexesString) };
        }

        private void DeletePages(string prefix, int[] indexes)
        {
            using (var fluent = App.WorkWith())
            {
                var urls = indexes.Select(index => string.Concat(prefix, index)).ToArray();
                var ids = urls.Select(url => ((PageSiteNode)SiteMapBase.GetCurrentProvider().FindSiteMapNode(url)).Id).ToArray();

                foreach (var id in ids)
                {
                    fluent.Page(id).Delete().SaveChanges();
                }
            }
        }

        private void UpdatePages(string prefix, int[] indexes)
        {
            var placeHolder = "Body";
            using (var fluent = App.WorkWith())
            {
                var urls = indexes.Select(index => string.Concat(prefix, index)).ToArray();
                var ids = urls.Select(url => ((PageSiteNode)SiteMapBase.GetCurrentProvider().FindSiteMapNode(url)).Id).ToArray();

                foreach (var id in ids)
                {
                    var pageFacade = fluent.Page(id);
                    pageFacade
                        .AsStandardPage()
                        .CheckOut()
                        .Do(
                            p =>
                            {
                                var contentblock = new Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock();
                                contentblock.Html = DateTime.Now.ToLongDateTimeString();
                                var draftControl = pageFacade.PageManager.CreateControl<PageDraftControl>(contentblock, placeHolder);
                                draftControl.SiblingId = GetLastControlInPlaceHolderInPageId(p, placeHolder);
                                draftControl.Caption = "Content block";
                                pageFacade.PageManager.SetControlDefaultPermissions(draftControl);
                                p.Controls.Add(draftControl);
                            }
                        )
                        .CheckIn()
                        .Publish()
                        .Done()
                        .SaveChanges();
                }
            }
        }

        private Guid GetLastControlInPlaceHolderInPageId(PageDraft pageDraft, string placeHolder)
        {
            var id = Guid.Empty;
            PageDraftControl control;

            var controls = new List<PageDraftControl>(pageDraft.Controls.Where(c => c.PlaceHolder == placeHolder));

            while (controls.Count > 0)
            {
                control = controls.Where(c => c.SiblingId == id).SingleOrDefault();
                id = control.Id;

                controls.Remove(control);
            }

            return id;
        }


        private void CreatePages(string prefix, int[] indexes)
        {
            Guid parentId = SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId;
            Guid defaultTemplateId = Config.Get<PagesConfig>().DefaultFrontendTemplateId;
            var theme = Config.Get<AppearanceConfig>().DefaultFrontendTheme;

            using (var fluent = App.WorkWith())
            {
                var pageFacade = fluent.Page();
                foreach (var index in indexes)
                {
                    pageFacade
                        .CreateNewStandardPage(parentId, Guid.Empty)
                            .Do(p =>
                            {
                                var pageName = string.Concat(prefix, index);
                                p.UrlName = pageName;
                                p.Title = pageName;
                            })
                            .CheckOut()
                                .SetTemplateTo(defaultTemplateId)
                                .SetTheme(theme)
                                .CheckIn()
                            .Publish()
                            .Done();
                }
                pageFacade.SaveChanges();
            }
        }
    }
}