#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetOrdersResponse : ResponseType
    {
        public GetOrdersResponse()
        {
            this.Orders = new List<OrderListDTO>();
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (OrderListDTO))]
        public IList<OrderListDTO> Orders { get; set; }
    }
}