#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Customer
{
    public class CustomerViewModel : ListViewModelBase<CustomerListDTO>
    {
        protected override void LoadData()
        {
            this.DisplayName = Strings.CustomerViewModel_DisplayName;
            IList<CustomerListDTO> customers = Service.GetAllCustomers();
            foreach (CustomerListDTO customer in customers)
                Items.Add(customer);
        }

        protected override void New()
        {
            CustomerDTO newCustomer = new CustomerDTO();
            CustomerDetailViewModel detailModelView = new CustomerDetailViewModel(newCustomer, true);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == newCustomer.Id);
            }
        }

        protected override void Delete()
        {
            Service.DeleteCustomer(SelectedItem.Id);
            Load();
        }

        protected override void Edit()
        {
            CustomerDTO selectedDto = Service.GetCustomerById(SelectedItem.Id);
            CustomerDetailViewModel detailModelView = new CustomerDetailViewModel(selectedDto, false);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == selectedDto.Id);
            }
        }
    }
}