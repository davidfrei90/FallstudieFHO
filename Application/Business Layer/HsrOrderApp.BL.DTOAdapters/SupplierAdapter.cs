#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DTOAdapters.Helper;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.BL.BusinessComponents;

#endregion#region


namespace HsrOrderApp.BL.DtoAdapters
{
    public class SupplierAdapter
    {
        #region SupplierConditionToDTO

        public static IList<SupplierListDTO> SuppliersToDtos(IQueryable<Supplier> suppliers)
        {
            IQueryable<SupplierListDTO> supplierDtos = from s in suppliers
                select new SupplierListDTO()
                {
                    SupplierId = s.SupplierId,
                    AccountNumber = s.AccountNumber,
                    Name = s.Name,
                    CreditRating = s.CreditRating,
                    PreferredSupplierFlag = s.PreferredSupplierFlag,
                    ActiveFlag = s.ActiveFlag,
                    PurchasingWebServiceURL = s.PurchasingWebServiceURL
                };

            return supplierDtos.ToList();

        }


        public static SupplierDTO SupplierToDto(Supplier s)
        {
            SupplierDTO dto = new SupplierDTO()
            {
                Id = s.SupplierId,
                AccountNumber = s.AccountNumber,
                Name = s.Name,
                CreditRating = s.CreditRating,
                PreferredSupplierFlag = s.PreferredSupplierFlag,
                ActiveFlag = s.ActiveFlag,
                PurchasingWebServiceURL = s.PurchasingWebServiceURL,
                Version = s.Version,
                Addresses = AddressAdapter.AddressToDtos(s.Addresses)
            };

            return dto;
        }

        #endregion

        #region private helpers

        private static int GetNumberOfSupplierConditionsOfSuppliers(Supplier supplier)
        {
            if (supplier.SupplierConditions == null)
            {
                return 0;
            }

            return supplier.SupplierConditions.Count();
        }

        #endregion



        #region DTOToSupplier


        public static Supplier DtoToSupplier(SupplierDTO dto)
        {
            Supplier supplier = new Supplier()
            {
                SupplierId = dto.Id,
                AccountNumber = dto.AccountNumber,
                Name = dto.Name,
                CreditRating = dto.CreditRating,
                PreferredSupplierFlag = dto.PreferredSupplierFlag,
                ActiveFlag = dto.ActiveFlag,
                PurchasingWebServiceURL = dto.PurchasingWebServiceURL,
                Version = dto.Version
            };
            ValidationHelper.Validate(supplier);
            return supplier;
        }

        public static IEnumerable<ChangeItem> GetChangeItems(SupplierDTO dto, Supplier supplier)
        {
            IEnumerable<ChangeItem> changeItems = from c in dto.Changes
                select
                    new ChangeItem(c.ChangeType,
                        AddressAdapter.DtoToAddress((AddressDTO) c.Object));
            return changeItems;
        }

        #endregion


    }


}