using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace SitefinityWebApp.Tests.OutputCacheTests
{
    public partial class Warmup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Context.Server.ScriptTimeout = 600;
        }

        protected void SetupBtn_Click(object sender, EventArgs e)
        {
        }

        protected void WarmupBtn_Click(object sender, EventArgs e)
        {
            Warm.All();
        }

        protected void WarmupByUrlBtn_Click(object sender, EventArgs e)
        {
            var urlList = urls.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Warm.Selection(urlList);
        }
    }
}