#region

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    [HasSelfValidation]
    public class OrderDTO : DTOParentObject
    {
        private int _customerId;
        private string _customerName;
        private IList<OrderDetailDTO> _details;
        private DateTime? _orderDate;
        private OrderStatus _orderStatus;
        private DateTime? _shippedDate;

        public OrderDTO()
        {
            this.CustomerId = default(int);
            this.CustomerName = string.Empty;
            this.OrderStatus = OrderStatus.Draft;
            this.OrderDate = null;
            this.ShippedDate = null;
            this.Details = new List<OrderDetailDTO>();
        }

        [DataMember]
        [NotNullValidator]
        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                if (value != _customerId)
                {
                    this._customerId = value;
                    OnPropertyChanged(() => CustomerId);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (value != _customerName)
                {
                    this._customerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }

        [DataMember]
        public OrderStatus OrderStatus
        {
            get { return _orderStatus; }
            set
            {
                if (value != _orderStatus)
                {
                    this._orderStatus = value;
                    OnPropertyChanged(() => OrderStatus);
                }
            }
        }

        [DataMember]
        public DateTime? OrderDate
        {
            get { return _orderDate; }
            set
            {
                if (value != _orderDate)
                {
                    this._orderDate = value;
                    OnPropertyChanged(() => OrderDate);
                }
            }
        }

        [DataMember]
        public DateTime? ShippedDate
        {
            get { return _shippedDate; }
            set
            {
                if (value != _shippedDate)
                {
                    this._shippedDate = value;
                    OnPropertyChanged(() => ShippedDate);
                }
            }
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (OrderDetailDTO))]
        public IList<OrderDetailDTO> Details
        {
            get { return _details; }
            set
            {
                if (value != _details)
                {
                    this._details = value;
                    OnPropertyChanged(() => Details);
                }
            }
        }

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