#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel
{
    public class User : DomainObject
    {
        public User()
        {
            this.UserId = default(int);
            this.UserName = string.Empty;
            this.Password = Guid.NewGuid().ToString();
            this.Customer = null;
            this.Roles = new List<Role>().AsQueryable();
        }

        public int UserId { get; set; }

        [StringLengthValidator(1, 50)]
        public string UserName { get; set; }

        [StringLengthValidator(1, 50)]
        public string Password { get; set; }

        public Customer Customer { get; set; }

        public IQueryable<Role> Roles { get; set; }
    }
}