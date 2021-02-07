﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using System.Linq;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HsrOrderApp.BL.DomainModel
{
    public class SupplierCondition:DomainObject
    {
        public SupplierCondition()
        {
            this.SupplierConditionId = default(int);
            this.Product = new UnknownProduct();
            this.Supplier = new UnknownSupplier();
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

        public Supplier Supplier { get; set; }

        public Product Product { get; set; }

        public int SupplierConditionId { get; set; }
    }
}
