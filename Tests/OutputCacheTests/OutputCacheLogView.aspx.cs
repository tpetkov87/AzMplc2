using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.Tests.OutputCacheTests
{
    /*
     * Add the following code in the global asax to start logging
     * 
        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Bootstrapped += Bootstrapper_Bootstrapped;
        }

        private void Bootstrapper_Bootstrapped(object sender, EventArgs e)
        {
            EventHub.Subscribe<IContextOperationEndEvent>(this.OnOperationEnd);

        }

        private void OnOperationEnd(IContextOperationEndEvent @event)
        {
            OutputCacheLogger.HandleOperationEndEvent(@event);
        }
      */


    public partial class OutputCacheLogView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(string.Format("Machine: {0}", Environment.MachineName));

            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            OutputCacheLogger.Clear();
            this.BindGrid();
        }

        private void BindGrid()
        {
            this.GridView1.DataSource = OutputCacheLogger.GetLog().GroupBy(e => new { e.Domain, e.Url, e.Headers, e.HttpStatus, e.ServerCache })
                .Select(g => new PageLogEntryAggregate() {
                    Domain = g.Key.Domain,
                    Url = g.Key.Url,
                    Headers = g.Key.Headers,
                    HttpStatus = g.Key.HttpStatus,
                    ServerCache = g.Key.ServerCache,
                    Count = g.Count(),
                    LastDate = g.Max(e => e.Date)
                })
                .OrderByDescending(e => e.LastDate).Take(50);
            this.GridView1.DataBind();
        }
    }
}