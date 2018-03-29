<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigTestPage.aspx.cs" Inherits="SitefinityWebApp.ConfigTestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" Text="Get Config" OnClick="GetConfig_Click" />
        <asp:Button runat="server" Text="Save Config" OnClick="SaveConfig_Click" />
        <asp:Button runat="server" Text="Remove/Add" OnClick="RemoveAdd_Click" />

        <div>
            <asp:PlaceHolder ID="ph" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
