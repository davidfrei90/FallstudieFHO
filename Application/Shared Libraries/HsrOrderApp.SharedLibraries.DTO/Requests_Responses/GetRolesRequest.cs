#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetRolesRequest : RequestType
    {
        public GetRolesRequest()
        {
            this.SearchType = RoleSearchType.None;
            this.Rolename = string.Empty;
        }

        [DataMember]
        [NotNullValidator]
        public RoleSearchType SearchType { get; set; }

        [DataMember]
        public string Rolename { get; set; }
    }
}