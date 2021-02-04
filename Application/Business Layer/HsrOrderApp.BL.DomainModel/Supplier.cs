﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HsrOrderApp.BL.DomainModel
{
    class Supplier: DomainObject
    {


        public Supplier()
        {
            this.SupplierId = default(int);
            this.AccountNumber = default(int);
            this.Name = String.Empty;
            this.CreditRating = default(int);
            this.PreferredSupplierFlag = default(int);
            this.ActiveFlag = default(int);
            this.PurchasingWebServiceURL = String.Empty;
            this.Addresses = new List<Address>().AsQueryable();
            this.SupplierConditions = new List<SupplierCondition>().AsQueryable();
        }

        [StringLengthValidator(1, 50)]
        public string PurchasingWebServiceURL { get; set; }

        public IQueryable<SupplierCondition> SupplierConditions { get; set; }

        public IQueryable<Address> Addresses { get; set; }

        public int ActiveFlag { get; set; }

        public int PreferredSupplierFlag { get; set; }

        public int CreditRating { get; set; }

        [StringLengthValidator(1, 50)]
        public string Name { get; set; }

        public int AccountNumber { get; set; }

        public int SupplierId { get; set; }
    }
}