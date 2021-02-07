#region

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
            return AdaptSupplier(s.Value);
        }



        internal static BL.DomainModel.Supplier AdaptSupplier(Supplier s)
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


            supplier.SupplierConditions = SupplierConditionAdapter.AdaptSupplierConditions(s.SupplierConditions, supplier,null);
            supplier.Addresses = AddressAdapter.AdaptAddresses(s.Addresses);
            return supplier;
        }
    }
}