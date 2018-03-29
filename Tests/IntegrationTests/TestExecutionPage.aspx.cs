using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Tests.IntegrationTests
{
    public partial class TestExecutionPage : System.Web.UI.Page
    {
        #region Tests

        [Test]
        private void ControlPropertiesTestMonolingual()
        {


        }

        #endregion

        #region Test UI

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.grid.RowCommand += grid_RowCommand;
            if (!this.IsPostBack)
                this.BindGrid();
        }

        void BindGrid()
        {
            var tests = this.GetTests();
            var testModels = tests.Select(x => new TestMethod(x.Name));
            this.grid.DataSource = testModels;
            this.grid.DataBind();
        }

        IEnumerable<MethodInfo> GetTests()
        {
            return typeof(TestExecutionPage).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => x.GetCustomAttributes(false).OfType<TestAttribute>().Any());
        }

        void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var test = this.GetTests().FirstOrDefault(x => x.Name == e.CommandArgument.ToString());
            var row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            if (test != null)
            {
                try
                {
                    test.Invoke(this, null);
                    row.BackColor = Color.Green;
                }
                catch
                {
                    row.BackColor = Color.Red;
                }
            }
        }

        #endregion
    }

    #region TestHelpers

    public class TestMethod
    {
        public TestMethod(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }



    [AttributeUsage(AttributeTargets.Method)]
    public class TestAttribute : Attribute
    {

    }

    #endregion

}