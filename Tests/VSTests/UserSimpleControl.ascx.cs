using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp
{
    public partial class UserSimpleControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                var intValue = 0;
                int.TryParse(label.Text, out intValue);
                label.Text = (intValue + 1).ToString();
            }
        }
    }
}