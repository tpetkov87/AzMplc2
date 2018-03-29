using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Tests.OutputCacheTests
{
    public partial class TestOutputCache : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.time.Text = DateTime.Now.ToLongTimeString();
            this.site.Text = SystemManager.CurrentContext.CurrentSite.Name;
        }
    }
}