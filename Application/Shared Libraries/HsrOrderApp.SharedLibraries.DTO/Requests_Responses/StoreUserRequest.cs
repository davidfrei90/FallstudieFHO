#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class StoreUserRequest : RequestType
    {
        [DataMember]
        [ObjectValidator]
        public UserDTO User { get; set; }
    }
}