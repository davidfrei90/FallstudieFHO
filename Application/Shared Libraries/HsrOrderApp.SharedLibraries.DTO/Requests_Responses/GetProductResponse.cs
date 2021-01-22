#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetProductResponse : ResponseType
    {
        public GetProductResponse()
        {
            this.Product = new ProductDTO();
        }

        [DataMember]
        [ObjectValidator]
        public ProductDTO Product { get; set; }
    }
}