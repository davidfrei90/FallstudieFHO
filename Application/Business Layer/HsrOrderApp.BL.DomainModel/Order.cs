#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel
{
    [HasSelfValidation]
    public class Order : DomainObject
    {
        public Order()
        {
            this.OrderId = default(int);
            this.Customer = new UnkownCustomer();
            this.OrderDate = null;
            this.ShippedDate = null;
            this.OrderStatus = OrderStatus.Draft;
            this.OrderDetails = new List<OrderDetail>().AsQueryable();
        }

        public int OrderId { get; set; }

        [NotNullValidator]
        public Customer Customer { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [NotNullValidator]
        public OrderStatus OrderStatus { get; set; }

        public IQueryable<OrderDetail> OrderDetails { get; set; }

        [SelfValidation]
        public void Validate(ValidationResults results)
        {
            switch (OrderStatus)
            {
                case OrderStatus.Draft:
                    if (ShippedDate != null)
                        results.AddResult(new ValidationResult("Shipped date must be null.", this, "ShippedDate", null, null));
                    break;
                case OrderStatus.Ordered:
                    if (OrderDate == null)
                        results.AddResult(new ValidationResult("Order date cannot be null.", this, "OrderDate", null, null));
                    if (ShippedDate != null)
                        results.AddResult(new ValidationResult("Shipped date must be null.", this, "ShippedDate", null, null));
                    break;
                case OrderStatus.Shipped:
                    if (OrderDate == null)
                        results.AddResult(new ValidationResult("Order date cannot be null.", this, "OrderDate", null, null));
                    if (ShippedDate == null)
                        results.AddResult(new ValidationResult("Shipped date cannot be null.", this, "ShippedDate", null, null));
                    break;
            }
        }
    }
}