<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnyFluentTestPage.aspx.cs" Inherits="SitefinityWebApp.AnyFluentTestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--Create, checkout, edit, checkin-and-publish--%>
        <asp:Button ID="createNew" Text="Create new" runat="server" 
            onclick="createNew_Click" /> <br />
        <%--Delete--%>
        <asp:Button ID="Button1" Text="Delete" runat="server" onclick="Button1_Click" /> <br />
        <%--Unpublish, chechkout, eidt, checkin--%>
        <asp:Button ID="Button2" Text="Edit" runat="server" onclick="Button2_Click" /> <br />
        <asp:Button ID="button3" Text="Flush All Publishing Points" OnClick="Button3_Click"  runat="server"/> <br />
    </div>
    </form>
</body>
</html>
