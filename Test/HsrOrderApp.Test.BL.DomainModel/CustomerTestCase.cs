#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HsrOrderApp.Test.BL.DomainModel
{
    /// <summary>
    /// Summary description for CustomerTestCase
    /// </summary>
    [TestClass]
    public class CustomerTestCase
    {
        private Customer customer;

        [TestInitialize]
        public void SetUp()
        {
            customer = new Customer();
        }

        [TestCleanup]
        public void TearDown()
        {
            customer = null;
        }

        #region ctors tests

        [TestMethod]
        public void Build()
        {
            customer = new Customer();
            Helper.AssertEmptiness(customer, "CustomerId", "Name", "FirstName", "Orders", "Addresses");
        }

        #endregion

        #region Property Assignment

        [TestMethod]
        public void PropsAssignments()
        {
            Helper.TestProperty<int>(123, "CustomerId", customer);
            Helper.TestProperty<string>("FakeName", "Name", customer);
            Helper.TestProperty<string>("FakeFirstname", "FirstName", customer);
            customer.Addresses = new List<Address>() {new Address()}.AsQueryable();
            customer.Orders = new List<Order>() {new Order()}.AsQueryable();
            Assert.AreEqual<int>(1, customer.Orders.Count());
            Assert.AreEqual<int>(1, customer.Addresses.Count());
        }

        [TestMethod]
        public void Validation()
        {
            customer = new Customer()
                           {
                               CustomerId = 1,
                               FirstName = "FakeName",
                               Name = "FakeName",
                               Version = 0
                           };
            Assert.IsTrue(customer.IsValid, "Should be valid");
        }

        [TestMethod]
        public void ValidationFails()
        {
            customer = new Customer();
            Assert.IsFalse(customer.IsValid, "Should be invalid");
        }

        #endregion
    }
}