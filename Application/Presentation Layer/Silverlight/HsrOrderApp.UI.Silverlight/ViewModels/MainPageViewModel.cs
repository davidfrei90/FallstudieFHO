using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Windows.Navigation;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;


namespace HsrOrderApp.UI.Silverlight.ViewModels
{
    public class MainPageViewModel : Base.ViewModelBase
    {
        private IEventAggregator _messageBus;
        private Uri _selectedPage;
        private Uri _currentViewUri;
        private IServiceFacade _serviceFacade;

        public ICommand NavigateCommand { get; private set; }
        public ICommand LogoutCommand{get; private set;}


        public MainPageViewModel(IEventAggregator messageBus)
        {

            NavigateCommand = new DelegateCommand<string>(Navigate);
            LogoutCommand = new DelegateCommand<object>(Logout);
            _serviceFacade = ((ServiceLocator)App.Current.Resources["ServiceLocator"]).ServiceFacade;
            _messageBus = messageBus;
            _messageBus.GetEvent<Messages.NavigateMessage>().Subscribe(Navigate);
            _selectedPage = new Uri("/Views/Login/LoginWindow.xaml", UriKind.Relative);

        }

        public override void LoadData()
        {
            throw new NotImplementedException();
        }

        
        private void Logout(object ob)
        {
            _serviceFacade.LogoutCompletedEvent += new EventHandler<AsyncCompletedEventArgs>(MainPageViewModel_LogoutCompletedEvent);
            _serviceFacade.Logout();
            SelectedPage = new Uri("/Views/Login/LoginWindow.xaml", UriKind.Relative);
            UpdateState("loggedOut",null);
        }

        void MainPageViewModel_LogoutCompletedEvent(object sender, AsyncCompletedEventArgs e)
        {
           
        }


        public void OnNavigatedMessage(string uriString)
        {
            Navigate(new Uri(uriString, UriKind.Relative));
        }

        public void Navigate(Uri uri)
        {
            SelectedPage = uri;
        }

        public void Navigate(string uriString)
        {
            Navigate(new Uri(uriString, UriKind.Relative));
        }

        public Uri SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                if (_selectedPage != value)
                {
                    _selectedPage = value;
                    this.RaisePropertyChanged("SelectedPage");
                }
            }
        }



    }
}
