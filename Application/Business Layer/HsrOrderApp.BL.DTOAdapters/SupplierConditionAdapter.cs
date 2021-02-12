#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DTOAdapters.Helper;
using HsrOrderApp.SharedLibraries.DTO;

#endregion

namespace HsrOrderApp.BL.DtoAdapters
{
    public class SupplierConditionAdapter
    {
        #region SupplierConditionToDTO

        public static IList<SupplierConditionListDTO> SupplierConditionsToListDtos(IQueryable<SupplierCondition> supplierConditions)
        {
            IQueryable<SupplierConditionListDTO> supplierconditinDTOs = from s in supplierConditions
                                                                        select new SupplierConditionListDTO()
                                                                        {
                                                                            Id = s.SupplierConditionId,
                                                                            //ProductName = s.Product.ToString(),
                                                                            //SupplierName = s.Supplier.ToString(),
                                                                            StandardPrice = s.StandardPrice,
                                                                            LastReceiptCost = s.LastReceiptCost,
                                                                            LastReceiptDate = s.LastReceiptDate,
                                                                            MinOrderQty = s.MinOrderQty,
                                                                            MaxOrderQty = s.MaxOrderQty,
                                                                        };
            return supplierconditinDTOs.ToList();
        }


        public static SupplierConditionDTO SupplierConditionToDto(SupplierCondition s)
        {
            SupplierConditionDTO dto = new SupplierConditionDTO()
            {
                Id = s.SupplierConditionId,
                //ProductName = s.Product.ToString(),
                //SupplierName = s.Supplier.ToString(),
                ProductId = s.Product.ProductId,
                SupplierId = s.Supplier.SupplierId,
                StandardPrice = s.StandardPrice,
                LastReceiptCost = s.LastReceiptCost,
                LastReceiptDate = s.LastReceiptDate,
                MinOrderQty = s.MinOrderQty,
                MaxOrderQty = s.MaxOrderQty,
                Version = s.Version
            };

            return dto;
        }

        #endregion

        #region DTOToSupplierCondition

        public static SupplierCondition DtoToSupplierCondition(SupplierConditionDTO dto)
        {
            SupplierCondition supplierCondition = new SupplierCondition()
            {
                SupplierConditionId = dto.Id,
                StandardPrice = dto.StandardPrice,
                LastReceiptCost = dto.LastReceiptCost,
                LastReceiptDate = dto.LastReceiptDate,
                MinOrderQty = dto.MinOrderQty,
                MaxOrderQty = dto.MaxOrderQty,
                //Product = new Product() { ProductId = dto.ProductId },
                //Supplier = new Supplier() { SupplierId = dto.SupplierId },
                Version = dto.Version
            };
            ValidationHelper.Validate(supplierCondition);
            return supplierCondition;
        }



        #endregion
    }
}