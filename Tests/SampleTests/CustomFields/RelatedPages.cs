using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity;
using System.ComponentModel;
using Telerik.Sitefinity.Web.UI.NavigationControls;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Abstractions;

namespace SitefinityWebApp.SampleTests.CustomFields
{
    public class RelatedPages : SimpleView
    {

        #region Properties

        public override string LayoutTemplatePath
        {
            get
            {
                return "~/SampleTests/CustomFields/RelatedPages.ascx";
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        protected virtual LightNavigationControl NavigationWidget
        {
            get
            {
                return this.Container.GetControl<LightNavigationControl>("navigationWidget", true);
            }
        }

        public string FieldName
        {
            get
            {
                return this.fieldName;
            }
            set
            {
                this.fieldName = value;
            }
        }

        #endregion

        protected override void InitializeControls(GenericContainer container)
        {
            var currentNode = SiteMapBase.GetCurrentNode();
            List<string> selectedPageIds = new List<string>();
            if (currentNode != null)
            {
                var tagIds = (currentNode.GetCustomFieldValue(fieldName) as IList<Guid>);

                if (tagIds != null && tagIds.Count > 0)
                {
                    var pageManager = PageManager.GetManager();
                    selectedPageIds = pageManager.GetPageNodes()
                        .Where(x => x.RootNodeId == SiteInitializer.CurrentFrontendRootNodeId && x.Id != currentNode.Id)
                        .Where(x => (x.GetValue<TrackedList<Guid>>(fieldName)).Any(y => tagIds.Contains(y)))
                        .Select(x => x.Id.ToString())
                        .ToList();
                }

                if (selectedPageIds.Count > 0)
                {
                    this.NavigationWidget.SelectedPageIds = selectedPageIds.ToArray();
                    this.NavigationWidget.SelectionMode = PageSelectionModes.SelectedPages;
                }
            }
            if (selectedPageIds.Count < 1)
            {
                this.NavigationWidget.Visible = false;
            }
        }

        private string fieldName;
    }
}