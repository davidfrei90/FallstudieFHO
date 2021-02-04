using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HsrOrderApp.BL.DomainModel
{
    public class SupplierCondition
    {
        public SupplierCondition()
        {
            this.SupplierConditionId = default(int);
            this.ProductId = default(int);
            this.SupplierId = default(int);
            this.StandardPrice = default(decimal);
            this.LastReceiptDate = null;
            this.MinOrderQty = default(int);
            this.MaxOrderQty = default(int);
        }

        public int MaxOrderQty { get; set; }

        public int MinOrderQty { get; set; }

        public DateTime? LastReceiptDate { get; set; }

        public decimal StandardPrice { get; set; }

        public int SupplierId { get; set; }

        public int ProductId { get; set; }

        public int SupplierConditionId { get; set; }
    }
}
