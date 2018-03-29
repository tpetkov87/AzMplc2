<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubstitutionCacheControl.ascx.cs" Inherits="SitefinityWebApp.Tests.OutputCacheTests.SubstitutionCacheControl" %>
<p>Page render</p>
<ul>
    <li><b>Time: </b><asp:Literal ID="time" runat="server"></asp:Literal></li>
    <li><b>Site: </b><asp:Literal ID="site" runat="server"></asp:Literal></li>
    <li><b>Profile: </b><asp:Literal ID="CacheProfile" runat="server"></asp:Literal></li>
</ul>
<p>Page render</p>
<asp:Substitution ID="Substitution1" runat="server" Visible="True" methodname="GetInformation" />