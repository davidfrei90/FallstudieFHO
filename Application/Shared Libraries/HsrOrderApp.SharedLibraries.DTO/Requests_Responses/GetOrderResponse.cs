#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    [DataContract]
    [KnownType(typeof (OrderDTO))]
    [KnownType(typeof (OrderDetailDTO))]
    public class GetOrderResponse : ResponseType
    {
        public GetOrderResponse()
        {
            this.Order = new OrderDTO();
        }

        [DataMember]
        [ObjectValidator]
        public OrderDTO Order { get; set; }
    }
}