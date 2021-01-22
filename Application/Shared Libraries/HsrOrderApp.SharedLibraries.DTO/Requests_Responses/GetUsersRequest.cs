#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetUsersRequest : RequestType
    {
        public GetUsersRequest()
        {
            this.SearchType = UserSearchType.None;
            this.Username = string.Empty;
            this.Rolename = string.Empty;
        }

        [DataMember]
        [NotNullValidator]
        public UserSearchType SearchType { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Rolename { get; set; }
    }
}