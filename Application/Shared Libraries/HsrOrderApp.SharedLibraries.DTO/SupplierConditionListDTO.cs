#region

using System;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    public class SupplierConditionListDTO : DTOBase
    {
        public SupplierConditionListDTO()
        {
            this.SupplierName = string.Empty;
            this.ProductName = string.Empty;
            this.StandardPrice = default(decimal);
            this.LastReceiptDate = null;
            this.LastReceiptCost = default(decimal);
            this.MinOrderQty = default(int);
            this.MaxOrderQty = default(int);

        }
        [RangeValidator(0, RangeBoundaryType.Exclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int MaxOrderQty { get; set; }

        [RangeValidator(0, RangeBoundaryType.Exclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int MinOrderQty { get; set; }


        public decimal LastReceiptCost { get; set; }

        public DateTime? LastReceiptDate { get; set; }

        [RangeValidator(typeof(decimal), "0.0", RangeBoundaryType.Inclusive, "0.0", RangeBoundaryType.Ignore)]
        public decimal StandardPrice { get; set; }

        [StringLengthValidator(1, 50)]
        public string SupplierName { get; set; }

        [StringLengthValidator(1, 50)]
        public string ProductName { get; set; }

        public int SupplierConditionId { get; set; }
    }
}