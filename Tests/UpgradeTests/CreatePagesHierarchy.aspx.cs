using System;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.TestUtilities.Modules.Pages;

namespace SitefinityWebApp.UpgradeTests
{
    public partial class CreatePagesHierarchy : Page
    {
       
        private int PagesPerLevelCount
        {
            get
            {
                return int.Parse(this.pagesPerLevel.Text);
            }
        }
        
        private int PageLevelsCount
        {
            get
            {
                return int.Parse(this.pageLevels.Text);
            }
        }

        protected void OnCreatePagesClick(object sender, EventArgs e)
        {
            this.Server.ScriptTimeout = 3600;
            new PageCreator().
                CreatePageHierarchy(new PageCreator.HierarchicalPage(SiteInitializer.CurrentFrontendRootNodeId), 
                this.PagesPerLevelCount, this.PageLevelsCount);
        }
    }
}