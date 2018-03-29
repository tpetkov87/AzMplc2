<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Warmup.aspx.cs" Inherits="SitefinityWebApp.Tests.OutputCacheTests.Warmup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <header>Warm-up spike tests</header>
        <div style="display:none">
            <asp:Button ID="SetupBtn" runat="server" Text="Setup" OnClick="SetupBtn_Click" />
            <asp:Literal ID="setupResult" runat="server"></asp:Literal>
        </div>
        <p>Considerations</p>
        <ul>
            <li>Go to <asp:HyperLink NavigateUrl="~/Sitefinity/Administration/Settings/Advanced/System" runat="server" Target="_blank">System Settings</asp:HyperLink>, open OutputCacheSettings > Output Cache Profiles > Standard Caching, and make sure that "Vary by user agent" is not checked and "Vary by host" is checked</li>
            <li>Make sure the following setting is set in the web.config: aspnet:UseHostHeaderForRequestUrl=true <a target="_blank" href="http://knowledgebase.progress.com/articles/Article/incorrect-urls-generated-when-using-nlb-and-nodes-under-different-ports">For more information.</a></li>
        </ul>
        <p>Scenarios</p>
        <ul>
            <li>
                <p>Warm-up all pages in all sites</p>
                <asp:Button ID="WarmupBtn" runat="server" Text="Warm-up" OnClick="WarmupBtn_Click" />
                <asp:Literal ID="warmupResult" runat="server"></asp:Literal>
            </li>
            <li>
                <p>Warm-up pages from the list</p>
                <asp:TextBox runat="server" TextMode="MultiLine" ID="urls" Rows="5" Width="100%"></asp:TextBox>
                <asp:Button ID="WarmupByUrlBtn" runat="server" Text="Warm-up" OnClick="WarmupByUrlBtn_Click" />
            </li>
        </ul>
    </div>
    </form>
</body>
</html>
