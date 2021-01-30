#region

using System;
using System.Data.SqlTypes;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel
{
    public class SupplierCondition : DomainObject
    {
        public SupplierCondition()
        {
            this.SupplierConditionId = default(int);
            this.ProductId = default(int);
            this.SupplierId = default(int);
            this.StandardPrice = default(decimal);
            this.LastReceiptCost = default(decimal);
            this.LastReceiptDate = default(SqlDateTime);
            this.MinOrderQty = default(int);
            this.MaxOrderQty = default(int);
        }

        public int SupplierConditionId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }

        [RangeValidator(typeof(decimal), "0.0", RangeBoundaryType.Inclusive, "0.0", RangeBoundaryType.Ignore)]
        public decimal StandardPrice { get; set; }

        [RangeValidator(typeof(decimal), "0.0", RangeBoundaryType.Inclusive, "0.0", RangeBoundaryType.Ignore)]
        public decimal LastReceiptCost { get; set; }

        public SqlDateTime LastReceiptDate { get; set; }

        public int MinOrderQty { get; set; }

        public int MaxOrderQty { get; set; }
    }
}