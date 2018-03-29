using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.VSTests
{
    public partial class UserAddControls : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.PreRenderComplete += new EventHandler(Page_PreRenderComplete);
        }

        void Page_PreRenderComplete(object sender, EventArgs e)
        {
            Label l = new Label();
            l.Text = "User label";
            Page.Controls.Add(l);
        }
    }
}