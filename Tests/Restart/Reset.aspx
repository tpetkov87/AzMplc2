<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reset.aspx.cs" Inherits="SitefinityWebApp.Reset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2  >Reset Sitefinity</h2>
        <div><asp:CheckBox ID="fullRestart" runat="server" Text="Full Restart" /></div>
        <div><asp:Button ID="RestartButton" runat="server" Text="Restart" OnClick="RestartButton_Click" /></div>
    </div>
    </form>
</body>
</html>
