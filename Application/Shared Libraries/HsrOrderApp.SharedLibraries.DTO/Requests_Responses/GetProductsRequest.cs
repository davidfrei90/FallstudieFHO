#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetProductsRequest : RequestType
    {
        [DataMember]
        public ProductSearchType SearchType { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string Category { get; set; }
    }
}