#region

using System;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    public class SupplierConditionDTO : DTOParentObject
    {
        private int _productId;
        private int _supplierId;
        private decimal _standardPrice;
        private decimal _lastReceiptCost;
        private DateTime _lastReceiptDate;
        private int _minOrderQty;
        private int _maxOrderQty;

        public SupplierConditionDTO()
        {
            this.ProductId = default(int);
            this.SupplierId = default(int);
            this.StandardPrice = default(decimal);
            this.LastReceiptCost = default(decimal);
            this.LastReceiptDate = default(DateTime);
            this.MinOrderQty = default(int);
            this.MaxOrderQty = default(int);
        }

        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
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
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
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
        [RangeValidator(typeof(decimal), "0.0", RangeBoundaryType.Inclusive, "0", RangeBoundaryType.Ignore)]
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
        [RangeValidator(typeof(decimal), "0.0", RangeBoundaryType.Inclusive, "0", RangeBoundaryType.Ignore)]
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
        //[RangeValidator(typeof(decimal), "0.0", RangeBoundaryType.Inclusive, "0", RangeBoundaryType.Ignore)]
        public DateTime LastReceiptDate
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
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int MinOrderQty
        {
            get { return _minOrderQty; }
            set
            {
                if (value != _minOrderQty)
                {
                    this._minOrderQty = value;
                    OnPropertyChanged(() => MinOrderQty);
                }
            }
        }

        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
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
    }
}