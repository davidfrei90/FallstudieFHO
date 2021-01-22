#region

using System.Collections.Generic;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;
using HsrOrderApp.UI.WPF.Converters;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Product
{
    public class ProductDetailViewModel : DetailViewModelBase<ProductDTO>
    {
        public ProductDetailViewModel(ProductDTO product, bool isNew) : base(product, isNew)
        {
            this.DisplayName = Strings.ProductDetailViewModel_DisplayName;
        }

        protected override void SaveData()
        {
            Service.StoreProduct(Model);
            SaveCommandExecuted();
        }
    }
}