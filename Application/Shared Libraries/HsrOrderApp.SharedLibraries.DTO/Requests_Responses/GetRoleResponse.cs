#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetRoleResponse : ResponseType
    {
        public GetRoleResponse()
        {
            this.Role = new RoleDTO();
        }

        [DataMember]
        [ObjectValidator]
        public RoleDTO Role { get; set; }
    }
}