#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using HsrOrderApp.BL.BusinessComponents.DependencyInjection;
using HsrOrderApp.BL.BusinessComponents.DistributionSystemService;
using HsrOrderApp.BL.Core.Helpers;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.BL.BusinessComponents
{
    public class OrderBusinessComponent
    {
        private IOrderRepository rep;


        public OrderBusinessComponent()
        {
        }

        public OrderBusinessComponent(IOrderRepository unitOfWork)
        {
            this.rep = unitOfWork;
        }

        #region CRUD Operations

        public Order GetOrderById(int orderId)
        {
            Order order = rep.GetById(orderId);
            return order;
        }

        public IQueryable<Order> GetOrdersByCriteria(OrderSearchType searchType, int customerId)
        {
            IQueryable<Order> orders = null;

            switch (searchType)
            {
                case OrderSearchType.None:
                    orders = rep.GetAll();
                    break;
                case OrderSearchType.ByCustomer:
                    orders = rep.GetAll().Where(cu => cu.Customer.CustomerId == customerId);
                    break;
            }

            return orders;
        }

        public int StoreOrder(Order order, IEnumerable<ChangeItem> changeItems)
        {
            int orderId = default(int);
            using (TransactionScope transaction = new TransactionScope())
            {
                orderId = rep.SaveOrder(order);
                foreach (ChangeItem change in changeItems)
                {
                    if (change.Object is OrderDetail)
                    {
                        OrderDetail detail = (OrderDetail) change.Object;
                        switch (change.ChangeType)
                        {
                            case ChangeType.ChildInsert:
                            case ChangeType.ChildUpate:
                                rep.SaveOrderDetail(detail, order);
                                break;
                            case ChangeType.ChildDelete:
                                rep.DeleteOrderDetail(detail.OrderDetailId);
                                break;
                        }
                    }
                }

                if (ConfigurationHelper.UseMsmqService && order.OrderStatus == OrderStatus.Draft)
                {
                    OrderToShipDTO dto = new OrderToShipDTO();
                    dto.Id = order.OrderId;
                    DistributionSystemServiceClient client = new DistributionSystemServiceClient();
                    client.ShipOrder(dto);
                    order = rep.GetById(order.OrderId);
                    order.OrderStatus = OrderStatus.Ordered;
                    rep.SaveOrder(order, false);
                }
                transaction.Complete();
            }
            return orderId;
        }

        public void DeleteOrder(int orderId)
        {
            rep.DeleteOrder(orderId);
        }

        #endregion

        public IOrderRepository Repository
        {
            get { return this.rep; }
            set { this.rep = value; }
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            // Not as queriable due to problems with Linq2SQL implementation
            return rep.GetAllDetails().ToList();
        }

        public void SetOrderShipped(int orderId, DateTime shippedDate)
        {
            // If a transaction exists already, this transaction scope will join it. 
            // Otherwise, it will create its own transaction.
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                Order order = rep.GetById(orderId);
                if (!(order is MissingOrder))
                {
                    order.ShippedDate = shippedDate;
                    order.OrderStatus = OrderStatus.Shipped;
                    rep.SaveOrder(order, false);

                    ProductBusinessComponent productBC = DependencyInjectionHelper.GetProductBusinessComponent();
                    foreach (OrderDetail detail in order.OrderDetails)
                    {
                        Product product = detail.Product;
                        int unitsOnStock = product.UnitsOnStock - detail.QuantityInUnits;
                        product.UnitsOnStock = unitsOnStock > 0 ? unitsOnStock : 0;
                        productBC.StoreProduct(product);
                    }
                }
                transaction.Complete();
            }
        }
    }
}