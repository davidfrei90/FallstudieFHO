#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    [DataContract]
    public class GetCustomersRequest : RequestType
    {
        [DataMember]
        [NotNullValidator]
        public CustomerSearchType SearchType { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string City { get; set; }
    }
}