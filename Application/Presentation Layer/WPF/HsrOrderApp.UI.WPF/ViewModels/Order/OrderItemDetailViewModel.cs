#region

using System.Collections.Generic;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;
using HsrOrderApp.UI.PresentationLogic.Helpers;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Order
{
    public class OrderItemDetailViewModel : DetailViewModelBase<OrderDetailDTO>
    {
        private string _estimatedDeliveryTime;

        public OrderItemDetailViewModel(OrderDetailDTO orderDetail, bool isNew)
            : base(orderDetail, isNew)
        {
            this.DisplayName = Strings.OrderItemDetailViewModel_DisplayName;
        }

        public int ProductId
        {
            get { return this.Model.ProductId; }
            set
            {
                if (this.Model.ProductId != value)
                {
                    SendPropertyChanging("ProductId");
                    ProductDTO pdto = Service.GetProductById(value);
                    this.Model.ProductId = value;
                    this.Model.ProductName = pdto.Name;
                    this.Model.UnitPrice = pdto.ListUnitPrice;
                    CalculateEstimatedDeliveryTime();
                    SendPropertyChanged("ProductId");
                }
            }
        }

        public string EstimatedDeliveryTime
        {
            get
            {
                if (string.IsNullOrEmpty(_estimatedDeliveryTime))
                {
                    CalculateEstimatedDeliveryTime();
                }
                return _estimatedDeliveryTime;
            }
            private set
            {
                if (this._estimatedDeliveryTime != value)
                {
                    SendPropertyChanging("EstimatedDeliveryTime");
                    _estimatedDeliveryTime = value;
                    SendPropertyChanged("EstimatedDeliveryTime");
                }
            }
        }


        private void CalculateEstimatedDeliveryTime()
        {
            int unitsAvailable = default(int);
            int deliveryTime = -1;
            if (ProductId > 0)
            {
                Service.GetEstimatedDeliveryTime(ProductId, out unitsAvailable, out deliveryTime);
            }
            if (unitsAvailable > 0 || deliveryTime == -1)
            {
                deliveryTime = ConfigurationHelper.DeliveryTimeFromStock;
            }
            if (deliveryTime > 1)
                EstimatedDeliveryTime = string.Format(Strings.DeliveryTimeFromStock_Plural, deliveryTime);
            else if (deliveryTime == 1)
                EstimatedDeliveryTime = Strings.DeliveryTimeFromStock_Singular;
            else
                EstimatedDeliveryTime = Strings.DeliveryTimeFromStock_Unknown;
        }

        #region Additional Datasources

        // TODO: Mit anderer statischer Ressource lösen?
        public IList<ProductDTO> Products
        {
            get { return Service.GetAllProducts(); }
        }

        #endregion
    }
}