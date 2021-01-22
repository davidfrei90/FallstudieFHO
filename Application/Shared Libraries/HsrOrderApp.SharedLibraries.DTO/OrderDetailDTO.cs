#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    public class OrderDetailDTO : DTOVersionObject
    {
        private int _productId;
        private string _productName;
        private int _quantity;
        private decimal _unitPrice;

        public OrderDetailDTO()
        {
            this.Id = default(int);
            this.ProductName = default(string);
            this.UnitPrice = default(decimal);
            this.QuantityInUnits = default(int);
        }

        [DataMember]
        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int ProductId
        {
            get { return _productId; }
            set
            {
                if (value != _productId)
                {
                    this._productId = value;
                    OnPropertyChanged(() => Id);
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
        [RangeValidator(0, RangeBoundaryType.Exclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int QuantityInUnits
        {
            get { return _quantity; }
            set
            {
                if (value != _quantity)
                {
                    this._quantity = value;
                    OnPropertyChanged(() => QuantityInUnits);
                    OnPropertyChanged(() => TotalPrice);
                }
            }
        }

        [DataMember]
        [RangeValidator(typeof (decimal), "0.0", RangeBoundaryType.Inclusive, "0", RangeBoundaryType.Ignore)]
        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (value != _unitPrice)
                {
                    this._unitPrice = value;
                    OnPropertyChanged(() => UnitPrice);
                    OnPropertyChanged(() => TotalPrice);
                }
            }
        }


        public decimal TotalPrice
        {
            get { return _unitPrice*_quantity; }
        }
    }
}