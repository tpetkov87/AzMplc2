<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentGeneratorTestPage.aspx.cs" Inherits="SitefinityWebApp.Tools.ContentGeneratorTestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Button ID="Create" runat="server" Text="Create" OnClick="Create_Click" />
        <p>
            <asp:Button ID="Delete" runat="server" Text="Delete" OnClick="Delete_Click" />
        </p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
