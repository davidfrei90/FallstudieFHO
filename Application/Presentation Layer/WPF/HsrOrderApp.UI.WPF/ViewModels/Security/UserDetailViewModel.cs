#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Security
{
    public class UserDetailViewModel : DetailViewModelBase<UserDTO>
    {
        #region Fields

        private List<CustomerListDTO> _customers = null;
        private RoleViewModel _listViewModel;

        #endregion

        public UserDetailViewModel(UserDTO user, bool isNew)
            : base(user, isNew)
        {
            this.DisplayName = Strings.UserDetailViewModel_DisplayName;
        }

        public RoleViewModel ListViewModel
        {
            get
            {
                if (this._listViewModel == null)
                {
                    this._listViewModel = new RoleViewModel(this.Model);
                    this._listViewModel.LoadCommand.Command.Execute(null);
                }
                return _listViewModel;
            }
        }

        protected override void Load()
        {
            base.Load();
            _customers = Service.GetAllCustomers().ToList();
            _customers.Insert(0, new CustomerListDTO() {Id = default(int), Name = "<Kein Kunde>"});
        }


        protected override void SaveData()
        {
            Service.StoreUser(Model);
            SaveCommandExecuted();
        }

        #region Additional Datasources

        public List<CustomerListDTO> Customers
        {
            get { return _customers; }
        }

        #endregion
    }
}