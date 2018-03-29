<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Create100MultisiteTestPage.aspx.cs" Inherits="SitefinityWebApp.CustomTests.Create100MultisiteTestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
    <div>
    
        <asp:Button ID="CreateNewSites" runat="server" Text="Create new sites" 
            onclick="CreateNewSites_Click" />
        <asp:Button ID="DeleteSites" runat="server" Text="Delete sites" 
            onclick="DeleteSites_Click" />
    </div>
    </form>
</body>
</html>
