<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormsTestPage.aspx.cs" Inherits="SitefinityWebApp.CustomTests.FormsTestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="FormTitleTextBox" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ensure Unique Urls for attached files" />
    </form>
</body>
</html>
