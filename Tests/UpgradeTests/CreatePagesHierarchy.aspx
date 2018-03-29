<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePagesHierarchy.aspx.cs" Inherits="SitefinityWebApp.UpgradeTests.CreatePagesHierarchy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" AssociatedControlID="pagesPerLevel" Text="Pages per level" />
        <asp:TextBox ID="pagesPerLevel" runat="server" Width="50px" />
        <asp:Label runat="server" AssociatedControlID="pageLevels" Text="Page Levels" />
        <asp:TextBox ID="pageLevels" runat="server" Width="50px" />
        <asp:Button runat="server" ID="createPages" OnClick="OnCreatePagesClick"  Text="Go"/>
    </div>
    </form>
</body>
</html>
