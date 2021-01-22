#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel
{
    public class Role : DomainObject
    {
        public Role()
        {
            this.RoleId = default(int);
            this.RoleName = string.Empty;
            this.Users = new List<User>().AsQueryable();
        }

        public int RoleId { get; set; }

        [StringLengthValidator(1, 50)]
        public string RoleName { get; set; }

        public IQueryable<User> Users { get; set; }
    }
}