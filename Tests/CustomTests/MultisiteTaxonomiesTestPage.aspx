<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultisiteTaxonomiesTestPage.aspx.cs" Inherits="SitefinityWebApp.Tests.CustomTests.MultisiteTaxonomiesTestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal runat="server" ID="StatusLiteral" />
        <br />
        <br />
        <asp:Label AssociatedControlID="GenerateFlatButton">Generate flat split taxonomy:</asp:Label>
        <asp:Button runat="server" Text="Generate" ID="GenerateFlatButton" onclick="GenerateFlatButtonOnClick" />
        <br />
        <br />
        <asp:Label AssociatedControlID="TaxonomyNameTextBox">Taxonomy name:</asp:Label>
        <asp:TextBox runat="server" id="TaxonomyNameTextBox"/>
        <asp:Label AssociatedControlID="UnSplitButton">Remove splits for taxonomy:</asp:Label>
        <asp:Button runat="server" Text="UnSplit" ID="UnSplitButton" onclick="UnSplitButtonOnClick" />
        <br />
        <br />
        Remove site taxonomy link<br />
        <asp:Label AssociatedControlID="TaxonomyNameUnlinkTextBox">Taxonomy name:</asp:Label>
        <asp:TextBox runat="server" id="TaxonomyNameUnlinkTextBox"/>
        <asp:Label AssociatedControlID="SiteIdUnlinkTextBox">Site Id:</asp:Label>
        <asp:TextBox runat="server" id="SiteIdUnlinkTextBox"/>
        <asp:Label AssociatedControlID="UnLinkButton">Remove link:</asp:Label>
        <asp:Button runat="server" Text="UnLinkButton" ID="UnLinkButton" onclick="UnLinkButtonOnClick" />
        <br />
        <br />
    </div>
    </form>
</body>
</html>
