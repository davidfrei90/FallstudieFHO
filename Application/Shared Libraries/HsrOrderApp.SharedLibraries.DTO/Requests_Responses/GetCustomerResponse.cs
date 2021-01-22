#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    [DataContract]
    [KnownType(typeof (CustomerDTO))]
    [KnownType(typeof (AddressDTO))]
    public class GetCustomerResponse : ResponseType
    {
        public GetCustomerResponse()
        {
            this.Customer = new CustomerDTO();
        }

        [DataMember]
        [ObjectValidator]
        public CustomerDTO Customer { get; set; }
    }
}