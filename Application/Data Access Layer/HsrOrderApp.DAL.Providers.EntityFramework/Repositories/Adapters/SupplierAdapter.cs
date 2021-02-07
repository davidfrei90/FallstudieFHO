#region

using HsrOrderApp.BL.DomainModel;
using System.Data.Objects.DataClasses;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters
{
    internal static class SupplierAdapter
    {
        internal static BL.DomainModel.Supplier AdaptSupplier(EntityReference<Supplier> s)
        {
            if (s == null || s.Value == null)
                return null;
            return AdaptSupplier(s.Value, null);
        }

        internal static BL.DomainModel.Supplier AdaptSupplier(EntityReference<Supplier> s, BL.DomainModel.User user)
        {
            if (s == null || s.Value == null)
                return null;
            return AdaptSupplier(s.Value, user);
        }

        internal static BL.DomainModel.Supplier AdaptSupplier(Supplier s)
        {
            return AdaptSupplier(s, null);
        }

        internal static BL.DomainModel.Supplier AdaptSupplier(Supplier s, BL.DomainModel.User user)
        {
            BL.DomainModel.Supplier supplier = new BL.DomainModel.Supplier()
            {
                SupplierId = s.SupplierId,
                AccountNumber = s.AccountNumber,
                Name = s.Name,
                CreditRating = s.CreditRating,
                PreferredSupplierFlag = s.PreferredSupplierFlag,
                ActiveFlag = s.ActiveFlag,
                PurchasingWebServiceURL = s.PurchasingWebServiceURL,
                Version = s.Version.ToUlong(),
            };
            //supplier.SupplierCondition = SupplierConditionAdapter.AdaptSupplierConditions(s.Orders, supplier);
            supplier.Addresses = AddressAdapter.AdaptAddresses(s.Addresses);
            return supplier;
        }
    }
}