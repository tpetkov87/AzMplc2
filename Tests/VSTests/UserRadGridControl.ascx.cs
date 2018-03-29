using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Web.UI;

namespace SitefinityWebApp.VSTests
{
    [RequireScriptManager]
    public partial class UserRadGridControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("column1");
            dt.Columns.Add("column2");
            dt.Columns.Add("column3");
            for (int i = 0; i < 30; i++)
            {
                DataRow dr = dt.NewRow();
                dr["column1"] = "c1r" + i;
                dr["column2"] = "c2r" + i;
                dr["column3"] = "c3r" + i;
                dt.Rows.Add(dr);
            }
            RadGrid1.DataSource = dt;

            RadGrid1.PageSizeChanged += new GridPageSizeChangedEventHandler(RadGrid1_PageSizeChanged);
            RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);

            label.Visible = false;
        }

        void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            return;
        }

        void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            label.Visible = true;
            label.Text = "Page size changed!";
        }

        protected void RadGrid1_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
        {
            label.Visible = true;
            label.Text = "Item updated!";
        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            label.Visible = true;
            label.Text = "Item inserted!";
        }

        protected void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
        {
            label.Visible = true;
            label.Text = "Item deleted!";
        }

        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {
            label.Visible = true;
            label.Text = "Data bound!";
        }
    }
}