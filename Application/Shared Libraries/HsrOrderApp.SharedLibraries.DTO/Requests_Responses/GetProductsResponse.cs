#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetProductsResponse : ResponseType
    {
        public GetProductsResponse()
        {
            this.Products = new List<ProductDTO>();
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (ProductDTO))]
        public IList<ProductDTO> Products { get; set; }
    }
}