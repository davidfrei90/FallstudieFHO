#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO 
{
    public class CurrentUserDTO : DTOBase
    {
        public CurrentUserDTO()
        {
            this.UserName = string.Empty;
            this.Roles = new List<RoleDTO>();
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string UserName { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        [ObjectCollectionValidator(typeof (RoleDTO))]
        public IList<RoleDTO> Roles { get; set; }
    }
}