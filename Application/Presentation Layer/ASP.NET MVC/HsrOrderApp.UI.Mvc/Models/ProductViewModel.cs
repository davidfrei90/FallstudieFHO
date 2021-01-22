using HsrOrderApp.SharedLibraries.DTO;
using System.Collections.Generic;

namespace HsrOrderApp.UI.Mvc.Models
{
    public class ProductListViewModel : ListViewModelBase<ProductDTO>
    {
        public ProductListViewModel() { }
        public ProductListViewModel(List<ProductDTO> list) { Items = list; }
    }
    public class ProductViewModel : DetailViewModelBase<ProductDTO>
    {
        public ProductViewModel() { }
        public ProductViewModel(ProductDTO model, bool isNew) : base(model, isNew) { }
    }
}