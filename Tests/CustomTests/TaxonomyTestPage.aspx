<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaxonomyTestPage.aspx.cs" Inherits="SitefinityWebApp.CustomTests.TaxonomyTestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Tags for News" OnClick="Button1_Click" /> <asp:Label runat="server" ID="tagsLabel"></asp:Label>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Fix statistics" OnClick="Button2_Click" />
    </div>
    </form>
</body>
</html>
