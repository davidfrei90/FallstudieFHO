#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetSupplierConditionsRequest : RequestType
    {
        [DataMember]
        public SupplierConditionSearchType SearchType { get; set; }

        [DataMember]
        public string SupplierConditionId { get; set; }

    }
}