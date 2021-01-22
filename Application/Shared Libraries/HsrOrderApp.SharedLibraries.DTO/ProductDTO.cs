#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    public class ProductDTO : DTOParentObject
    {
        private string _category;
        private decimal _listUnitPrice;
        private string _name;
        private string _productNumber;
        private double _quantityPerUnit;
        private int _unitsOnStock;

        public ProductDTO()
        {
            this.Name = string.Empty;
            this.ProductNumber = string.Empty;
            this.Category = string.Empty;
            this.QuantityPerUnit = default(double);
            this.ListUnitPrice = default(decimal);
            this.UnitsOnStock = default(int);
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    this._name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string ProductNumber
        {
            get { return _productNumber; }
            set
            {
                if (value != _productNumber)
                {
                    this._productNumber = value;
                    OnPropertyChanged(() => ProductNumber);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string Category
        {
            get { return _category; }
            set
            {
                if (value != _category)
                {
                    this._category = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }

        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Exclusive, double.MaxValue, RangeBoundaryType.Ignore)]
        public double QuantityPerUnit
        {
            get { return _quantityPerUnit; }
            set
            {
                if (value != _quantityPerUnit)
                {
                    this._quantityPerUnit = value;
                    OnPropertyChanged(() => QuantityPerUnit);
                }
            }
        }

        [DataMember]
        [RangeValidator(typeof (decimal), "0.0", RangeBoundaryType.Inclusive, "0", RangeBoundaryType.Ignore)]
        public decimal ListUnitPrice
        {
            get { return _listUnitPrice; }
            set
            {
                if (value != _listUnitPrice)
                {
                    this._listUnitPrice = value;
                    OnPropertyChanged(() => ListUnitPrice);
                }
            }
        }

        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int UnitsOnStock
        {
            get { return _unitsOnStock; }
            set
            {
                if (value != _unitsOnStock)
                {
                    this._unitsOnStock = value;
                    OnPropertyChanged(() => UnitsOnStock);
                }
            }
        }
    }
}