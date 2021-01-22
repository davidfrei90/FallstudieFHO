#region

using HsrOrderApp.BL.DomainModel.HelperObjects;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel
{
    public class OrderDetail : DomainObject
    {
        public OrderDetail()
        {
            this.Order = new UnknownOrder();
            this.Product = new UnknownProduct();
            this.OrderDetailId = default(int);
            this.UnitPrice = default(decimal);
            this.QuantityInUnits = default(int);
        }

        public int OrderDetailId { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

        [RangeValidator(typeof (decimal), "0.0", RangeBoundaryType.Inclusive, "0.0", RangeBoundaryType.Ignore)]
        public decimal UnitPrice { get; set; }

        [RangeValidator(0, RangeBoundaryType.Exclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int QuantityInUnits { get; set; }
    }
}