using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Data;

namespace SitefinityWebApp.CustomTests
{
    public partial class FetchStrategiesTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = NewsManager.GetManager();
            var newsItems = manager.GetNewsItems().ToList();
            var provider = manager.Provider as IOpenAccessDataProvider;
            var context = provider.GetContext();
            var author = newsItems[0].Author;
            var summary = newsItems[0].Summary;
            var url = newsItems[0].ItemDefaultUrl;
        }
    }
}