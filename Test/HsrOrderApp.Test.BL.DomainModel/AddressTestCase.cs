#region

using HsrOrderApp.BL.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HsrOrderApp.Test.BL.DomainModel
{
    /// <summary>
    /// Summary description for AddressTestCase
    /// </summary>
    [TestClass]
    public class AddressTestCase
    {
        private Address address;

        [TestInitialize]
        public void SetUp()
        {
            address = new Address();
        }

        [TestCleanup]
        public void TearDown()
        {
            address = null;
        }

        #region ctors tests

        [TestMethod]
        public void Build()
        {
            address = new Address();
            Helper.AssertEmptiness(address, "AddressId", "AddressLine1", "AddressLine2", "PostalCode", "City", "Phone");
        }

        #endregion

        #region Property Assignment

        [TestMethod]
        public void PropsAssignments()
        {
            Helper.TestProperty<int>(123, "AddressId", address);
            Helper.TestProperty<string>("FakeStreet", "AddressLine1", address);
            Helper.TestProperty<string>("FakeStreet", "AddressLine2", address);
            Helper.TestProperty<string>("FakePostalCode", "PostalCode", address);
            Helper.TestProperty<string>("FakeCity", "City", address);
            Helper.TestProperty<string>("FakePhone", "Phone", address);
        }

        [TestMethod]
        public void Validation()
        {
            address = new Address()
                          {
                              AddressId = 1,
                              City = "FakeCity",
                              Email = "FakeEmail",
                              Phone = "FakePhone",
                              PostalCode = "FakePostalCode",
                              AddressLine1 = "FakeStreet",
                              Version = 0
                          };
            Assert.IsTrue(address.IsValid, "Should be valid");
        }

        [TestMethod]
        public void ValidationFails()
        {
            address = new Address();
            Assert.IsFalse(address.IsValid, "Should be invalid");
        }

        #endregion
    }
}