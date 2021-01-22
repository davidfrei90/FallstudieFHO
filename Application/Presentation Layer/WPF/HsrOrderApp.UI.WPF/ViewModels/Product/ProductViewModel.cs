#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Product
{
    public class ProductViewModel : ListViewModelBase<ProductDTO>
    {
        protected override void LoadData()
        {
            this.DisplayName = Strings.ProductViewModel_DisplayName;
            IList<ProductDTO> products = Service.GetAllProducts();
            foreach (ProductDTO product in products)
                Items.Add(product);
        }

        protected override void New()
        {
            ProductDTO newProduct = new ProductDTO();
            ProductDetailViewModel detailModelView = new ProductDetailViewModel(newProduct, true);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == newProduct.Id);
            }
        }

        protected override void Delete()
        {
            Service.DeleteProduct(SelectedItem.Id);
            Load();
        }

        protected override void Edit()
        {
            ProductDTO selectedDto = Service.GetProductById(SelectedItem.Id);
            ProductDetailViewModel detailModelView = new ProductDetailViewModel(selectedDto, false);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == selectedDto.Id);
            }
        }
    }
}