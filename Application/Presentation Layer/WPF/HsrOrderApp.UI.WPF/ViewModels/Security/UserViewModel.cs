#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Security
{
    public class UserViewModel : ListViewModelBase<UserListDTO>
    {
        protected override void LoadData()
        {
            this.DisplayName = Strings.UserViewModel_DisplayName;
            IList<UserListDTO> users = Service.GetAllUsers();
            foreach (UserListDTO user in users)
                Items.Add(user);
        }

        protected override void New()
        {
            UserDTO newUser = new UserDTO();
            UserDetailViewModel detailModelView = new UserDetailViewModel(newUser, true);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == newUser.Id);
            }
        }

        protected override void Delete()
        {
            Service.DeleteUser(SelectedItem.Id);
            Load();
        }

        protected override void Edit()
        {
            UserDTO selectedDto = Service.GetUserById(SelectedItem.Id);
            UserDetailViewModel detailModelView = new UserDetailViewModel(selectedDto, false);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == selectedDto.Id);
            }
        }
    }
}