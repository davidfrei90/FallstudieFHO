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
            this.SupplierConditions = new List<SupplierConditionDTO>();
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (SupplierConditionDTO))]
        public IList<SupplierConditionDTO> SupplierConditions { get; set; }
    }
}