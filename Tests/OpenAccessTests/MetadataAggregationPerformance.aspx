<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MetadataAggregationPerformance.aspx.cs" Inherits="SitefinityWebApp.Tests.OpenAccessTests.MetadataAggregationPerformance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button3" runat="server" Text="Aggregation Test" OnClick="Button3_Click" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Setup" OnClick="Button2_Click" />&nbsp;Number of cultures<asp:TextBox ID="NumCulturesText" runat="server"></asp:TextBox><br /><br />
        Test method:<asp:DropDownList ID="DropDownListTestMethods" runat="server"></asp:DropDownList>Number of tests:<asp:TextBox ID="NumberOfTestsText" runat="server" Text="10">&nbsp;</asp:TextBox><asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Test" />
        <ul>
            <li>Max: <asp:Label ID="LabelMax" runat="server" Text="?"></asp:Label></li>
            <li>Min: <asp:Label ID="LabelMin" runat="server" Text="?"></asp:Label></li>
            <li>Avg: <asp:Label ID="LabelAvg" runat="server" Text="?" Font-Bold="true"></asp:Label></li>
            <li>Registered modules: <asp:Label ID="LabelRegModules" runat="server" Text="?"></asp:Label></li>
        </ul>
    </div>
    </form>
</body>
</html>
