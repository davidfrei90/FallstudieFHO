using System;
using System.Collections.Generic;
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
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;


namespace HsrOrderApp.UI.Silverlight.ViewModels
{
    public class LoginViewModel : Base.ViewModelBase
    {
        private const string ERRORMESSAGE = "Versuchen Sie es nochmals";
        private bool _isBusy;
        private bool _loginfailed;
        private IEventAggregator _messageBus;
        private IServiceFacade _serviceFacade;

        public LoginViewModel(IEventAggregator messageBus)
        {
            _messageBus = messageBus;
            LoginCommand = new DelegateCommand<object>(Login);
            _serviceFacade = ((ServiceLocator)App.Current.Resources["ServiceLocator"]).ServiceFacade;
            _serviceFacade.LoginCompletedEvent += new EventHandler<HsrOrderApp.UI.Silverlight.AuthenticationService.LoginCompletedEventArgs>(LoginViewModel_LoginCompletedEvent);
        }

        public void Login(object obj)
        {
            _serviceFacade.Login(Username, Password, false);
        }

        void LoginViewModel_LoginCompletedEvent(object sender, HsrOrderApp.UI.Silverlight.AuthenticationService.LoginCompletedEventArgs e)
        {
            if (e.Result)
            {
                UpdateState("loggedIn",null);
                _messageBus.GetEvent<Messages.NavigateMessage>().Publish(new Uri("/Views/Home.xaml",UriKind.Relative));
                return;
            }
            _loginfailed = true;
            this.RaisePropertyChanged("ErrorMessage");
            //InvokePropertyChanged(new PropertyChangedEventArgs("ErrorMessage"));
        }

        public override void LoadData()
        {
            throw new NotImplementedException();
        }

        public Cursor Cursor
        {
            get { return _isBusy ? Cursors.Wait : Cursors.Arrow; }
        }

        public string ErrorMessage
        {
            get { return _loginfailed ? ERRORMESSAGE : null;}
        }
        public string Username
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }

        public ICommand LoginCommand { get; private set; }
        
   
    }
}
