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
    /// Summary description for CustomerBusinessComponentTestCase
    /// </summary>
    [TestClass]
    public class CustomerBusinessComponentTestCase
    {
        private ICustomerRepository context;
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
            context = mockBuilder.NewMock<ICustomerRepository>();
        }

        [TestMethod]
        public void TestGetCustomerById()
        {
            CustomerBusinessComponent service = new CustomerBusinessComponent(this.context);
            Customer customer = new Customer() {CustomerId = 123};

            Expect.Once.On(context).Method("GetById").Will(Return.Value(customer));
            Customer resultCustomer = service.GetCustomerById(123);
            Assert.AreEqual<decimal>(customer.CustomerId, resultCustomer.CustomerId);
            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void TestGetCustomerByCriteria()
        {
            CustomerBusinessComponent service = new CustomerBusinessComponent(this.context);
            Address address = new Address {AddressId = 12345, City = "FakeCity"};
            Customer customer = new Customer {CustomerId = 456, Name = "FakeName", Addresses = new List<Address> {address}.AsQueryable()};
            IList<Customer> customers = new List<Customer>();
            customers.Add(customer);

            foreach (CustomerSearchType type in Enum.GetValues(typeof (CustomerSearchType)))
            {
                Expect.Once.On(context).Method("GetAll").Will(Return.Value(customers.AsQueryable()));
                IQueryable<Customer> resultCustomers = service.GetCustomersByCriteria(type, "FakeCity", "FakeName");
                Assert.AreEqual<decimal>(1, resultCustomers.Count());
                Assert.AreEqual<decimal>(customer.CustomerId, resultCustomers.First().CustomerId);
            }

            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void TestStoreCustomer()
        {
            int customerId = 123;
            CustomerBusinessComponent service = new CustomerBusinessComponent(this.context);
            Customer customer = new Customer() {CustomerId = customerId};
            List<ChangeItem> changeItems = new List<ChangeItem>
                                               {
                                                   new ChangeItem(ChangeType.ChildInsert, new Address()),
                                                   new ChangeItem(ChangeType.ChildUpate, new Address()),
                                                   new ChangeItem(ChangeType.ChildDelete, new Address())
                                               };

            Expect.Once.On(context).Method("SaveCustomer").Will(Return.Value(customerId));
            Expect.Once.On(context).Method("SaveAddress").Will(Return.Value(1));
            Expect.Once.On(context).Method("SaveAddress").Will(Return.Value(1));
            Expect.Once.On(context).Method("DeleteAddress");
            int resultCustomerId = service.StoreCustomer(customer, changeItems);
            Assert.AreEqual<int>(customerId, resultCustomerId);

            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        public void TestDeleteCustomer()
        {
            CustomerBusinessComponent service = new CustomerBusinessComponent(this.context);

            Expect.Once.On(context).Method("DeleteCustomer").With(1);
            service.DeleteCustomer(1);
            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }
    }
}