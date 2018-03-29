<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForumsTestPage.aspx.cs" Inherits="SitefinityWebApp.CustomTests.ForumsTestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="uxTestValidation" runat="server" onclick="uxTestValidation_Click" Text="Test Validation" />

        <asp:Button ID="uxTestPermissions" runat="server" onclick="uxTestPermissions_Click" Text="Test permissions" />

        <asp:Button ID="uxTestPageModules" runat="server" onclick="uxTestPageModules_Click" Text="Test page modules" />
    </div>
    </form>
</body>
</html>
