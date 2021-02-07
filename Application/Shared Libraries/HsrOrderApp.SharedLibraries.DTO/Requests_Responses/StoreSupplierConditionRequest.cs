#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class StoreSupplierConditionRequest : RequestType
    {
        [DataMember]
        [ObjectValidator]
        public SupplierConditionDTO SupplierCondition { get; set; }
    }
}