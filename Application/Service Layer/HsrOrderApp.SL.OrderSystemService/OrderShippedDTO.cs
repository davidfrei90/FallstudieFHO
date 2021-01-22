#region

using System;
using System.Runtime.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SL.OrderSystemService
{
    [DataContract]
    public class OrderShippedDTO
    {
        public OrderShippedDTO()
        {
            this.Id = -1;
            this.ShippedDate = DateTime.Now;
        }

        [DataMember]
        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive)]
        public int Id { get; set; }

        [DataMember]
        [NotNullValidator]
        public DateTime ShippedDate { get; set; }
    }
}