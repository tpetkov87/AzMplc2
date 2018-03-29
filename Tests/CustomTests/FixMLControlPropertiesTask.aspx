<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FixMLControlPropertiesTask.aspx.cs" Inherits="SitefinityWebApp.Tests.CustomTests.ControlPropertiesReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="execute" Text="Execute" />
        <asp:Button runat="server" ID="report" Text="Report" Visible="false" />
        <asp:DropDownList ID="siteList" runat="server" DataTextField="Name" DataValueField="SiteMapRootNodeId" AutoPostBack="true" />
        <div id="errorMessage" runat="server">
        </div>
        <asp:Repeater ID="problemPages" runat="server">
            <ItemTemplate>
                <div>
                    <span><%# Eval("Title") %></span>
                    <span><%# Eval("Id") %></span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:TreeView runat="server" ID="pageTree" />
        <div id="htmlContainer" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
