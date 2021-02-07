#region

using System.Data.Objects.DataClasses;
using System.Linq;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters
{
    internal static class SupplierConditionAdapter
    {
        internal static BL.DomainModel.SupplierCondition AdaptSupplierCondition(EntityReference<SupplierCondition> o)
        {
            if (o.Value == null)
                return null;

            return AdaptSupplierCondition(o.Value);
        }

        internal static BL.DomainModel.SupplierCondition AdaptSupplierCondition(SupplierCondition o)
        {
            return AdaptSupplierCondition(o, null,null);
        }

        internal static BL.DomainModel.SupplierCondition AdaptSupplierCondition(SupplierCondition o, BL.DomainModel.Supplier c, BL.DomainModel.Product p)
        {
            BL.DomainModel.SupplierCondition supplierCondition = new BL.DomainModel.SupplierCondition()
                                             {
                                                 SupplierConditionId = o.SupplierConditionId,
                                                 StandardPrice = o.StandardPrice,
                                                 LastReceiptDate = o.LastReceiptDate,
                                                 LastReceiptCost = o.LastReceiptCost,
                                                 MinOrderQty = o.MinOrderQty,
                                                 MaxOrderQty = o.MaxOrderQty,
                                                 Version = o.Version.ToUlong(),
                                                 Supplier = c ?? SupplierAdapter.AdaptSupplier(o.SuppliersReference),
                                                 Product = p??ProductAdapter.AdaptProduct(o.ProductsReference),
        };
            return supplierCondition;
        }

        internal static IQueryable<BL.DomainModel.SupplierCondition> AdaptSupplierConditions(EntityCollection<SupplierCondition> supplierConditionCollection, BL.DomainModel.Supplier c, BL.DomainModel.Product p)
        {
            if (supplierConditionCollection.IsLoaded == false)
            {
                return null;
            }
            var supplierConditions = from o in supplierConditionCollection.AsEnumerable()
                         select AdaptSupplierCondition(o, c,p);
            return supplierConditions.AsQueryable();
        }

       
    }
}