<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutputCacheLogView.aspx.cs" Inherits="SitefinityWebApp.Tests.OutputCacheTests.OutputCacheLogView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Refresh" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Clear" />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
