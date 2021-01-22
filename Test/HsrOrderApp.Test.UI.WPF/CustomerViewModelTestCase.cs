#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Address;
using HsrOrderApp.UI.WPF.ViewModels.Customer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock2;

#endregion

namespace HsrOrderApp.Test.UI.WPF
{
    /// <summary>
    /// Summary description for CustomerViewModelTestCase
    /// </summary>
    [TestClass]
    public class CustomerViewModelTestCase
    {
        private Mockery mockBuilder;
        private IServiceFacade serviceFacade;

        private TestContext testContextInstance;

        public CustomerViewModelTestCase()
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
        public void TestCustomerViewModel()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.Service = serviceFacade;
            CustomerListDTO customer = new CustomerListDTO() {Id = 1};
            IList<CustomerListDTO> customers = new List<CustomerListDTO>() {customer};
            Expect.Once.On(serviceFacade).Method("GetAllCustomers").Will(Return.Value(customers));
            customerViewModel.LoadCommand.Command.Execute(null);

            Assert.AreEqual<int>(1, customerViewModel.Items.Count);
            Assert.AreEqual(customer, customerViewModel.SelectedItem);
            Assert.AreEqual(Strings.CustomerViewModel_DisplayName, customerViewModel.DisplayName);
        }

        [TestMethod]
        public void TestCustomerDetailModel()
        {
            AddressDTO addressDto = new AddressDTO() {Id = 1, AddressLine1 = "FakeStreet", PostalCode = "FakePostalCode", City = "FakeCity", Phone = "FakePhone", Email = "FakeEmail", Version = 0};
            CustomerDTO dto = new CustomerDTO() {Id = 1, Name = "FakeUsername", FirstName = "FakeFirstname", Version = 1};
            dto.Addresses.Add(addressDto);

            CustomerDetailViewModel customerDetailViewModel = new CustomerDetailViewModel(dto, false);
            customerDetailViewModel.Service = serviceFacade;

            Expect.Once.On(serviceFacade).Method("StoreCustomer").With(dto);
            customerDetailViewModel.SaveCommand.Command.Execute(null);

            Assert.AreEqual(dto, customerDetailViewModel.Model);
            Assert.AreEqual(Strings.CustomerDetailViewModel_DisplayName, customerDetailViewModel.DisplayName);

            AddressViewModel addressViewModel = (AddressViewModel) customerDetailViewModel.ListViewModel;
            Assert.AreEqual(addressDto, addressViewModel.Items.FirstOrDefault());
        }

        [TestMethod]
        public void TestAddressDetailViewModel()
        {
            AddressDTO dto = new AddressDTO() {Id = 1, AddressLine1 = "FakeStreet", PostalCode = "FakePostalCode", City = "FakeCity", Phone = "FakePhone", Email = "FakeEmail", Version = 0};
            AddressDetailViewModel viewModel = new AddressDetailViewModel(dto, false);
            Assert.AreEqual(dto, viewModel.Model);
            Assert.AreEqual(Strings.AddressDetailViewModel_DisplayName, viewModel.DisplayName);
        }
    }
}