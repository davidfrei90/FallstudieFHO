#region

using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HsrOrderApp.Test.BL.DomainModel
{
    /// <summary>
    /// Summary description for OrderDetailTestCase
    /// </summary>
    [TestClass]
    public class OrderDetailTestCase
    {
        private OrderDetail orderDetail;

        [TestInitialize]
        public void SetUp()
        {
            orderDetail = new OrderDetail();
        }

        [TestCleanup]
        public void TearDown()
        {
            orderDetail = null;
        }

        #region ctors tests

        [TestMethod]
        public void Build()
        {
            orderDetail = new OrderDetail();
            Helper.AssertEmptiness(orderDetail, "QuantityInUnits", "UnitPrice");
            Assert.IsInstanceOfType(orderDetail.Product, typeof (UnknownProduct), "Should be a unknown product");
            Assert.IsInstanceOfType(orderDetail.Order, typeof (UnknownOrder), "Should be a unknown order");
        }

        #endregion

        #region Property Assignment

        [TestMethod]
        public void PropsAssignments()
        {
            orderDetail.Product = new Product();
            Assert.IsNotNull(orderDetail.Product);
            orderDetail.Order = new Order();
            Assert.IsNotNull(orderDetail.Order);
            Helper.TestProperty<int>(100, "QuantityInUnits", orderDetail);
            Helper.TestProperty<decimal>(123.45m, "UnitPrice", orderDetail);
        }

        [TestMethod]
        public void Validation()
        {
            orderDetail = new OrderDetail()
                              {
                                  OrderDetailId = 1,
                                  QuantityInUnits = 100,
                                  UnitPrice = 200.0m,
                                  Version = 0
                              };
            Assert.IsTrue(orderDetail.IsValid, "Should be valid");
        }

        [TestMethod]
        public void ValidationFails()
        {
            orderDetail = new OrderDetail();
            Assert.IsFalse(orderDetail.IsValid, "Should be invalid");
        }

        #endregion
    }
}