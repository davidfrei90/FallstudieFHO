#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetUserResponse : ResponseType
    {
        public GetUserResponse()
        {
            this.User = new UserDTO();
        }

        [DataMember]
        [ObjectValidator]
        public UserDTO User { get; set; }
    }
}