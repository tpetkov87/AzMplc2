using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity;

namespace SitefinityWebApp.CustomTests
{
    public partial class PublicationDateTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var fluent =App.WorkWith().Pages(); 
            var pag2e = fluent.Where(page => page.Title.Contains("zzz")).ThatArePublished().Get().First();
            pag2e.GetPageData().Controls.Clear();
            fluent.SaveChanges();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var pubDate = new DateTime(1978, 4, 15);

            var manager = NewsManager.GetManager();

            var item = manager.CreateNewsItem();
            item.Title = "Test news";
            item.UrlName = "test-news";
            item.Content = "This item is created through API";
            item.ApprovalWorkflowState = "Published";
            manager.RecompileItemUrls(item);

            manager.Lifecycle.PublishWithSpecificDate(item, pubDate);
            manager.SaveChanges();
        }
    }
}