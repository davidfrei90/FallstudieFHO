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
    public class SupplierConditionBusinessComponent
    {
        private ISupplierConditionRepository rep;


        public SupplierConditionBusinessComponent()
        {
        }

        public SupplierConditionBusinessComponent(ISupplierConditionRepository unitOfWork)
        {
            this.rep = unitOfWork;
        }

        #region CRUD Operations

        public SupplierCondition GetSupplierConditionById(int supplierConditionId)
        {
            SupplierCondition supplierCondition = rep.GetById(supplierConditionId);
            return supplierCondition;
        }

        public IQueryable<SupplierCondition> GetSupplierConditionsByCriteria(SupplierConditionSearchType searchType, int supplierId)
        {
            IQueryable<SupplierCondition> supplierConditions = null;

            switch (searchType)
            {
                case SupplierConditionSearchType.None:
                    supplierConditions = rep.GetAll();
                    break;
                case SupplierConditionSearchType.BySupplier:
                    supplierConditions = rep.GetAll().Where(cu => cu.Supplier.SupplierId == supplierId);
                    break;
            }

            return supplierConditions;
        }

        public int StoreSupplierCondition(SupplierCondition supplierCondition) //, IEnumerable<ChangeItem> changeItems)
        {
            int supplierConditionId = default(int);
            using (TransactionScope transaction = new TransactionScope())
            {
                supplierConditionId = rep.SaveSupplierCondition(supplierCondition);
                //foreach (ChangeItem change in changeItems)
                //{
                //    if (change.Object is SupplierConditionDetail)
                //    {
                //        OrderDetail detail = (OrderDetail) change.Object;
                //        switch (change.ChangeType)
                //        {
                //            case ChangeType.ChildInsert:
                //            case ChangeType.ChildUpate:
                //                rep.SaveOrderDetail(detail, order);
                //                break;
                //            case ChangeType.ChildDelete:
                //                rep.DeleteOrderDetail(detail.OrderDetailId);
                //                break;
                //        }
                //    }
                //}

                //if (ConfigurationHelper.UseMsmqService && order.OrderStatus == OrderStatus.Draft)
                //{
                //    OrderToShipDTO dto = new OrderToShipDTO();
                //    dto.Id = order.OrderId;
                //    DistributionSystemServiceClient client = new DistributionSystemServiceClient();
                //    client.ShipOrder(dto);
                //    order = rep.GetById(order.OrderId);
                //    order.OrderStatus = OrderStatus.Ordered;
                //    rep.SaveOrder(order, false);
                //}
                transaction.Complete();
            }
            return supplierConditionId;
        }

        public void DeleteSupplierCondition(int supplierConditionId)
        {
            rep.DeleteSupplierCondition(supplierConditionId);
        }

        #endregion

        public ISupplierConditionRepository Repository
        {
            get { return this.rep; }
            set { this.rep = value; }
        }

        //public List<SupplierConditionDetail> GetAllSupplierConditionDetails()
        //{
        //    // Not as queriable due to problems with Linq2SQL implementation
        //    return rep.GetAllDetails().ToList();
        //}

        //public void SetOrderShipped(int orderId, DateTime shippedDate)
        //{
        //    // If a transaction exists already, this transaction scope will join it. 
        //    // Otherwise, it will create its own transaction.
        //    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
        //    {
        //        Order order = rep.GetById(orderId);
        //        if (!(order is MissingOrder))
        //        {
        //            order.ShippedDate = shippedDate;
        //            order.OrderStatus = OrderStatus.Shipped;
        //            rep.SaveOrder(order, false);

        //            ProductBusinessComponent productBC = DependencyInjectionHelper.GetProductBusinessComponent();
        //            foreach (OrderDetail detail in order.OrderDetails)
        //            {
        //                Product product = detail.Product;
        //                int unitsOnStock = product.UnitsOnStock - detail.QuantityInUnits;
        //                product.UnitsOnStock = unitsOnStock > 0 ? unitsOnStock : 0;
        //                productBC.StoreProduct(product);
        //            }
        //        }
        //        transaction.Complete();
        //    }
        //}
    }
}