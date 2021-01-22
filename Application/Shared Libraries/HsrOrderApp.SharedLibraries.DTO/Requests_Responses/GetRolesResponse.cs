#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    [KnownType(typeof (RoleDTO))]
    public class GetRolesResponse : ResponseType
    {
        public GetRolesResponse()
        {
            this.Roles = new List<RoleDTO>();
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (RoleDTO))]
        public IList<RoleDTO> Roles { get; set; }
    }
}