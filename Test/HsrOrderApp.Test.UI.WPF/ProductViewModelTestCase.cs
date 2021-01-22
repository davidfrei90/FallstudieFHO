#region

using System.Collections.Generic;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock2;

#endregion

namespace HsrOrderApp.Test.UI.WPF
{
    /// <summary>
    /// Summary description for ProductViewModelTestCase
    /// </summary>
    [TestClass]
    public class ProductViewModelTestCase
    {
        private Mockery mockBuilder;
        private IServiceFacade serviceFacade;

        private TestContext testContextInstance;

        public ProductViewModelTestCase()
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
        public void TestProductViewModel()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Service = serviceFacade;
            ProductDTO product = new ProductDTO() {Id = 1};
            IList<ProductDTO> products = new List<ProductDTO>() {product};
            Expect.Once.On(serviceFacade).Method("GetAllProducts").Will(Return.Value(products));
            productViewModel.LoadCommand.Command.Execute(null);

            Assert.AreEqual<int>(1, productViewModel.Items.Count);
            Assert.AreEqual(product, productViewModel.SelectedItem);
            Assert.AreEqual(Strings.ProductViewModel_DisplayName, productViewModel.DisplayName);
        }

        [TestMethod]
        public void TestProductDetailModel()
        {
            ProductDTO dto = new ProductDTO() {Id = 1, Name = "FakeProduct", Category = "FakeCategory", ListUnitPrice = 123.21m, QuantityPerUnit = 100.2, Version = 1};

            ProductDetailViewModel productDetailViewModel = new ProductDetailViewModel(dto, false);
            productDetailViewModel.Service = serviceFacade;

            Expect.Once.On(serviceFacade).Method("StoreProduct").With(dto).Will(Return.Value(1));
            productDetailViewModel.SaveCommand.Command.Execute(null);

            Assert.AreEqual(dto, productDetailViewModel.Model);
            Assert.AreEqual(Strings.ProductDetailViewModel_DisplayName, productDetailViewModel.DisplayName);
        }
    }
}