#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetSupplierConditionsRequest : RequestType
    {
        [DataMember]
        [NotNullValidator]
        public SupplierConditionSearchType SearchType { get; set; }

        [DataMember]
        public int SupplierId { get; set; }
    }
}