#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetUsersResponse : ResponseType
    {
        public GetUsersResponse()
        {
            this.Users = new List<UserListDTO>();
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (UserListDTO))]
        public IList<UserListDTO> Users { get; set; }
    }
}