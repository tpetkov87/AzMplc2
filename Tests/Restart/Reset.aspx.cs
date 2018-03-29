using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp
{
    public partial class Reset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RestartButton_Click(object sender, EventArgs e)
        {
            var restartFlags = SystemRestartFlags.Default;
            if (fullRestart.Checked)
                restartFlags = SystemRestartFlags.AttemptFullRestart;

            SystemManager.RestartApplication(OperationReason.KnownKeys.UnknownReason, restartFlags);
        }
    }
}