#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DtoAdapters;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HsrOrderApp.Test.BL.Services.DtoAdapters
{
    /// <summary>
    /// Summary description for CustomerAdapterTestCase
    /// </summary>
    [TestClass]
    public class CustomerAdapterTestCase
    {
        private TestContext testContextInstance;

        public CustomerAdapterTestCase()
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
        public void TestCustomersToDtos()
        {
            Customer customer = new Customer() {CustomerId = 1, Name = "FakeUserName", FirstName = "FakeFirstName", Version = 0};
            Order order1 = new Order() {OrderId = 1, OrderStatus = OrderStatus.Draft};
            Order order2 = new Order() {OrderId = 2, OrderStatus = OrderStatus.Ordered};
            customer.Orders = new List<Order>() {order1, order2}.AsQueryable();
            Assert.AreEqual(true, customer.IsValid);

            IQueryable<Customer> customers = new List<Customer>() {customer}.AsQueryable();
            IList<CustomerListDTO> customerDtos = CustomerAdapter.CustomersToDtos(customers);
            Assert.AreEqual<int>(1, customerDtos.Count());

            CustomerListDTO dto = customerDtos.First();
            Assert.AreEqual<int>(customer.CustomerId, dto.Id);
            Assert.AreEqual<string>(customer.Name, dto.Name);
            Assert.AreEqual<string>(customer.FirstName, dto.FirstName);
            Assert.AreEqual<int>(2, dto.NumberOfTotalOrders);
            Assert.AreEqual<int>(1, dto.NumberOfOpenOrders);
            Assert.AreEqual(true, dto.IsValid);
        }

        [TestMethod]
        public void TestCustomerToDto()
        {
            Customer customer = new Customer() {CustomerId = 1, Name = "FakeUserName", FirstName = "FakeFirstName", Version = 0};
            Address address = new Address() {AddressId = 1, AddressLine1 = "FakeStreet", PostalCode = "FakePostalCode", City = "FakeCity", Phone = "FakePhone", Email = "FakeEmail", Version = 0};
            customer.Addresses = new List<Address>() {address}.AsQueryable();
            Assert.AreEqual(true, customer.IsValid);
            Assert.AreEqual(true, address.IsValid);

            CustomerDTO dto = CustomerAdapter.CustomerToDto(customer);
            Assert.AreEqual<int>(customer.CustomerId, dto.Id);
            Assert.AreEqual<string>(customer.Name, dto.Name);
            Assert.AreEqual<string>(customer.FirstName, dto.FirstName);
            Assert.AreEqual(customer.Version, dto.Version);
            Assert.AreEqual<int>(1, dto.Addresses.Count());

            AddressDTO dtoAddress = dto.Addresses.First();
            Assert.AreEqual<int>(address.AddressId, dtoAddress.Id);
            Assert.AreEqual<String>(address.AddressLine1, dtoAddress.AddressLine1);
            Assert.AreEqual<String>(address.City, dtoAddress.City);
            Assert.AreEqual<string>(address.PostalCode, dtoAddress.PostalCode);
            Assert.AreEqual<string>(address.Phone, dtoAddress.Phone);
            Assert.AreEqual<string>(address.Email, dtoAddress.Email);
            Assert.AreEqual(address.Version, dtoAddress.Version);
            Assert.AreEqual(true, dto.IsValid);
            Assert.AreEqual(true, dtoAddress.IsValid);
        }


        [TestMethod]
        public void TestDtoToCustomer()
        {
            AddressDTO addressDto = new AddressDTO() { Id = 1, AddressLine1 = "FakeStreet", PostalCode = "FakePostalCode", City = "FakeCity", Phone = "FakePhone", Email = "FakeEmail", Version = 0 };
            CustomerDTO dto = new CustomerDTO() { Id = 1, Name = "FakeUsername", FirstName = "FakeFirstname", Version = 1 };
            dto.Addresses.Add(addressDto);
            Assert.AreEqual(true, dto.IsValid);
            Assert.AreEqual(true, addressDto.IsValid);

            Customer customer = CustomerAdapter.DtoToCustomer(dto);
            Assert.AreEqual<int>(dto.Id, customer.CustomerId);
            Assert.AreEqual<string>(dto.Name, customer.Name);
            Assert.AreEqual<string>(dto.FirstName, customer.FirstName);
            Assert.AreEqual(dto.Version, customer.Version);
            Assert.AreEqual(true, customer.IsValid);

            //Assert.AreEqual<int>(dto.Addresses.Count(), customer.Addresses.Count());
            //Assert.AreEqual(true, address.IsValid);
        }

        [TestMethod]
        public void TestDtoToAddress()
        {
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.MarkChildForInsertion(new AddressDTO { Id = 1, City = "FakeCity", Email = "FakeEmail", Phone = "09", PostalCode = "1234", AddressLine1 = "FakeStreet", Version = 0 });
            customerDTO.MarkChildForUpdate(new AddressDTO { Id = 2, City = "FakeCity", Email = "FakeEmail", Phone = "09", PostalCode = "1234", AddressLine1 = "FakeStreet", Version = 0 });
            customerDTO.MarkChildForDeletion(new AddressDTO { Id = 3, City = "FakeCity", Email = "FakeEmail", Phone = "09", PostalCode = "1234", AddressLine1 = "FakeStreet", Version = 0 });

            IEnumerable<ChangeItem> changeItems = CustomerAdapter.GetChangeItems(customerDTO, new Customer());
            Assert.AreEqual<int>(3, changeItems.Count());
        }
    }
}