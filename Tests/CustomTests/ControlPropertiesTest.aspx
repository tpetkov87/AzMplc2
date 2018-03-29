<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlPropertiesTest.aspx.cs" Inherits="SitefinityWebApp.CustomTests.ControlPropertiesTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Re-persist all pages controls" />
        </p>
        <p>Enter url name of the page to display its controls and properties for the page Live, Draft and Temp versions</p>
        Page Url Name:
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show Controls &amp; Props" />
        <br />
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
