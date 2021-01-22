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
    /// Summary description for ProductServiceTestCase
    /// </summary>
    [TestClass]
    public class ProductBusinessComponentTestCase
    {
        private IProductRepository context;
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
            context = mockBuilder.NewMock<IProductRepository>();
        }

        [TestMethod]
        public void TestGetProductById()
        {
            ProductBusinessComponent service = new ProductBusinessComponent(this.context);
            Product product = new Product() {ProductId = 123};

            Expect.Once.On(context).Method("GetById").Will(Return.Value(product));
            Product resultProduct = service.GetProductById(123);
            Assert.AreEqual<decimal>(product.ProductId, resultProduct.ProductId);
            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        public void TestGetProductByCriteria()
        {
            ProductBusinessComponent service = new ProductBusinessComponent(this.context);
            Product product = new Product() {ProductId = 456, Name = "FakeProduct", Category = "FakeCategory"};
            IList<Product> products = new List<Product>();
            products.Add(product);

            foreach (ProductSearchType type in Enum.GetValues(typeof (ProductSearchType)))
            {
                Expect.Once.On(context).Method("GetAll").Will(Return.Value(products.AsQueryable()));
                IQueryable<Product> resultProducts = service.GetProductsByCriteria(type, "FakeCategory", "FakeProduct");
                Assert.AreEqual<decimal>(1, resultProducts.Count());
                Assert.AreEqual<decimal>(product.ProductId, resultProducts.First().ProductId);
            }

            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void TestStoreProduct()
        {
            int productId = 123;
            ProductBusinessComponent service = new ProductBusinessComponent(this.context);
            Product product = new Product() {ProductId = 456, Name = "FakeProduct", Category = "FakeCategory"};

            Expect.Once.On(context).Method("SaveProduct").Will(Return.Value(productId));
            int resultProductId = service.StoreProduct(product);
            Assert.AreEqual<int>(productId, resultProductId);

            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        public void TestDeleteProduct()
        {
            ProductBusinessComponent service = new ProductBusinessComponent(this.context);

            Expect.Once.On(context).Method("DeleteProduct").With(1);
            service.DeleteProduct(1);
            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }
    }
}