<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportsTest.aspx.cs" Inherits="SitefinityWebApp.ReportsTest.ReportsTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            <asp:Button ID="installProductTypes" runat="server" Text="Install product types" OnClick="installProductTypes_Click" Visible="false" />
        </h1>
        <h3>Products</h3>
        <asp:Button ID="createProducts" runat="server" Text="Create 500 products" OnClick="createProducts_Click" />
        |
        <asp:Button ID="deleteProducts" runat="server" Text="Delete all products" OnClick="deleteProducts_Click" />
        <h3>Customers</h3>
        <asp:Button ID="createCustomers" runat="server" Text="Create customers" OnClick="createCustomers_Click" />
        |
        <asp:Button ID="deleteCustomers" runat="server" Text="Delete all customers" OnClick="deleteCustomers_Click" />
        <h3>Orders</h3>
        <asp:Button ID="createOrders" runat="server" Text="Create orders" OnClick="createOrders_Click" />
        |
        <asp:Button ID="deleteOrders" runat="server" Text="Delete orders" OnClick="deleteOrders_Click" />        
    </div>
    </form>
</body>
</html>
