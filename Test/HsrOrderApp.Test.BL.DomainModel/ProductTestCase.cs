#region

using HsrOrderApp.BL.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.Test.BL.DomainModel
{
    /// <summary>
    /// Summary description for ProductTestCase
    /// </summary>
    [TestClass]
    public class ProductTestCase
    {
        private Product product;

        [TestInitialize]
        public void SetUp()
        {
            product = new Product();
        }

        [TestCleanup]
        public void TearDown()
        {
            product = null;
        }

        #region ctors tests

        [TestMethod]
        public void Build()
        {
            product = new Product();
            Helper.AssertEmptiness(product, "ProductId", "Name", "Category", "ListUnitPrice", "QuantityPerUnit");
        }

        #endregion

        #region Property Assignment

        [TestMethod]
        public void PropsAssignments()
        {
            Helper.TestProperty<int>(123, "ProductId", product);
            Helper.TestProperty<string>("FakeProduct", "Name", product);
            Helper.TestProperty<string>("FakeCategory", "Category", product);
            Helper.TestProperty<decimal>(123.45m, "ListUnitPrice", product);
            Helper.TestProperty<double>(123.45, "QuantityPerUnit", product);
        }

        [TestMethod]
        public void Validation()
        {
            product = new Product()
                          {
                              ProductId = 1,
                              Category = "FakeCategory",
                              QuantityPerUnit = 100.2,
                              Name = "FakeProductName",
                              ListUnitPrice = 200.45m,
                              ProductNumber = "FakeProductNumber",
                              UnitsOnStock = 100,
                              Version = 0
                          };
            Assert.IsTrue(product.IsValid, "Should be valid");
        }

        [TestMethod]
        public void ValidationFails()
        {
            product = new Product();
            Assert.IsFalse(product.IsValid, "Should be invalid");
        }

        #endregion
    }
}