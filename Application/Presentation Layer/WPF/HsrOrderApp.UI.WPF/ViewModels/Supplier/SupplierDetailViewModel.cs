#region

using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Supplier
{
    public class SupplierDetailViewModel : DetailViewModelBase<SupplierDTO>
    {
        #region Fields

        private AddressViewModel _listViewModel;

        #endregion

        public SupplierDetailViewModel(SupplierDTO supplier, bool isNew) : base(supplier, isNew)
        {
            this.DisplayName = Strings.SupplierViewModel_DisplayName;
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
            Service.StoreSupplier(Model);
            SaveCommandExecuted();
        }
    }
}