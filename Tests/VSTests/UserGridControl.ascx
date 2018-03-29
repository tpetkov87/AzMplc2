<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserGridControl.ascx.cs" Inherits="SitefinityWebApp.UserGridControl" %>

<asp:GridView ID="gridView" runat="server" AllowPaging="True" 
    AllowSorting="True" AutoGenerateColumns="true" PageSize="2" 
    onpageindexchanging="gridView_PageIndexChanging" 
    onsorting="gridView_Sorting">
</asp:GridView>