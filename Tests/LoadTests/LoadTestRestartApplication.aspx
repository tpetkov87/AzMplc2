<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System"%>
<%@ Import Namespace="System.Linq"%>
<%@ Import Namespace="Telerik.Sitefinity.Data.OA"%>
<%@ Import Namespace="Telerik.Sitefinity.DynamicModules"%>
<%@ Import Namespace="Telerik.Sitefinity.DynamicModules.Builder"%>
<%@ Import Namespace="Telerik.Sitefinity.Modules.Events"%>
<%@ Import Namespace="Telerik.Sitefinity.Modules.Forms"%>
<%@ Import Namespace="Telerik.Sitefinity.Modules.GenericContent"%>
<%@ Import Namespace="Telerik.Sitefinity.Modules.Libraries"%>
<%@ Import Namespace="Telerik.Sitefinity.Modules.News"%>
<%@ Import Namespace="Telerik.Sitefinity.Multisite"%>
<%@ Import Namespace="Telerik.Sitefinity.Publishing"%>
<%@ Import Namespace="Telerik.Sitefinity.Security"%>
<%@ Import Namespace="Telerik.Sitefinity.Services"%>
<%@ Import Namespace="Telerik.Sitefinity.Taxonomies"%>
<%@ Import Namespace="Telerik.Sitefinity.Taxonomies.Model"%>
<%@ Import Namespace="Telerik.Sitefinity.Web"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<script runat="server" language="C#">
    /// <summary>
    /// Page for reset restart load test
    /// </summary>
    /// <param name="sender">the sender</param>
    /// <param name="e">the parameter</param>
    public void Page_Load(object sender, EventArgs e)
    {
        this.Server.ScriptTimeout = 600;
        var operation = Request.Params["operation"];
        switch (operation)
        {
            case "AppRestart":
                SystemManager.RestartApplication(false);
                break;
            case "ResetModel":
                OpenAccessConnection.ResetModel();
                break;
            case "AppRestartAndCleanModel":
                OpenAccessConnection.CleanAll();
                SystemManager.RestartApplication(false);
                break;
            case "FullAppRestart":
                SystemManager.RestartApplication(true);
                string redirectUrl = this.Request.Url.AbsoluteUri.Split('?')[0];
                this.Page.Response.Redirect(redirectUrl, true);
                return;
            default:
                // do nothing
                break;
        }

        SystemManager.ClearCurrentTransactions();
        var nodes = SitefinitySiteMap.GetCurrentProvider().GetChildNodes(SitefinitySiteMap.GetCurrentProvider().RootNode);
        Telerik.Sitefinity.Security.UserManager.GetManager().GetUsers();
        RoleManager.GetManager().GetRoles();
        SecurityManager.GetManager().GetPermissions();
        ModuleBuilderManager.GetManager().Provider.GetDynamicModules();
        DynamicModuleManager.GetManager();
        TaxonomyManager.GetManager().GetTaxonomies<FlatTaxonomy>();
        NewsManager.GetManager().GetNewsItems();
        LibrariesManager.GetManager().GetAlbums();
        EventsManager.GetManager().GetEvents();
        ContentManager.GetManager().GetContent();
        MultisiteManager.GetManager().GetSites();
        FormsManager.GetManager().GetForms();
        PublishingManager.GetManager().GetPublishingPoints();
    }
</script>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
