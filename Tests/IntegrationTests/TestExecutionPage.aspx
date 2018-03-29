<%@ Page Language="C#" CodeBehind="TestExecutionPage.aspx.cs" Inherits="SitefinityWebApp.Tests.IntegrationTests.TestExecutionPage" AutoEventWireup="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView runat="server" ID="grid" AutoGenerateColumns="false"> 
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:TemplateField> 
                    <ItemTemplate>
                        <asp:Button ID="execute" runat="server" Text="Execute" CommandName="Execute" CommandArgument="<%# ((SitefinityWebApp.Tests.IntegrationTests.TestMethod)Container.DataItem).Name %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </form>
</body>
</html>
 