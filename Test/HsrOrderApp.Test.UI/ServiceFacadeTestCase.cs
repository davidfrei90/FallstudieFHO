#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses;
using HsrOrderApp.SharedLibraries.ServiceInterfaces;
using HsrOrderApp.UI.PresentationLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock2;

#endregion

namespace HsrOrderApp.Test.UI
{
    /// <summary>
    /// Summary description for TestServiceFacade
    /// </summary>
    [TestClass]
    public class ServiceFacadeTestCase
    {
        private Mockery mockBuilder;
        private IAdminService service;
        private IServiceFacade serviceFacade;
        private TestContext testContextInstance;


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestInitialize()]
        public void Initialize()
        {
            mockBuilder = new Mockery();
            service = mockBuilder.NewMock<IAdminService>();
            serviceFacade = ServiceFacade.GetInstance(service);
        }

        #region Order

        [TestMethod]
        public void TestGetOrder()
        {
            int orderId = 1;
            GetOrderResponse response = new GetOrderResponse();
            response.Order = new OrderDTO() {Id = orderId};

            Expect.Once.On(service).Method("GetOrderById").Will(Return.Value(response));
            OrderDTO order = serviceFacade.GetOrderById(orderId);
            Assert.AreEqual(order.Id, orderId);
        }

        [TestMethod]
        public void TestGetOrders()
        {
            int orderId = 1;
            GetOrdersResponse response = new GetOrdersResponse();
            response.Orders.Add(new OrderListDTO() {Id = orderId});

            Expect.Once.On(service).Method("GetOrdersByCriteria").Will(Return.Value(response));
            IList<OrderListDTO> orders = serviceFacade.GetOrdersByCustomer(orderId);
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(orders.First().Id, orderId);

            Expect.Once.On(service).Method("GetOrdersByCriteria").Will(Return.Value(response));
            orders = serviceFacade.GetAllOrders();
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(orders.First().Id, orderId);
        }

        [TestMethod]
        public void TestStoreOrder()
        {
            OrderDTO order = new OrderDTO();
            order.Id = 123;
            StoreOrderResponse response = new StoreOrderResponse();
            response.Id = order.Id;
            Expect.Once.On(service).Method("StoreOrder").Will(Return.Value(response));
            serviceFacade.StoreOrder(order);
        }

        [TestMethod]
        public void TestDeleteOrder()
        {
            DeleteOrderResponse response = new DeleteOrderResponse();
            Expect.Once.On(service).Method("DeleteOrder").Will(Return.Value(response));
            serviceFacade.DeleteOrder(1);
        }

        #endregion

        #region Customer

        [TestMethod]
        public void TestGetCustomer()
        {
            int customerId = 1;
            GetCustomerResponse response = new GetCustomerResponse();
            response.Customer = new CustomerDTO() { Id = customerId };

            Expect.Once.On(service).Method("GetCustomerById").Will(Return.Value(response));
            CustomerDTO customer = serviceFacade.GetCustomerById(customerId);
            Assert.AreEqual(customer.Id, customerId);
        }

        [TestMethod]
        public void TestGetCustomers()
        {
            int customerId = 1;
            GetCustomersResponse response = new GetCustomersResponse();
            response.Customers.Add(new CustomerListDTO() {Id = customerId});

            Expect.Once.On(service).Method("GetCustomersByCriteria").Will(Return.Value(response));
            IList<CustomerListDTO> customers = serviceFacade.GetCustomersByCity("FakeCity");
            Assert.AreEqual(1, customers.Count);
            Assert.AreEqual(customers.First().Id, customerId);

            Expect.Once.On(service).Method("GetCustomersByCriteria").Will(Return.Value(response));
            customers = serviceFacade.GetCustomersByName("FAkeName");
            Assert.AreEqual(1, customers.Count);
            Assert.AreEqual(customers.First().Id, customerId);

            Expect.Once.On(service).Method("GetCustomersByCriteria").Will(Return.Value(response));
            customers = serviceFacade.GetAllCustomers();
            Assert.AreEqual(1, customers.Count);
            Assert.AreEqual(customers.First().Id, customerId);
        }

        [TestMethod]
        public void TestStoreCustomer()
        {
            CustomerDTO customer = new CustomerDTO();
            customer.Id = 123;
            StoreCustomerResponse response = new StoreCustomerResponse();
            response.CustomerId = customer.Id;
            Expect.Once.On(service).Method("StoreCustomer").Will(Return.Value(response));
            serviceFacade.StoreCustomer(customer);
        }

        [TestMethod]
        public void TestDeleteCustomer()
        {
            DeleteCustomerResponse response = new DeleteCustomerResponse();
            Expect.Once.On(service).Method("DeleteCustomer").Will(Return.Value(response));
            serviceFacade.DeleteCustomer(1);
        }

        #endregion
                
        #region Product

        [TestMethod]
        public void TestGetProduct()
        {
            int productId = 1;
            GetProductResponse response = new GetProductResponse();
            response.Product = new ProductDTO() {Id = productId};

            Expect.Once.On(service).Method("GetProductById").Will(Return.Value(response));
            ProductDTO product = serviceFacade.GetProductById(productId);
            Assert.AreEqual(product.Id, productId);
        }

        [TestMethod]
        public void TestGetProducts()
        {
            int productId = 1;
            GetProductsResponse response = new GetProductsResponse();
            response.Products.Add(new ProductDTO() {Id = productId});

            Expect.Once.On(service).Method("GetProductsByCriteria").Will(Return.Value(response));
            IList<ProductDTO> products = serviceFacade.GetProductsByCategory("FakeCategory");
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual(products.First().Id, productId);

            Expect.Once.On(service).Method("GetProductsByCriteria").Will(Return.Value(response));
            products = serviceFacade.GetProductsByName("FAkeName");
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual(products.First().Id, productId);

            Expect.Once.On(service).Method("GetProductsByCriteria").Will(Return.Value(response));
            products = serviceFacade.GetAllProducts();
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual(products.First().Id, productId);
        }

        [TestMethod]
        public void TestStoreProduct()
        {
            ProductDTO product = new ProductDTO();
            product.Id = 123;
            StoreProductResponse response = new StoreProductResponse();
            response.Id = product.Id;
            Expect.Once.On(service).Method("StoreProduct").Will(Return.Value(response));
            serviceFacade.StoreProduct(product);
        }

        [TestMethod]
        public void TestDeleteProduct()
        {
            DeleteProductResponse response = new DeleteProductResponse();
            Expect.Once.On(service).Method("DeleteProduct").Will(Return.Value(response));
            serviceFacade.DeleteProduct(1);
        }

        #endregion

        #region User

        [TestMethod]
        public void TestGetUser()
        {
            int userId = 1;
            GetUserResponse response = new GetUserResponse();
            response.User = new UserDTO() {Id = userId};

            Expect.Once.On(service).Method("GetUserById").Will(Return.Value(response));
            UserDTO user = serviceFacade.GetUserById(userId);
            Assert.AreEqual(user.Id, userId);
        }

        [TestMethod]
        public void TestGetUsers()
        {
            int userId = 1;
            GetUsersResponse response = new GetUsersResponse();
            response.Users.Add(new UserListDTO() {Id = userId, UserName = "FakeName"});

            Expect.Once.On(service).Method("GetUsersByCriteria").Will(Return.Value(response));
            IList<UserListDTO> users = serviceFacade.GetUsersByRole("FakeRole");
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual(users.First().Id, userId);

            Expect.Once.On(service).Method("GetUsersByCriteria").Will(Return.Value(response));
            users = serviceFacade.GetUsersByName("FakeName");
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual(users.First().Id, userId);

            Expect.Once.On(service).Method("GetUsersByCriteria").Will(Return.Value(response));
            users = serviceFacade.GetAllUsers();
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual(users.First().Id, userId);
        }

        [TestMethod]
        public void TestStoreUser()
        {
            UserDTO user = new UserDTO();
            user.Id = 123;
            StoreUserResponse response = new StoreUserResponse();
            response.Id = user.Id;
            Expect.Once.On(service).Method("StoreUser").Will(Return.Value(response));
            serviceFacade.StoreUser(user);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            DeleteUserResponse response = new DeleteUserResponse();
            Expect.Once.On(service).Method("DeleteUser").Will(Return.Value(response));
            serviceFacade.DeleteUser(1);
        }


        [TestMethod]
        public void TestGetRole()
        {
            int roleId = 1;
            GetRoleResponse response = new GetRoleResponse();
            response.Role = new RoleDTO() { Id = roleId };

            Expect.Once.On(service).Method("GetRoleById").Will(Return.Value(response));
            RoleDTO role = serviceFacade.GetRoleById(roleId);
            Assert.AreEqual(role.Id, roleId);
        }


        [TestMethod]
        public void TestGetRoles()
        {
            int roleId = 1;
            GetRolesResponse response = new GetRolesResponse();
            response.Roles.Add(new RoleDTO() { Id = roleId, RoleName = "FakeRole" });

            Expect.Once.On(service).Method("GetRolesByCriteria").Will(Return.Value(response));
            IList<RoleDTO> roles = serviceFacade.GetRolesByName("FakeRole");
            Assert.AreEqual(1, roles.Count);
            Assert.AreEqual(roles.First().Id, roleId);

            Expect.Once.On(service).Method("GetRolesByCriteria").Will(Return.Value(response));
            roles = serviceFacade.GetAllRoles();
            Assert.AreEqual(1, roles.Count);
            Assert.AreEqual(roles.First().Id, roleId);
        }

        #endregion
    }
}