<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LibrariesTestPage.aspx.cs" Inherits="SitefinityWebApp.LibrariesTestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Album Name: <asp:TextBox runat="server" ID="libUrlName"></asp:TextBox>
        <br />
        Change Album name to: <asp:TextBox runat="server" ID="libNewUrlName"></asp:TextBox>
        <br />
        Change Blob Storage to: <asp:TextBox runat="server" ID="libNewStorage"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Start" />&nbsp;&nbsp;<asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="Refresh" />&nbsp;<asp:Label runat="server" ID="progress"></asp:Label>
        <br />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Stop" />
        <br />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Restart" />
    </div>
    </form>
</body>
</html>
