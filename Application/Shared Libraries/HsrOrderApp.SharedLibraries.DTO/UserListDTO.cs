#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    public class UserListDTO : DTOBase
    {
        public UserListDTO()
        {
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string UserName { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string Roles { get; set; }
    }
}