<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourcesAscxMirrorer.aspx.cs" Inherits="SitefinityWebApp.TemplateTests.AscxMirrorer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ResourcesAscxMirrorer.aspx</title>
    <style type="text/css">
        .small
        {
            font-size: small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Copy all ascx-s included in Telerik.Sitefinity.Resources project to a destination directory. Renaming the files in the destination to contain their path separated with dots.<br/>
        <span class="small">Example: File Telerik.Sitefinity.Resources/Templates/Backend/ContentUI/FormsDetailView.ascx to Templates.Backend.ContentUI.FormsDetailView.ascx</span><br />
        <br />
        <b>Resources physical path:</b>&nbsp;<asp:TextBox ID="ResourcesPath" runat="server" Width="700px" />&nbsp;<i>Must contain "Telerik.Sitefinity.Resources.csproj" file.</i><br />
        <b>Target physical path:</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TargetPath" runat="server" Width="700px"/>&nbsp;<i>If the direcotry does not exist is will be created.</i><br />
        <asp:Button runat="server" ID="MirrorButton" Text="Mirror" onclick="MirrorButton_Click"/><br />
        <i>Remember to remove ReadOnly properties from all copied files in case you copy them from a Source control.</i><br />
        <br /><b>Output:</b><br />
        <asp:PlaceHolder ID="ResultPlaceHolder" runat="server" />
    </div>
    </form>
</body>
</html>
