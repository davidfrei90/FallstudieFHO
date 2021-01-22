#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HsrOrderApp.Test.BL.DomainModel
{
    /// <summary>
    /// Summary description for OrderTestCase
    /// </summary>
    [TestClass]
    public class OrderTestCase
    {
        private Order order;

        [TestInitialize]
        public void SetUp()
        {
            order = new Order();
        }

        [TestCleanup]
        public void TearDown()
        {
            order = null;
        }

        #region ctors tests

        [TestMethod]
        public void Build()
        {
            Order order = new Order();
            Helper.AssertEmptiness(order, "OrderId", "OrderDate", "ShippedDate", "OrderDetails");
            Assert.IsInstanceOfType(order.Customer, typeof (UnkownCustomer), "Customer should be of type unknown customer.");
            Assert.AreEqual(OrderStatus.Draft, order.OrderStatus);
        }

        #endregion

        #region Property Assignment

        [TestMethod]
        public void PropsAssignments()
        {
            Helper.TestProperty<int>(123, "OrderId", order);
            Helper.TestProperty<DateTime>(DateTime.Now, "OrderDate", order);
            Helper.TestProperty<DateTime>(DateTime.Now, "ShippedDate", order);
            order.OrderStatus = OrderStatus.Draft;
            Assert.AreEqual(OrderStatus.Draft, order.OrderStatus);
            order.Customer = new Customer();
            Assert.IsNotNull(order.Customer);
            order.OrderDetails = new List<OrderDetail>() {new OrderDetail()}.AsQueryable();
            Assert.AreEqual<int>(1, order.OrderDetails.Count());
        }

        [TestMethod]
        public void Validation()
        {
            order = new Order()
                        {
                            OrderId = 1,
                            OrderStatus = OrderStatus.Ordered,
                            OrderDate = DateTime.Now,
                            ShippedDate = null,
                            Version = 0
                        };
            Assert.IsTrue(order.IsValid, "Should be valid");
        }

        [TestMethod]
        public void ValidationFails()
        {
            order = new Order()
                        {
                            OrderId = 1,
                            OrderStatus = OrderStatus.Draft,
                            OrderDate = DateTime.Now,
                            ShippedDate = DateTime.Now,
                            Version = 0
                        };
            Assert.IsFalse(order.IsValid, "Should be invalid");

            order = new Order()
                        {
                            OrderId = 1,
                            OrderStatus = OrderStatus.Ordered,
                            OrderDate = null,
                            ShippedDate = DateTime.Now,
                            Version = 0
                        };
            Assert.IsFalse(order.IsValid, "Should be invalid");
            Assert.AreEqual<int>(2, order.Validate().Count);

            order = new Order()
                        {
                            OrderId = 1,
                            OrderStatus = OrderStatus.Shipped,
                            OrderDate = null,
                            ShippedDate = null,
                            Version = 0
                        };
            Assert.IsFalse(order.IsValid, "Should be invalid");
            Assert.AreEqual<int>(2, order.Validate().Count);
        }

        #endregion
    }
}