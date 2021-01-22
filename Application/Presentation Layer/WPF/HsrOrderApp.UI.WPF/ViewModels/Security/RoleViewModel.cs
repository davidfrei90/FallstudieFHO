#region

using System.Collections.Generic;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Security
{
    public class RoleViewModel : ListViewModelBase<RoleDTO>
    {
        public RoleViewModel(UserDTO order)
            : base(order)
        {
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
                       {
                           new CommandViewModel(Strings.NewCommand, new RelayCommand(param => New(), param => CanNew())),
                           new CommandViewModel(Strings.DeleteCommand,
                                                new RelayCommand(param => Delete(), param => CanDelete()))
                       };
        }

        protected override void LoadData()
        {
            foreach (RoleDTO role in ((UserDTO) ParentObject).Roles)
                Items.Add(role);
        }

        protected override void New()
        {
            RoleDTO newRole = new RoleDTO();
            RoleDetailViewModel detailModelView = new RoleDetailViewModel(newRole, true);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                newRole = detailModelView.Model;
                ParentObject.MarkChildForInsertion(newRole);
                Items.Add(newRole);
                SelectedItem = newRole;
            }
        }

        protected override void Delete()
        {
            ParentObject.MarkChildForDeletion(SelectedItem);
            Items.Remove(SelectedItem);
        }

        protected override void Edit()
        {
            RoleDetailViewModel detailModelView = new RoleDetailViewModel(SelectedItem, false);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                ParentObject.MarkChildForUpdate(SelectedItem);
            }
        }
    }
}