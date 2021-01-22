#region

using System.Collections.Generic;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Security
{
    public class RoleDetailViewModel : DetailViewModelBase<RoleDTO>
    {
        public RoleDetailViewModel(RoleDTO role, bool isNew)
            : base(role, isNew)
        {
            this.DisplayName = Strings.RoleDetailViewModel_DisplayName;
        }

        protected override void SaveData()
        {
            //this.Model = Service.GetRoleById(this.Model.RoleId);
            SaveCommandExecuted();
        }

        #region Additional Datasources

        public IList<RoleDTO> Roles
        {
            get { return Service.GetAllRoles(); }
        }

        #endregion
    }
}