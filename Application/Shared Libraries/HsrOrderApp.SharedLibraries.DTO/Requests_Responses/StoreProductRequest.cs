#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class StoreProductRequest : RequestType
    {
        [DataMember]
        [ObjectValidator]
        public ProductDTO Product { get; set; }
    }
}