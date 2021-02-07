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
    public class SupplierConditionDTO : DTOParentObject
    {
        private int _supplierId;
        private string _supplierName; 
        private int _productId;
        private string _productName;
        private decimal _standardPrice;
        private DateTime? _lastReceiptDate;
        private decimal _lastReceiptCost;
        private int _minOrderQty;
        private int _maxOrderQty;

        public SupplierConditionDTO()
        {
            this.SupplierId = default(int);
            this.SupplierName = string.Empty;
            this.ProductId = default(int);
            this.ProductName = string.Empty;
            this.StandardPrice = default(decimal);
            this.LastReceiptDate = null;
            this.LastReceiptCost = default(decimal);
            this.MinOrderQty = default(int);
            this.MaxOrderQty = default(int);
        }

       


        [DataMember]
        [NotNullValidator]
        public int SupplierId
        {
            get { return _supplierId; }
            set
            {
                if (value != _supplierId)
                {
                    this._supplierId = value;
                    OnPropertyChanged(() => SupplierId);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string SupplierName
        {
            get { return _supplierName; }
            set
            {
                if (value != _supplierName)
                {
                    this._supplierName = value;
                    OnPropertyChanged(() => SupplierName);
                }
            }
        }

        [DataMember]
        [NotNullValidator]
        public int ProductId
        {
            get { return _productId; }
            set
            {
                if (value != _productId)
                {
                    this._productId = value;
                    OnPropertyChanged(() => ProductId);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string ProductName
        {
            get { return _productName; }
            set
            {
                if (value != _productName)
                {
                    this._productName = value;
                    OnPropertyChanged(() => ProductName);
                }
            }
        }

        [DataMember]
        public decimal StandardPrice
        {
            get { return _standardPrice; }
            set
            {
                if (value != _standardPrice)
                {
                    this._standardPrice = value;
                    OnPropertyChanged(() => StandardPrice);
                }
            }
        }

        [DataMember]
        public DateTime? LastReceiptDate
        {
            get { return _lastReceiptDate; }
            set
            {
                if (value != _lastReceiptDate)
                {
                    this._lastReceiptDate = value;
                    OnPropertyChanged(() => LastReceiptDate);
                }
            }
        }

        [DataMember]
        public decimal LastReceiptCost
        {
            get { return _lastReceiptCost; }
            set
            {
                if (value != _lastReceiptCost)
                {
                    this._lastReceiptCost = value;
                    OnPropertyChanged(() => LastReceiptCost);
                }
            }
        }

        [DataMember]
        public int MaxOrderQty
        {
            get { return _maxOrderQty; }
            set
            {
                if (value != _maxOrderQty)
                {
                    this._maxOrderQty = value;
                    OnPropertyChanged(() => MaxOrderQty);
                }
            }
        }


        [DataMember]
        public int MinOrderQty
        {
            get { return _minOrderQty; }
            set
            {
                ;
                if (value != _minOrderQty)
                {
                    this._minOrderQty = value;
                    OnPropertyChanged(() => MinOrderQty);
                }
            }
        }
    }
}