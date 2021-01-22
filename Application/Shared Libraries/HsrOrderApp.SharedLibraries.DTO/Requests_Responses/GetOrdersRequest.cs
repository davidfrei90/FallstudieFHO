#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetOrdersRequest : RequestType
    {
        [DataMember]
        [NotNullValidator]
        public OrderSearchType SearchType { get; set; }

        [DataMember]
        public int CustomerId { get; set; }
    }
}