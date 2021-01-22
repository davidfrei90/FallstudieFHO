#region

using System;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    public class OrderListDTO : DTOBase
    {
        public OrderListDTO()
        {
            this.CustomerName = string.Empty;
            this.OrderStatus = OrderStatus.Draft;
            this.OrderDate = null;
            this.ShippedDate = null;
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string CustomerName { get; set; }

        [DataMember]
        [NotNullValidator]
        public OrderStatus OrderStatus { get; set; }

        [DataMember]
        public DateTime? OrderDate { get; set; }

        [DataMember]
        public DateTime? ShippedDate { get; set; }
    }
}