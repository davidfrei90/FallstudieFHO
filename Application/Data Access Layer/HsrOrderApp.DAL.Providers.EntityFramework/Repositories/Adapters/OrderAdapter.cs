#region

using System.Data.Objects.DataClasses;
using System.Linq;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters
{
    internal static class OrderAdapter
    {
        internal static BL.DomainModel.Order AdaptOrder(EntityReference<Order> o)
        {
            if (o.Value == null)
                return null;

            return AdaptOrder(o.Value);
        }

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
                                                 Customer = c ?? CustomerAdapter.AdaptCustomer(o.CustomerReference)
                                             };
            order.OrderDetails = AdaptOrderDetails(o.OrderDetails, order);
            return order;
        }

        internal static IQueryable<BL.DomainModel.Order> AdaptOrders(EntityCollection<Order> orderCollection, BL.DomainModel.Customer c)
        {
            if (orderCollection.IsLoaded == false)
            {
                return null;
            }
            var orders = from o in orderCollection.AsEnumerable()
                         select AdaptOrder(o, c);
            return orders.AsQueryable();
        }

        internal static IQueryable<BL.DomainModel.OrderDetail> AdaptOrderDetails(EntityCollection<OrderDetail> orderDetailCollection, BL.DomainModel.Order o)
        {
            if (orderDetailCollection.IsLoaded == false)
            {
                return null;
            }

            var orderDetails = from d in orderDetailCollection.AsEnumerable()
                               select AdaptOrderDetail(d, o);
            return orderDetails.AsQueryable();
        }

        internal static BL.DomainModel.OrderDetail AdaptOrderDetail(OrderDetail d)
        {
            return AdaptOrderDetail(d, null);
        }

        internal static BL.DomainModel.OrderDetail AdaptOrderDetail(OrderDetail d, BL.DomainModel.Order o)
        {
            BL.DomainModel.OrderDetail orderDetail = new BL.DomainModel.OrderDetail()
                                                         {
                                                             OrderDetailId = d.OrderDetailId,
                                                             QuantityInUnits = d.QuantityInUnits,
                                                             UnitPrice = d.UnitPrice,
                                                             Version = d.Version.ToUlong(),
                                                             Order = (o == null) ? AdaptOrder(d.OrderReference) : o,
                                                             Product = ProductAdapter.AdaptProduct(d.ProductReference)
                                                         };

            return orderDetail;
        }
    }
}