#region

using System.Windows.Input;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private RelayCommand _navigationCommand;

        public MainWindowViewModel()
        {
        }

        public SecurityContext SecurityContext
        {
            get { return SecurityContext.Current(); }
        }

        public ViewModelBase CurrentViewModel
        {
            get { return this._currentViewModel; }
            set
            {
                if (this._currentViewModel != value)
                {
                    SendPropertyChanging("CurrentViewModel");
                    this._currentViewModel = value;
                    SendPropertyChanged("CurrentViewModel");
                }
            }
        }

        public ICommand NavigationCommand
        {
            get
            {
                if (_navigationCommand == null)
                {
                    _navigationCommand = new RelayCommand(
                        param => NavigationService.NavigateTo((string) param)
                        );
                }
                return _navigationCommand;
            }
        }
    }
}