#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    public class RoleDTO : DTOVersionObject
    {
        public RoleDTO()
        {
            this.RoleName = string.Empty;
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string RoleName { get; set; }
    }
}