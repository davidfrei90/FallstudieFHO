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
    /// Summary description for OrderAdapterTestCase
    /// </summary>
    [TestClass]
    public class OrderAdapterTestCase
    {
        private TestContext testContextInstance;

        public OrderAdapterTestCase()
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
        public void TestOrdersToDtos()
        {
            Customer customer = new Customer() {CustomerId = 213, FirstName = "FakeFirstName", Name = "FakeName"};
            Order order = new Order() {OrderId = 1, OrderStatus = OrderStatus.Ordered, OrderDate = DateTime.Now, ShippedDate = null, Customer = customer, Version = 0};
            Assert.AreEqual(true, order.IsValid);

            IQueryable<Order> orders = new List<Order>() {order}.AsQueryable();
            IList<OrderListDTO> orderDtos = OrderAdapter.OrdersToListDtos(orders);
            Assert.AreEqual<int>(1, orderDtos.Count());

            OrderListDTO dto = orderDtos.First();
            Assert.AreEqual<int>(order.OrderId, dto.Id);
            Assert.AreEqual<int>((int) order.OrderStatus, (int) dto.OrderStatus);
            Assert.AreEqual<DateTime?>(order.OrderDate, dto.OrderDate);
            Assert.AreEqual<DateTime?>(order.ShippedDate, dto.ShippedDate);
            Assert.AreEqual<string>(order.Customer.ToString(), dto.CustomerName);

            Assert.AreEqual(true, dto.IsValid);
        }

        [TestMethod]
        public void TestOrderToDto()
        {
            Customer customer = new Customer() {CustomerId = 1, Name = "FakeUserName", FirstName = "FakeFirstName", Version = 0};
            Order order = new Order() {OrderId = 1, OrderStatus = OrderStatus.Ordered, OrderDate = DateTime.Now, ShippedDate = null, Customer = customer, Version = 0};

            OrderDetail detail = new OrderDetail() {OrderDetailId = 1, Order = order, QuantityInUnits = 123, UnitPrice = 213.43m, Version = 0};
            Product product = new Product() {ProductId = 123, Name = "FakeProductName", Category = "FakeCategory", QuantityPerUnit = 213, ListUnitPrice = 123, Version = 0};
            detail.Product = product;
            order.OrderDetails = new List<OrderDetail>() {detail}.AsQueryable();

            Assert.AreEqual(true, order.IsValid);
            Assert.AreEqual(true, detail.IsValid);

            OrderDTO dto = OrderAdapter.OrderToDto(order);
            Assert.AreEqual<int>(order.OrderId, dto.Id);
            Assert.AreEqual<int>((int) order.OrderStatus, (int) dto.OrderStatus);
            Assert.AreEqual<DateTime?>(order.OrderDate, dto.OrderDate);
            Assert.AreEqual<DateTime?>(order.ShippedDate, dto.ShippedDate);
            Assert.AreEqual<int>(order.Customer.CustomerId, dto.CustomerId);
            Assert.AreEqual<string>(order.Customer.ToString(), dto.CustomerName);
            Assert.AreEqual(order.Version, dto.Version);

            Assert.AreEqual<int>(1, dto.Details.Count());
            OrderDetailDTO dtodetail = dto.Details.First();

            Assert.AreEqual<int>(detail.OrderDetailId, dtodetail.Id);
            Assert.AreEqual<double>(detail.QuantityInUnits, dtodetail.QuantityInUnits);
            Assert.AreEqual<decimal>(detail.UnitPrice, dtodetail.UnitPrice);
            Assert.AreEqual(detail.Version, dtodetail.Version);

            Assert.AreEqual<int>(detail.Product.ProductId, dtodetail.ProductId);
            Assert.AreEqual<string>(detail.Product.Name, dtodetail.ProductName);

            Assert.AreEqual(true, dto.IsValid);
            Assert.AreEqual(true, dtodetail.IsValid);
        }

        [TestMethod]
        public void TestDtoToOrder()
        {
            OrderDetailDTO detailDto = new OrderDetailDTO() { Id = 1, QuantityInUnits = 123, UnitPrice = 213, ProductId = 1, ProductName = "FAkeProduct", Version = 1 };
            OrderDTO dto = new OrderDTO() { Id = 1, OrderStatus = OrderStatus.Ordered, OrderDate = DateTime.Now, ShippedDate = null, CustomerId = 123, CustomerName = "Fakecustomer" };
            dto.Details.Add(detailDto);
            Assert.AreEqual(true, dto.IsValid);
            Assert.AreEqual(true, detailDto.IsValid);

            Order order = OrderAdapter.DtoToOrder(dto);
            Assert.AreEqual<int>(dto.Id, order.OrderId);
            Assert.AreEqual<int>((int) dto.OrderStatus, (int) order.OrderStatus);
            Assert.AreEqual<DateTime?>(dto.OrderDate, order.OrderDate);
            Assert.AreEqual<DateTime?>(dto.ShippedDate, order.ShippedDate);
            Assert.AreEqual<int>(dto.CustomerId, order.Customer.CustomerId);
            //TODO: Assert.AreEqual<string>(dto.CustomerName, order.Customer.Username);
            Assert.AreEqual(dto.Version, order.Version);

            //TODO: Assert.AreEqual<int>(dto.Details.Count(), order.Details.Count());
            Assert.AreEqual(true, order.IsValid);
        }

        [TestMethod]
        public void TestDtoToDetail()
        {
            OrderDTO orderDTO = new OrderDTO();
            orderDTO.MarkChildForInsertion(new OrderDetailDTO { Id = 1, ProductId = 123, ProductName = "FakeProduct", QuantityInUnits = 123, UnitPrice = 123.32m, Version = 0 });
            orderDTO.MarkChildForUpdate(new OrderDetailDTO { Id = 2, ProductId = 123, ProductName = "FakeProduct", QuantityInUnits = 123, UnitPrice = 123.32m, Version = 0 });
            orderDTO.MarkChildForDeletion(new OrderDetailDTO { Id = 3, ProductId = 123, ProductName = "FakeProduct", QuantityInUnits = 123, UnitPrice = 123.32m, Version = 0 });

            IEnumerable<ChangeItem> changeItems = OrderAdapter.GetChangeItems(orderDTO, new Order());
            Assert.AreEqual<int>(3, changeItems.Count());
        }
    }
}