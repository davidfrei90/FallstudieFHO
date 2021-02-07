#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    [DataContract]
    public class GetSuppliersResponse : ResponseType
    {
        public GetSuppliersResponse()
        {
            this.Suppliers = new List<SupplierListDTO>();
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (SupplierListDTO))]
        public IList<SupplierListDTO> Suppliers { get; set; }
    }
}