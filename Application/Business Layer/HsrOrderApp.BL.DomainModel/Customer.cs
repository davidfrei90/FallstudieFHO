#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel
{
    public class Customer : DomainObject
    {
        public Customer()
        {
            this.CustomerId = default(int);
            this.Name = string.Empty;
            this.FirstName = string.Empty;
            this.Addresses = new List<Address>().AsQueryable();
            this.Orders = new List<Order>().AsQueryable();

            this.User = null;
        }

        public int CustomerId { get; set; }

        [StringLengthValidator(1, 50)]
        public string Name { get; set; }

        [StringLengthValidator(1, 50)]
        public string FirstName { get; set; }

        public IQueryable<Address> Addresses { get; set; }
        public IQueryable<Order> Orders { get; set; }

        public User User { get; set; }

        public override string ToString()
        {
            if (this.Name == string.Empty)
                return string.Empty;
            if (this.FirstName == string.Empty)
                return this.Name;
            return this.Name + ", " + this.FirstName;
        }
    }
}