#region

using System;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Linq;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        public OrderRepository(HsrOrderAppEntities db)
            : base(db)
        {
        }

        public OrderRepository(string connectionString) : base(connectionString)
        {
        }

        public OrderRepository() : base()
        {
        }

        public IQueryable<HsrOrderApp.BL.DomainModel.Order> GetAll()
        {
            var orders = from o in this.db.OrderSet.Include("Customer").AsEnumerable()
                         select OrderAdapter.AdaptOrder(o);

            return orders.AsQueryable();
        }

        public HsrOrderApp.BL.DomainModel.Order GetById(int id)
        {
            try
            {
                var orders = from o in this.db.OrderSet.Include("Customer").Include("OrderDetails.Product").AsEnumerable()
                             where o.OrderId == id
                             select OrderAdapter.AdaptOrder(o);

                return orders.First();
            }
            catch (ArgumentNullException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return new MissingOrder();
            }
        }

        public IQueryable<HsrOrderApp.BL.DomainModel.OrderDetail> GetAllDetails()
        {
            var orderDetails = from od in this.db.OrderDetailSet.Include("Product").Include("Order").AsEnumerable()
                               select OrderAdapter.AdaptOrderDetail(od);

            return orderDetails.AsQueryable();
        }

        public int SaveOrder(HsrOrderApp.BL.DomainModel.Order order)
        {
            return SaveOrder(order, true);
        }

        public int SaveOrder(HsrOrderApp.BL.DomainModel.Order order, bool detached)
        {
            try
            {
                CustomerRepository cr = new CustomerRepository(this.db);
                string setname = "OrderSet";

                bool isNew = false;
                Order dbOrder;
                if (detached == false)
                {
                    dbOrder = db.OrderSet.FirstOrDefault(o => o.OrderId == order.OrderId);
                }
                else if (order.OrderId == default(int) || order.OrderId <= 0)
                {
                    isNew = true;
                    dbOrder = new Order();
                }
                else
                {
                    dbOrder = new Order()
                                  {
                                      OrderId = order.OrderId,
                                      Version = order.Version.ToTimestamp(),
                                  };
                    dbOrder.EntityKey = db.CreateEntityKey(setname, dbOrder);
                    db.AttachTo(setname, dbOrder);
                }
                dbOrder.OrderStatus = (int) order.OrderStatus;
                dbOrder.OrderDate = order.OrderDate;
                dbOrder.ShippedDate = order.ShippedDate;

                if (isNew)
                {
                    dbOrder.CustomerReference.EntityKey = new EntityKey("HsrOrderAppEntities.CustomerSet", "CustomerId", order.Customer.CustomerId);
                    db.AddToOrderSet(dbOrder);
                }
                else
                {
                    dbOrder.CustomerReference.Attach(db.CustomerSet.First(c => c.CustomerId == order.Customer.CustomerId));
                }
                db.SaveChanges();
                order.OrderId = dbOrder.OrderId;
                return dbOrder.OrderId;
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public int SaveOrderDetail(HsrOrderApp.BL.DomainModel.OrderDetail detail, HsrOrderApp.BL.DomainModel.Order forThisOrder)
        {
            try
            {
                string setname = "OrderDetailSet";
                OrderDetail dbOrderDetail;
                bool isNew = false;
                if (detail.OrderDetailId == default(int) || detail.OrderDetailId <= 0)
                {
                    isNew = true;
                    dbOrderDetail = new OrderDetail();
                }
                else
                {
                    dbOrderDetail = new OrderDetail() {OrderDetailId = detail.OrderDetailId, Version = detail.Version.ToTimestamp()};
                    dbOrderDetail.EntityKey = db.CreateEntityKey(setname, dbOrderDetail);
                    db.AttachTo(setname, dbOrderDetail);
                }
                dbOrderDetail.UnitPrice = detail.UnitPrice;
                dbOrderDetail.QuantityInUnits = detail.QuantityInUnits;


                if (isNew)
                {
                    dbOrderDetail.OrderReference.EntityKey = new EntityKey("HsrOrderAppEntities.OrderSet", "OrderId", detail.Order.OrderId);
                    dbOrderDetail.ProductReference.EntityKey = new EntityKey("HsrOrderAppEntities.ProductSet", "ProductId", detail.Product.ProductId);
                    db.AddToOrderDetailSet(dbOrderDetail);
                }
                else
                {
                    if (dbOrderDetail.ProductReference.GetEnsureLoadedReference().Value.ProductId != detail.Product.ProductId)
                    {
                        Product newProduct = new Product {ProductId = detail.Product.ProductId};
                        db.AttachTo("ProductSet", newProduct);
                        dbOrderDetail.Product = newProduct;
                    }
                }
                db.SaveChanges();

                detail.OrderDetailId = dbOrderDetail.OrderDetailId;
                return dbOrderDetail.OrderDetailId;
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void DeleteOrder(int id)
        {
            // TODO: Delete OrderDetails first
            Order cu = db.OrderSet.First(c => c.OrderId == id);
            if (cu != null)
            {
                db.DeleteObject(cu);
                db.SaveChanges();
            }
        }

        public void DeleteOrderDetail(int id)
        {
            OrderDetail cu = db.OrderDetailSet.FirstOrDefault(c => c.OrderDetailId == id);
            if (cu != null)
            {
                db.DeleteObject(cu);
                db.SaveChanges();
            }
        }
    }
}