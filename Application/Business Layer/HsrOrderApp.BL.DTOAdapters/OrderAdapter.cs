#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DTOAdapters.Helper;
using HsrOrderApp.SharedLibraries.DTO;

#endregion

namespace HsrOrderApp.BL.DtoAdapters
{
    public class OrderAdapter
    {
        #region OrderToDTO

        public static IList<OrderListDTO> OrdersToListDtos(IQueryable<Order> orders)
        {
            IQueryable<OrderListDTO> orderDTOs = from o in orders
                                                 select new OrderListDTO()
                                                            {
                                                                Id = o.OrderId,
                                                                CustomerName = o.Customer.ToString(),
                                                                OrderStatus = o.OrderStatus,
                                                                OrderDate = o.OrderDate,
                                                                ShippedDate = o.ShippedDate,
                                                            };

            return orderDTOs.ToList();
        }

        public static OrderDTO OrderToDto(Order o)
        {
            OrderDTO dto = new OrderDTO()
                               {
                                   Id = o.OrderId,
                                   CustomerName = o.Customer.ToString(),
                                   CustomerId = o.Customer.CustomerId,
                                   OrderStatus = o.OrderStatus,
                                   OrderDate = o.OrderDate,
                                   ShippedDate = o.ShippedDate,
                                   Version = o.Version,
                                   Details = OrderDetailsToDtos(o)
                               };

            return dto;
        }

        private static IList<OrderDetailDTO> OrderDetailsToDtos(Order order)
        {
            IQueryable<OrderDetailDTO> orderDetailDTO = from d in order.OrderDetails
                                                        select new OrderDetailDTO()
                                                                   {
                                                                       Id = d.OrderDetailId,
                                                                       QuantityInUnits = d.QuantityInUnits,
                                                                       UnitPrice = d.UnitPrice,
                                                                       Version = d.Version,
                                                                       ProductId = d.Product.ProductId,
                                                                       ProductName = d.Product.Name,
                                                                   };
            return orderDetailDTO.ToList();
        }

        #endregion

        #region DTOToOrder

        public static Order DtoToOrder(OrderDTO dto)
        {
            Order order = new Order()
                              {
                                  OrderId = dto.Id,
                                  OrderStatus = dto.OrderStatus,
                                  OrderDate = dto.OrderDate,
                                  ShippedDate = dto.ShippedDate,
                                  Customer = new Customer() {CustomerId = dto.CustomerId},
                                  Version = dto.Version
                              };
            ValidationHelper.Validate(order);
            return order;
        }

        public static IEnumerable<ChangeItem> GetChangeItems(OrderDTO dto, Order order)
        {
            IEnumerable<ChangeItem> changeItems = from c in dto.Changes
                                                  select
                                                      new ChangeItem(c.ChangeType,
                                                                     DtoToDetail((OrderDetailDTO) c.Object, order));
            return changeItems;
        }

        private static OrderDetail DtoToDetail(OrderDetailDTO dto, Order order)
        {
            OrderDetail detail = new OrderDetail()
                                     {
                                         OrderDetailId = dto.Id,
                                         QuantityInUnits = dto.QuantityInUnits,
                                         UnitPrice = dto.UnitPrice,
                                         Product = new Product() {ProductId = dto.ProductId},
                                         Version = dto.Version
                                     };
            detail.Order = order;
            ValidationHelper.Validate(detail);
            return detail;
        }

        #endregion
    }
}