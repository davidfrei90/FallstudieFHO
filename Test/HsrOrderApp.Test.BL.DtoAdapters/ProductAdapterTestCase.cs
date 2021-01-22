#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DtoAdapters;
using HsrOrderApp.SharedLibraries.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.Test.BL.Services.DtoAdapters
{
    /// <summary>
    /// Summary description for ProductAdapterTestCase
    /// </summary>
    [TestClass]
    public class ProductAdapterTestCase
    {
        private TestContext testContextInstance;

        public ProductAdapterTestCase()
        {
            //
            // TODO: Add constructor logic here
            //
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

        [TestMethod]
        public void TestProductsToDtos()
        {
            Product product = new Product() {ProductId = 1, Name = "FakeProduct", Category = "FakeCategory", ListUnitPrice = (decimal) 123.21, QuantityPerUnit = 100.2, ProductNumber = "FakeProductNumber", UnitsOnStock = 10, Version = 0};
            Assert.AreEqual(true, product.IsValid);

            IQueryable<Product> products = new List<Product>() {product}.AsQueryable();
            IList<ProductDTO> productDtos = ProductAdapter.ProductsToDtos(products);
            Assert.AreEqual<int>(1, productDtos.Count());

            ProductDTO dto = productDtos.First();
            Assert.AreEqual<int>(product.ProductId, dto.Id);
            Assert.AreEqual<string>(product.Name, dto.Name);
            Assert.AreEqual<string>(product.Category, dto.Category);
            Assert.AreEqual<double>(product.QuantityPerUnit, dto.QuantityPerUnit);
            Assert.AreEqual<decimal>(product.ListUnitPrice, dto.ListUnitPrice);
            Assert.AreEqual(true, dto.IsValid);
        }

        [TestMethod]
        public void TestProductToDto()
        {
            Product product = new Product() { ProductId = 1, Name = "FakeProduct", Category = "FakeCategory", ListUnitPrice = (decimal)123.21, QuantityPerUnit = 100.2, ProductNumber = "FakeProductNumber", UnitsOnStock = 10, Version = 0 };
            Assert.AreEqual(true, product.IsValid);

            ProductDTO dto = ProductAdapter.ProductToDto(product);
            Assert.AreEqual<int>(product.ProductId, dto.Id);
            Assert.AreEqual<string>(product.Name, dto.Name);
            Assert.AreEqual<string>(product.Category, dto.Category);
            Assert.AreEqual(product.Version, dto.Version);
            Assert.AreEqual<double>(product.QuantityPerUnit, dto.QuantityPerUnit);
            Assert.AreEqual<decimal>(product.ListUnitPrice, dto.ListUnitPrice);
            Assert.AreEqual(true, dto.IsValid);
        }

        [TestMethod]
        public void TestDtoToProduct()
        {
            ProductDTO dto = new ProductDTO() { Id = 1, Name = "FakeProduct", Category = "FakeCategory", ListUnitPrice = (decimal)123.21, QuantityPerUnit = 100.2, ProductNumber = "FakeProductNumber", UnitsOnStock = 10, Version = 0 };
            Assert.AreEqual(true, dto.IsValid);

            Product product = ProductAdapter.DtoToProduct(dto);
            Assert.AreEqual<int>(product.ProductId, dto.Id);
            Assert.AreEqual<string>(product.Name, dto.Name);
            Assert.AreEqual<string>(product.Category, dto.Category);
            Assert.AreEqual(product.Version, dto.Version);
            Assert.AreEqual<double>(product.QuantityPerUnit, dto.QuantityPerUnit);
            Assert.AreEqual<decimal>(product.ListUnitPrice, dto.ListUnitPrice);
            Assert.AreEqual(true, dto.IsValid);
        }
    }
}