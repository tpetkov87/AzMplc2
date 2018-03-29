<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Generator.aspx.cs" Inherits="SitefinityWebApp.Tests.Generator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ol>
            <li>Generate <asp:TextBox ID="ProvidersCount" runat="server"></asp:TextBox> providers for news.<asp:Button runat="server" ID="GenerateProviders" Text="Generate providers" OnClick="GenerateProviders_Click" /></li>
            <li>Generate <asp:TextBox ID="SitesCount" runat="server"></asp:TextBox> sites.<asp:Button runat="server" ID="GenerateSites" Text="Generate sites" OnClick="GenerateSites_Click" /></li>
            <li><asp:Button runat="server" ID="ImportDefaultModules" Text="Import default modules" OnClick="ImportDefaultModules_Click" /></li>
        </ol>    
    </div>
    </form>
</body>
</html>
