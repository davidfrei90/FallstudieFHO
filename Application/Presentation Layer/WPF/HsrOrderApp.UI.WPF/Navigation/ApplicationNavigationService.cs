#region

using System;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.ViewModels;
using HsrOrderApp.UI.WPF.ViewModels.Base;
using HsrOrderApp.UI.WPF.ViewModels.Customer;
using HsrOrderApp.UI.WPF.ViewModels.Order;
using HsrOrderApp.UI.WPF.ViewModels.Product;
using HsrOrderApp.UI.WPF.ViewModels.Security;

#endregion

namespace HsrOrderApp.UI.WPF.Navigation
{
    internal class ApplicationNavigationService : INavigationService
    {
        private MainWindowViewModel _mainWindowViewModel;

        public ApplicationNavigationService(MainWindowViewModel mainWindowViewModel)
        {
            this._mainWindowViewModel = mainWindowViewModel;
            // Startup-Screen = Customer
            NavigateTo("Customer");
        }

        public NavigationResult NavigateTo(string url)
        {
            NavigationResult result = NavigationResult.Close;
            if (url == "Detail" && NavigationService.Argument is ViewModelBase && NavigationService.Argument is IDetailViewModelBase)
            {
                ((ViewModelBase) NavigationService.Argument).LoadCommand.Command.Execute(null);
                DetailDialog dialog = new DetailDialog((IDetailViewModelBase) NavigationService.Argument);
                result = dialog.ShowDialog() == true ? NavigationResult.Ok : NavigationResult.Cancel;
            }
            else
            {
                switch (url)
                {
                    case "Customer":
                        this._mainWindowViewModel.CurrentViewModel = new CustomerViewModel();
                        break;
                    case "Order":
                        this._mainWindowViewModel.CurrentViewModel = new OrderViewModel();
                        break;
                    case "Product":
                        this._mainWindowViewModel.CurrentViewModel = new ProductViewModel();
                        break;
                    case "User":
                        this._mainWindowViewModel.CurrentViewModel = new UserViewModel();
                        break;
                    default:
                        throw new Exception(string.Format("Cannot navigate to URL '{0}'.", url));
                }
                this._mainWindowViewModel.CurrentViewModel.LoadCommand.Command.Execute(null);
            }
            return result;
        }
    }
}