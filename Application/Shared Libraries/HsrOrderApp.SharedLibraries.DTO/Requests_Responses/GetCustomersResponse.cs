#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    [DataContract]
    public class GetCustomersResponse : ResponseType
    {
        public GetCustomersResponse()
        {
            this.Customers = new List<CustomerListDTO>();
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (CustomerListDTO))]
        public IList<CustomerListDTO> Customers { get; set; }
    }
}