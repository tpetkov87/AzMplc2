<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceOptimizations.aspx.cs" Inherits="SitefinityWebApp.Tests.PerformanceOptimizations" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button_ChangeDynModulesConn" runat="server" Text="Move dynamic modules to a separate connection (same physical database)" OnClick="Button_ChangeDynModulesConn_Click" />
        <br />
        <br />
        <asp:Button ID="Button_VersioningSepration" runat="server" Text="Move versioning in separate database" OnClick="Button_VersioningSepration_Click" />
        <br />
        <br />
        <asp:Button ID="Button_BlobSeparation" runat="server" Text="Move blob storage in separate database" OnClick="Button_BlobSeparation_Click" />
    </div>
    </form>
</body>
</html>
