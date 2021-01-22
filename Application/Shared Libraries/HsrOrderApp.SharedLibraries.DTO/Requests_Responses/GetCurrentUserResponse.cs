#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    [KnownType(typeof (CustomerDTO))]
    public class GetCurrentUserResponse : ResponseType
    {
        public GetCurrentUserResponse()
        {
            this.User = new CurrentUserDTO();
        }

        [DataMember]
        [ObjectValidator]
        public CurrentUserDTO User { get; set; }
    }
}