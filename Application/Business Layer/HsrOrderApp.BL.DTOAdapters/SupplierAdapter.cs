#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DTOAdapters.Helper;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.BL.DtoAdapters
{
    public class SupplierAdapter
    {
        #region SupplierToDTO

        public static IList<SupplierListDTO> SuppliersToDtos(IQueryable<Supplier> suppliers)
        {
            IQueryable<SupplierListDTO> supplierDtos = from s in suppliers
                                                       select new SupplierListDTO()
                                                       {
                                                           Id = s.SupplierId,
                                                           AccountNumber = s.AccountNumber,
                                                           CreditRating = s.CreditRating,
                                                           PreferredSupplierFlag = s.PreferredSupplierFlag,
                                                           ActiveFlag = s.ActiveFlag,
                                                           PurchasingWebServiceURL = s.PurchasingWebServiceURL,
                                                           Name = s.Name,
                                                       };
            return supplierDtos.ToList();
        }

        public static SupplierDTO SupplierToDto(Supplier s)
        {
            SupplierDTO dto = new SupplierDTO()
            {
                Id = s.SupplierId,
                AccountNumber = s.AccountNumber,
                CreditRating = s.CreditRating,
                PreferredSupplierFlag = s.PreferredSupplierFlag,
                ActiveFlag = s.ActiveFlag,
                PurchasingWebServiceURL = s.PurchasingWebServiceURL,
                Name = s.Name,
                Version = s.Version,
                Addresses = AddressAdapter.AddressToDtos(s.Addresses)
            };

            return dto;
        }

        #region private helpers

        #endregion

        #endregion

        #region DTOToSupplier

        public static Supplier DtoToSupplier(SupplierDTO dto)
        {
            Supplier supplier = new Supplier()
            {
                SupplierId = dto.Id,
                Name = dto.Name,
                AccountNumber = dto.AccountNumber,
                CreditRating = dto.CreditRating,
                PreferredSupplierFlag = dto.PreferredSupplierFlag,
                ActiveFlag = dto.ActiveFlag,
                PurchasingWebServiceURL = dto.PurchasingWebServiceURL,
                Version = dto.Version
            };
            ValidationHelper.Validate(supplier);
            return supplier;
        }

        #endregion
    }
}