using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp
{
    public partial class SubstitutionServerNameControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            name.Text = string.Format("Machine: {0}, time: {1}, IpAddress:{2}, Port:{3}, AppPoolId: {4}", 
                Environment.MachineName, 
                DateTime.Now,
                this.Page.Request.UserHostAddress, 
                this.Page.Request.Url.Port, 
                this.Page.Request.ServerVariables["APP_POOL_ID"]);
        }
    }
}