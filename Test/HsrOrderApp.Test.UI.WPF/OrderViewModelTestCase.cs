#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Order;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock2;

#endregion

namespace HsrOrderApp.Test.UI.WPF
{
    /// <summary>
    /// Summary description for OrderViewModelTestCase
    /// </summary>
    [TestClass]
    public class OrderViewModelTestCase
    {
        private Mockery mockBuilder;
        private IServiceFacade serviceFacade;

        private TestContext testContextInstance;

        public OrderViewModelTestCase()
        {
        }

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
            serviceFacade = mockBuilder.NewMock<IServiceFacade>();
        }

        [TestMethod]
        public void TestOrderViewModel()
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.Service = serviceFacade;
            OrderListDTO order = new OrderListDTO() {Id = 1};
            IList<OrderListDTO> orders = new List<OrderListDTO>() {order};
            Expect.Once.On(serviceFacade).Method("GetAllOrders").Will(Return.Value(orders));
            orderViewModel.LoadCommand.Command.Execute(null);

            Assert.AreEqual<int>(1, orderViewModel.Items.Count);
            Assert.AreEqual(order, orderViewModel.SelectedItem);
            Assert.AreEqual(Strings.OrderViewModel_DisplayName, orderViewModel.DisplayName);
        }

        [TestMethod]
        public void TestOrderDetailModel()
        {
            OrderDetailDTO detailDto = new OrderDetailDTO() {Id = 1, QuantityInUnits = 123, UnitPrice = 213, ProductId = 1, ProductName = "FAkeProduct", Version = 1};
            OrderDTO dto = new OrderDTO() {Id = 1, OrderStatus = OrderStatus.Ordered, OrderDate = DateTime.Now, ShippedDate = null, CustomerId = 123, CustomerName = "Fakecustomer"};
            dto.Details.Add(detailDto);

            OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel(dto, false);
            orderDetailViewModel.Service = serviceFacade;

            Expect.Once.On(serviceFacade).Method("StoreOrder").With(dto);
            orderDetailViewModel.SaveCommand.Command.Execute(null);

            Assert.AreEqual(dto, orderDetailViewModel.Model);
            Assert.AreEqual(Strings.OrderDetailViewModel_DisplayName, orderDetailViewModel.DisplayName);

            OrderItemViewModel detailListViewModel = (OrderItemViewModel) orderDetailViewModel.ListViewModel;
            Assert.AreEqual(detailDto, detailListViewModel.Items.FirstOrDefault());
        }

        [TestMethod]
        public void TestOrderDetailViewModel()
        {
            OrderDetailDTO dto = new OrderDetailDTO() {Id = 1, QuantityInUnits = 123, UnitPrice = 213, ProductId = 1, ProductName = "FAkeProduct", Version = 1};
            OrderItemDetailViewModel viewModel = new OrderItemDetailViewModel(dto, false);
            Assert.AreEqual(dto, viewModel.Model);
            Assert.AreEqual(Strings.OrderItemDetailViewModel_DisplayName, viewModel.DisplayName);
        }
    }
}