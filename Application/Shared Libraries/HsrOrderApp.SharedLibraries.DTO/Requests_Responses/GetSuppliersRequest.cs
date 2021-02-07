#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    [DataContract]
    public class GetSuppliersRequest : RequestType
    {
        [DataMember]
        [NotNullValidator]
        public SupplierSearchType SearchType { get; set; }

        [DataMember]
        public string SupplierName { get; set; }

        [DataMember]
        public string City { get; set; }
    }
}