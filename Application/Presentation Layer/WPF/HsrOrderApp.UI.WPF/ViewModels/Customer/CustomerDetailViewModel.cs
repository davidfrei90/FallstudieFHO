#region

using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Customer
{
    public class CustomerDetailViewModel : DetailViewModelBase<CustomerDTO>
    {
        #region Fields

        private AddressViewModel _listViewModel;

        #endregion

        public CustomerDetailViewModel(CustomerDTO customer, bool isNew) : base(customer, isNew)
        {
            this.DisplayName = Strings.CustomerDetailViewModel_DisplayName;
        }

        public AddressViewModel ListViewModel
        {
            get
            {
                if (this._listViewModel == null)
                {
                    this._listViewModel = new AddressViewModel(this.Model);
                    this._listViewModel.LoadCommand.Command.Execute(null);
                }
                return _listViewModel;
            }
        }

        protected override void SaveData()
        {
            Service.StoreCustomer(Model);
            SaveCommandExecuted();
        }
    }
}