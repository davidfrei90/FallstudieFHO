#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Supplier
{
    public class SupplierViewModel : ListViewModelBase<SupplierListDTO>
    {
        protected override void LoadData()
        {
            this.DisplayName = Strings.SupplierViewModel_DisplayName;
            IList<SupplierListDTO> suppliers = Service.GetAllSuppliers();
            foreach (SupplierListDTO supplier in suppliers)
                Items.Add(supplier);
        }

        protected override void New()
        {
            SupplierDTO newSupplier = new SupplierDTO();
            SupplierDetailViewModel detailModelView = new SupplierDetailViewModel(newSupplier, true);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == newSupplier.Id);
            }
        }

        protected override void Delete()
        {
            Service.DeleteSupplier(SelectedItem.Id);
            Load();
        }

        protected override void Edit()
        {
            SupplierDTO selectedDto = Service.GetSupplierById(SelectedItem.Id);
            SupplierDetailViewModel detailModelView = new SupplierDetailViewModel(selectedDto, false);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == selectedDto.Id);
            }
        }
    }
}