using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Business;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Common;

namespace SitefinityWebApp.ReportsTest
{
    public partial class ReportsTest : System.Web.UI.Page
    {
        #region Properties

        protected CatalogManager CatalogManager
        {
            get
            {
                if (this.catalogManager == null)
                    this.catalogManager = CatalogManager.GetManager();
                return this.catalogManager;
            }
        }

        protected EcommerceManager EcommerceManager
        {
            get
            {
                if (this.EcommerceManager == null)
                    this.ecommerceManager = EcommerceManager.GetManager();
                return this.ecommerceManager;
            }
        }

        protected OrdersManager OrdersManager
        {
            get
            {
                if (this.ordersManager == null)
                    this.ordersManager = OrdersManager.GetManager();
                return this.ordersManager;
            }
        }

        #endregion

        #region Event handlers

        protected void installProductTypes_Click(object sender, EventArgs e)
        {
            var ecommerceConfig = Config.Get<EcommerceConfig>();
            if (!ecommerceConfig.DefaultProductTypeFieldsInstalled)
            {
                if (ecommerceConfig.DefaultProductTypesInstalled)
                {
                    var configMananeger = ConfigManager.GetManager();
                    ecommerceConfig = configMananeger.GetSection<EcommerceConfig>();
                    DefaultProductTypesInstaller.InstallDefaultProductTypeFields(ecommerceConfig);
                    configMananeger.SaveSection(ecommerceConfig);
                }
            }
            if (!Config.Get<EcommerceConfig>().DefaultProductTypesInstalled)
                DefaultProductTypesInstaller.InstallDefaultProductTypes();
        }

        protected void createProducts_Click(object sender, EventArgs e)
        {
            this.CreateProducts(500);
        }

        protected void deleteProducts_Click(object sender, EventArgs e)
        {
            this.DeleteAllProducts();
        }

        protected void createCustomers_Click(object sender, EventArgs e)
        {
            this.CreateCustomers(100);
        }

        protected void deleteCustomers_Click(object sender, EventArgs e)
        {
            this.DeleteCustomers();
        }

        protected void createOrders_Click(object sender, EventArgs e)
        {
            this.CreateOrders(500);
        }

        protected void deleteOrders_Click(object sender, EventArgs e)
        {
            this.DeleteOrders();
        }

        #endregion

        #region Private methods

        private void CreateProducts(int count)
        {
            var random = new Random();

            var generalProductType = this.EcommerceManager.GetProductType(CatalogModule.generalProductTypeId);

            for (var i = 0; i < count; i++)
            {
                var product = this.CatalogManager.CreateProduct(generalProductType.ClrType);
                product.Title = string.Concat("Product ", i);
                product.UrlName = string.Concat("product-", i);
                product.Price = random.Next(1, 2000);
                product.Sku = string.Concat("sku", i);
            }

            this.CatalogManager.SaveChanges();
        }

        private void DeleteAllProducts()
        {
            var products = this.CatalogManager.GetProducts();
            foreach (var product in products)
                this.CatalogManager.DeleteProduct(product);

            this.CatalogManager.SaveChanges();
        }

        private void CreateCustomers(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var customer = this.OrdersManager.CreateCustomer();
                customer.CustomerFirstName = string.Concat("First ", i);
                customer.CustomerLastName = string.Concat("Last ", i);
                customer.CustomerEmail = string.Format("customer{0}@telerik.com", i);
            }

            this.OrdersManager.SaveChanges();
        }

        private void DeleteCustomers()
        {
            var customers = this.OrdersManager.GetCustomers();
            foreach (var customer in customers)
                this.OrdersManager.DeleteCustomer(customer);

            this.OrdersManager.SaveChanges();
        }

        private void CreateOrders(int count)
        {
            var products = this.CatalogManager.GetProducts().ToList();
            var customers = this.OrdersManager.GetCustomers().ToList();

            var random = new Random();
            var customerCount = customers.Count();
            
            for (var i = 0; i < count; i++)
            {
                var customer = customers[random.Next(0, customerCount - 1)];

                var order = this.OrdersManager.CreateOrder();
                
                order.Customer = customer;
                order.OrderNumber = i + 1;
                order.OrderStatus = this.GetRandomOrderStatus();
                order.CurrencyInfo = "US";
                order.Currency = "US";
                this.AddRandomProductsToOrder(order, products);

                var shippingPair = this.GetRandomCityCounty();
                var shippingAddress = this.OrdersManager.CreateOrderAddress();
                shippingAddress.Address = string.Concat("Address ", i);
                shippingAddress.AddressType = AddressType.Shipping;
                shippingAddress.Country = shippingPair.Value;
                shippingAddress.City = shippingPair.Key;
                shippingAddress.FirstName = string.Concat("First ", i);
                shippingAddress.LastName = string.Concat("Last ", i);
                shippingAddress.Email = string.Format("email{0}@test.test", i);
                shippingAddress.PostalCode = "01234";
                order.Addresses.Add(shippingAddress);

                var billingPair = this.GetRandomCityCounty();
                var billingAddress = this.OrdersManager.CreateOrderAddress();
                billingAddress.Address = string.Concat("Address ", i);
                billingAddress.AddressType = AddressType.Billing;
                billingAddress.Country = billingPair.Value;
                billingAddress.City = billingPair.Key;
                billingAddress.FirstName = string.Concat("First ", i);
                billingAddress.LastName = string.Concat("Last ", i);
                billingAddress.Email = string.Format("email{0}@test.test", i);
                billingAddress.PostalCode = "01234";
                order.Addresses.Add(billingAddress);

                this.OrdersManager.SaveChanges();
                order.OrderDate = DateTime.Now.AddDays(-i);
                this.OrdersManager.SaveChanges();
            }
        }

        private OrderStatus GetRandomOrderStatus()
        {
            var random = new Random();
            switch (random.Next(0, 4))
            {
                case 0:
                    return OrderStatus.Declined;
                case 1:
                    return OrderStatus.Pending;
                case 2:
                    return OrderStatus.Paid;
                case 3:
                    return OrderStatus.Shipped;
                case 4:
                    return OrderStatus.Unknown;
            }

            return OrderStatus.Unknown;
        }

        private KeyValuePair<string, string> GetRandomCityCounty()
        {
            var cityCountry = new Dictionary<string, string>();

            cityCountry.Add("New York", "United States");
            cityCountry.Add("Las Vegas", "United States");
            cityCountry.Add("Miami", "United States");
            cityCountry.Add("San Diego", "United States");
            cityCountry.Add("Bonn", "Germany");
            cityCountry.Add("Berlin", "Germany");
            cityCountry.Add("Dresden", "Germany");
            cityCountry.Add("Plovdiv", "Bulgaria");
            cityCountry.Add("Sofia", "Bulgaria");
            cityCountry.Add("Svistov", "Bulgaria");
            cityCountry.Add("Varna", "Bulgaria");
            cityCountry.Add("Zagreb", "Croatia");
            cityCountry.Add("Pakrac", "Croatia");

            var random = new Random();
            return cityCountry.ElementAt(random.Next(0,12));

        }

        private void AddRandomProductsToOrder(Order order, IList<Product> products)
        {
            var random = new Random();

            int numberOfProductsToAdd = random.Next(1, 5);

            for (var i = 0; i < numberOfProductsToAdd; i++)
            {
                var product = products[random.Next(0, products.Count - 1)];
                var quantity = random.Next(1, 30);

                var orderDetail = this.OrdersManager.CreateOrderDetail();
                orderDetail.ProductId = product.Id;
                orderDetail.Price = product.Price;
                orderDetail.Quantity = quantity;
                orderDetail.Sku = product.Sku;
                orderDetail.Title = product.Title;
                orderDetail.Total = product.Price * quantity;

                order.Details.Add(orderDetail);
            }

        }

        private void DeleteOrders()
        {
            var orders = this.OrdersManager.GetOrders();
            foreach (var order in orders)
                this.OrdersManager.DeleteOrder(order);
            this.OrdersManager.SaveChanges();
        }

        #endregion

        #region Private fields and constants

        private CatalogManager catalogManager;
        private EcommerceManager ecommerceManager;
        private OrdersManager ordersManager;

        #endregion
    }
}