#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel
{
    public class Supplier : DomainObject
    {
        public Supplier()
        {
            this.SupplierId = default(int);
            this.AccountNumber = default(int);
            this.Name = string.Empty;
            this.CreditRating = default(int);
            this.PreferredSupplierFlag = default(bool);
            this.ActiveFlag = default(bool);
            this.PurchasingWebServiceURL = default(string);
            this.Addresses = new List<Address>().AsQueryable();
            //***********SupplierConditionMissing**********
        }

        public int SupplierId { get; set; }
        public int AccountNumber { get; set; }

        [StringLengthValidator(1, 50)]
        public string Name { get; set; }
        public int CreditRating { get; set; }
        public bool PreferredSupplierFlag { get; set; }
        public bool ActiveFlag { get; set; }

        [StringLengthValidator(1, 50)]
        public string PurchasingWebServiceURL { get; set; }
        public IQueryable<Address> Addresses { get; set; }
    }
}