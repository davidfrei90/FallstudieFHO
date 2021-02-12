#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.SupplierCondition
{
    public class SupplierConditionViewModel : ListViewModelBase<SupplierConditionListDTO>
    {
        protected override void LoadData()
        {
            this.DisplayName = Strings.SupplierConditionViewModel_DisplayName;
            IList<SupplierConditionListDTO> supplierConditions = Service.GetAllSupplierConditions();
            foreach (SupplierConditionListDTO supplierCondition in supplierConditions)
                Items.Add(supplierCondition);
        }

        protected override void New()
        {
            SupplierConditionDTO newSupplierCondition = new SupplierConditionDTO();
            SupplierConditionDetailViewModel detailModelView = new SupplierConditionDetailViewModel(newSupplierCondition, true);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == newSupplierCondition.Id);
            }
        }

        protected override void Delete()
        {
            Service.DeleteSupplierCondition(SelectedItem.Id);
            Load();
        }

        protected override void Edit()
        {
            SupplierConditionDTO selectedDto = Service.GetSupplierConditionById(SelectedItem.Id);
            SupplierConditionDetailViewModel detailModelView = new SupplierConditionDetailViewModel(selectedDto, false);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == selectedDto.Id);
            }
        }
    }
}