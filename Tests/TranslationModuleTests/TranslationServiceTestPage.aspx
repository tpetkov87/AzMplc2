<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TranslationServiceTestPage.aspx.cs" Inherits="SitefinityWebApp.Tests.TranslationModuleTests.TranslationServiceTestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ul>
            <li>
                <asp:Button ID="SendAllNewsForTranslationButton" runat="server" Text="Send All News For Translation" OnClick="SendAllNewsForTranslationButton_Click" />
            </li>
            <li>
                <asp:Button ID="MarkAllNewsForTranslationButton" runat="server" Text="Mark All News For Translation" OnClick="MarkAllNewsForTranslationButton_Click" />
            </li>
            <li>
                <asp:Button ID="GetFirstNewsItemStatusButton" runat="server" Text="GetFirstNewsItemStatus" OnClick="GetFirstNewsItemStatusButton_Click" />
                <asp:Label ID="FirstNewsItemStatusLabel" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Button ID="MarkSiteForTranslationButton" runat="server" Text="Mark site for translation" OnClick="MarkSiteForTranslationButton_Click" />
                <asp:DropDownList ID="SiteSelector" runat="server"></asp:DropDownList>
            </li>
        </ul>
    </div>
    </form>
</body>
</html>
