using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace SitefinityWebApp.JsTests.tests
{
    public partial class TestBase : System.Web.UI.MasterPage
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.ScriptManager1.Scripts.Insert(0, new ScriptReference("Telerik.Sitefinity.Resources.Scripts.MicrosoftAjax.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.FullName));
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
