<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchedulingTestPage.aspx.cs" Inherits="SitefinityWebApp.CustomTests.SchedulingTestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- <meta http-equiv="refresh" content="1" /> --%>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Scheduled Tasks</h1>

        <asp:Button ID="uxStart" Text="Start new" OnClick="uxStart_Click" runat="server" />
        <asp:Button ID="uxResume" Text="Resume failed" OnClick="uxResume_Click" runat="server" />
        
        <br />
        <asp:TextBox ID="uxNameFilter" runat="server" />
        <asp:Button ID="uxFilter" Text="Refresh" OnClick="uxFilter_Click" runat="server" />

        <br />
        <asp:TextBox ID="uxStatus" TextMode="MultiLine" Rows="10" Columns="100" runat="server" />
    </div>
    </form>
</body>
</html>
