#region

using System.Data.Linq;
using System.Linq;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Adapters
{
    internal static class OrderAdapter
    {
        internal static BL.DomainModel.Order AdaptOrder(Order o)
        {
            return AdaptOrder(o, null);
        }

        internal static BL.DomainModel.Order AdaptOrder(Order o, BL.DomainModel.Customer c)
        {
            BL.DomainModel.Order order = new BL.DomainModel.Order()
                                             {
                                                 OrderId = o.OrderId,
                                                 OrderStatus = (OrderStatus) o.OrderStatus,
                                                 OrderDate = o.OrderDate,
                                                 ShippedDate = o.ShippedDate,
                                                 Version = o.Version.ToUlong(),
                                                 Customer = c ?? CustomerAdapter.AdaptCustomer(o.Customer)
                                             };
            order.OrderDetails = AdaptOrderDetails(o.OrderDetails, order);
            return order;
        }

        internal static IQueryable<BL.DomainModel.Order> AdaptOrders(EntitySet<Order> orderCollection, BL.DomainModel.Customer c)
        {
            var orders = from o in orderCollection.AsQueryable()
                         select AdaptOrder(o, c);
            return orders;
        }

        internal static IQueryable<BL.DomainModel.OrderDetail> AdaptOrderDetails(EntitySet<OrderDetail> orderDetailCollection, BL.DomainModel.Order o)
        {
            var orderDetails = from d in orderDetailCollection.AsQueryable()
                               select AdaptOrderDetail(d, o);
            return orderDetails;
        }

        internal static BL.DomainModel.OrderDetail AdaptOrderDetail(OrderDetail d, BL.DomainModel.Order order)
        {
            BL.DomainModel.OrderDetail orderDetail = AdaptOrderDetail(d);
            orderDetail.Order = order ?? AdaptOrder(d.Order);
            return orderDetail;
        }

        internal static BL.DomainModel.OrderDetail AdaptOrderDetail(OrderDetail d)
        {
            BL.DomainModel.OrderDetail orderDetail = new BL.DomainModel.OrderDetail()
                                                         {
                                                             OrderDetailId = d.OrderDetailId,
                                                             QuantityInUnits = d.QuantityInUnits,
                                                             UnitPrice = d.UnitPrice,
                                                             Version = d.Version.ToUlong(),
                                                             Product = ProductAdapter.AdaptProduct(d.Product)
                                                         };

            return orderDetail;
        }
    }
}