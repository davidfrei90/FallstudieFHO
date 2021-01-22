#region

using System;
using System.Data.Linq;
using System.Linq;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.LinqToSql.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Repositories
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        public OrderRepository(HsrOrderAppDataContext db) : base(db)
        {
        }

        public OrderRepository(string connectionString) : base(connectionString)
        {
        }

        public IQueryable<HsrOrderApp.BL.DomainModel.Order> GetAll()
        {
            var orders = from o in this.db.Orders
                         select OrderAdapter.AdaptOrder(o);

            return orders;
        }

        public HsrOrderApp.BL.DomainModel.Order GetById(int id)
        {
            try
            {
                var orders = from o in this.db.Orders
                             where o.OrderId == id
                             select OrderAdapter.AdaptOrder(o);

                return orders.First();
            }
            catch (Exception ex)
            {
                //if (ExceptionPolicy.HandleException(ex, "MissingEntity")) throw;
                return new MissingOrder();
            }
        }

        public IQueryable<HsrOrderApp.BL.DomainModel.OrderDetail> GetAllDetails()
        {
            var orderDetails = from od in this.db.OrderDetails
                               select OrderAdapter.AdaptOrderDetail(od);

            return orderDetails;
        }

        public int SaveOrder(HsrOrderApp.BL.DomainModel.Order order)
        {
            return SaveOrder(order, true);
        }

        public int SaveOrder(HsrOrderApp.BL.DomainModel.Order order, bool detached)
        {
            try
            {
                Order dbOrder = new Order();
                if (detached == false)
                {
                    dbOrder = db.Orders.First(o => o.OrderId == order.OrderId);
                }
                bool isNew = false;
                if (order.OrderId == default(int) || order.OrderId <= 0)
                    isNew = true;

                dbOrder.OrderId = order.OrderId;
                dbOrder.Version = order.Version.ToTimestamp();
                dbOrder.OrderStatus = (int) order.OrderStatus;
                dbOrder.OrderDate = order.OrderDate;
                dbOrder.ShippedDate = order.ShippedDate;
                dbOrder.CustomerId = order.Customer.CustomerId;

                if (isNew)
                {
                    db.Orders.InsertOnSubmit(dbOrder);
                }
                else if (detached)
                {
                    db.Orders.Attach(dbOrder, true);
                }

                db.SubmitChanges();
                order.OrderId = dbOrder.OrderId;
                return dbOrder.OrderId;
            }
            catch (ChangeConflictException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }


        public int SaveOrderDetail(HsrOrderApp.BL.DomainModel.OrderDetail detail, HsrOrderApp.BL.DomainModel.Order forThisOrder)
        {
            try
            {
                OrderDetail dbOrderDetail = new OrderDetail();
                bool isNew = false;
                if (detail.OrderDetailId == default(int) || detail.OrderDetailId <= 0)
                {
                    isNew = true;
                }

                dbOrderDetail.OrderDetailId = detail.OrderDetailId;
                dbOrderDetail.Version = detail.Version.ToTimestamp();
                dbOrderDetail.UnitPrice = detail.UnitPrice;
                dbOrderDetail.QuantityInUnits = detail.QuantityInUnits;
                dbOrderDetail.ProductId = detail.Product.ProductId;
                dbOrderDetail.OrderId = forThisOrder.OrderId;

                if (isNew)
                    db.OrderDetails.InsertOnSubmit(dbOrderDetail);
                else
                    db.OrderDetails.Attach(dbOrderDetail, true);
                db.SubmitChanges();
                detail.OrderDetailId = dbOrderDetail.OrderDetailId;
                return dbOrderDetail.OrderDetailId;
            }
            catch (ChangeConflictException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }


        public void DeleteOrder(int id)
        {
            Order cu = db.Orders.FirstOrDefault(c => c.OrderId == id);
            if (cu != null)
            {
                db.Orders.DeleteOnSubmit(cu);
                db.SubmitChanges();
            }
        }

        public void DeleteOrderDetail(int id)
        {
            OrderDetail cu = db.OrderDetails.FirstOrDefault(c => c.OrderDetailId == id);
            if (cu != null)
            {
                db.OrderDetails.DeleteOnSubmit(cu);
                db.SubmitChanges();
            }
        }
    }
}