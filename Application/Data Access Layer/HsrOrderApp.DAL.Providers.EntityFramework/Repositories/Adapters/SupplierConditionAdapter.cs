#region

using System.Data.Objects.DataClasses;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters
{
    internal static class SupplierConditionAdapter
    {
        internal static BL.DomainModel.SupplierCondition AdaptSupplierCondition(EntityReference<SupplierCondition> p)
        {
            if (p.Value == null)
                return null;
            return AdaptSupplierCondition(p.Value);
        }

        internal static BL.DomainModel.SupplierCondition AdaptSupplierCondition(SupplierCondition c)
        {
            BL.DomainModel.SupplierCondition supp = new BL.DomainModel.SupplierCondition()
                                                 {
                                                     SupplierConditionId = c.SupplierConditionId,
                                                     ProductId = c.ProductId,
                                                     SupplierId = c.SupplierId,
                                                     StandardPrice = c.StandardPrice,
                                                     LastReceiptCost = c.LastReceiptCost,
                                                     LastReceiptDate = c.LastReceiptDate,
                                                     MinOrderQty = c.MinOrderQty,
                                                     MaxOrderQty = c.MaxOrderQty,
                                                     Version = c.Version.ToUlong(),
                                                 };
            return supp;
        }
    }
}