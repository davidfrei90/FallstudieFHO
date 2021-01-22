#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    [DataContract]
    [KnownType(typeof (OrderDetailDTO))]
    public class StoreOrderRequest : RequestType
    {
        [DataMember]
        [ObjectValidator]
        public OrderDTO Order { get; set; }
    }
}