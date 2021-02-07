#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetSupplierConditionsResponse : ResponseType
    {
        public GetSupplierConditionsResponse()
        {
            this.SupplierConditions = new List<SupplierConditionListDTO>();
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (SupplierConditionListDTO))]
        public IList<SupplierConditionListDTO> SupplierConditions { get; set; }
    }
}