using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.CustomTests
{
    public partial class ContentLocationsApiTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var clService = SystemManager.GetContentLocationService();


        }

        string GetNewsItemUrl(Guid itemId, string itemProvider = null)
        {
            var clService = SystemManager.GetContentLocationService();
            var location = clService.GetItemDefaultLocation(
                typeof(NewsItem),
                itemProvider,
                itemId);
            if (location != null)
                return location.ItemAbsoluteUrl;

            return string.Empty;
        }


        string GetNewsItemUrl(NewsItem item)
        {
            var clService = SystemManager.GetContentLocationService();
            var location = clService.GetItemDefaultLocation(item);
            if (location != null)
                return location.ItemAbsoluteUrl;

            return string.Empty;
        }

    }
}