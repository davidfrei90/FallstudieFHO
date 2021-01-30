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
        #region SupplierConditionsToDTO

        public static IList<SupplierConditionDTO> SupplierConditionsToDtos(IQueryable<SupplierCondition> supplierConditions)
        {
            IQueryable<SupplierConditionDTO> supplierConditionDTOs = from p in supplierConditions
                                                 select SupplierConditionToDto(p);
            return supplierConditionDTOs.ToList();
        }

        public static SupplierConditionDTO SupplierConditionToDto(SupplierCondition p)
        {
            SupplierConditionDTO dto = new SupplierConditionDTO()
                                 {
                                     Id = p.SupplierConditionId,
                                     ProductId = p.ProductId,
                                     StandardPrice = p.StandardPrice,
                                     LastReceiptCost = p.LastReceiptCost,
                                     LastReceiptDate = p.LastReceiptDate,
                                     MinOrderQty = p.MinOrderQty,
                                     MaxOrderQty = p.MaxOrderQty,
                                     Version = p.Version
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
                                      ProductId = dto.ProductId,
                                      StandardPrice = dto.StandardPrice,
                                      LastReceiptCost = dto.LastReceiptCost,
                                      LastReceiptDate = dto.LastReceiptDate,
                                      MinOrderQty = dto.MinOrderQty,
                                      MaxOrderQty = dto.MaxOrderQty,
                                      Version = dto.Version

            };
            ValidationHelper.Validate(supplierCondition);
            return supplierCondition;
        }

        #endregion
    }
}