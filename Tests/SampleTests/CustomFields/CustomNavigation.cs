using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.NavigationControls;

namespace SitefinityWebApp.SampleTests.CustomFields
{
    public class CustomNavigation : LightNavigationControl
    {
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            this.DataSource.NodeChecking += DataSource_NodeChecking;
            base.InitializeControls(container);
        }

        void DataSource_NodeChecking(object sender, NodeOperationEventArgs e)
        {
            var node = e.Node as PageSiteNode;
            var rating = node.GetCustomFieldValue("Rating") as decimal?;
            var ratingFilterString = SystemManager.CurrentHttpContext.Request.QueryString["rating"];
            if (rating != null && !ratingFilterString.IsNullOrEmpty())
            {
                var ratingFilter = int.Parse(ratingFilterString);
                if (rating < ratingFilter)
                    e.Cancel = true;
            }
        }
    }
}