#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock2;

#endregion

namespace HsrOrderApp.Test.BL.BusinessComponents
{
    /// <summary>
    /// Summary description for OrderBusinessComponentTestCase
    /// </summary>
    [TestClass]
    public class OrderBusinessComponentTestCase
    {
        private IOrderRepository context;
        private Mockery mockBuilder;
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

        [TestInitialize()]
        public void Initialize()
        {
            mockBuilder = new Mockery();
            context = mockBuilder.NewMock<IOrderRepository>();
        }

        [TestMethod]
        public void TestGetOrderById()
        {
            OrderBusinessComponent service = new OrderBusinessComponent(this.context);
            Order order = new Order() {OrderId = 123};

            Expect.Once.On(context).Method("GetById").Will(Return.Value(order));
            Order resultOrder = service.GetOrderById(123);
            Assert.AreEqual<decimal>(order.OrderId, resultOrder.OrderId);
            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void TestGetOrderByCriteria()
        {
            OrderBusinessComponent service = new OrderBusinessComponent(this.context);
            Order order = new Order {OrderId = 456, Customer = new Customer {CustomerId = 1234}};
            IList<Order> orders = new List<Order> {order};

            foreach (OrderSearchType type in Enum.GetValues(typeof (OrderSearchType)))
            {
                Expect.Once.On(context).Method("GetAll").Will(Return.Value(orders.AsQueryable()));
                IQueryable<Order> resultOrders = service.GetOrdersByCriteria(type, 1234);
                Assert.AreEqual<decimal>(1, resultOrders.Count());
                Assert.AreEqual<decimal>(order.OrderId, resultOrders.First().OrderId);
            }

            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void TestStoreOrder()
        {
            int orderId = 123;
            OrderBusinessComponent service = new OrderBusinessComponent(this.context);
            Order order = new Order() {OrderId = orderId};
            List<ChangeItem> changeItems = new List<ChangeItem>
                                               {
                                                   new ChangeItem(ChangeType.ChildInsert, new OrderDetail()),
                                                   new ChangeItem(ChangeType.ChildUpate, new OrderDetail()),
                                                   new ChangeItem(ChangeType.ChildDelete, new OrderDetail())
                                               };

            Expect.Once.On(context).Method("SaveOrder").Will(Return.Value(orderId));
            Expect.Once.On(context).Method("SaveOrderDetail").Will(Return.Value(1));
            Expect.Once.On(context).Method("SaveOrderDetail").Will(Return.Value(1));
            Expect.Once.On(context).Method("DeleteOrderDetail");
            int resultOrderId = service.StoreOrder(order, changeItems);
            Assert.AreEqual<int>(orderId, resultOrderId);

            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        public void TestDeleteOrder()
        {
            OrderBusinessComponent service = new OrderBusinessComponent(this.context);

            Expect.Once.On(context).Method("DeleteOrder").With(1);
            service.DeleteOrder(1);
            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }
    }
}