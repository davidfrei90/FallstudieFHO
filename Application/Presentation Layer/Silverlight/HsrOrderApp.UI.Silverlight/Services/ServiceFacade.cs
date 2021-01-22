using System;
using System.Net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using HsrOrderApp.UI.Silverlight.AuthenticationService;
using HsrOrderApp.UI.Silverlight.CustomerService;
using System.ComponentModel;
using Microsoft.Practices.Prism.Events;

namespace HsrOrderApp.UI.Silverlight
{
    public  class ServiceFacade : IServiceFacade
    {
        private static ServiceFacade _instance;
        private AuthenticationService.AuthenticationServiceClient _authenticationservice;
        private CustomerService.CustomerServiceClient _customerservice;
        private IEventAggregator _messageBus;
        public ServiceFacade(IEventAggregator  messageBus )
        {
            _messageBus = messageBus;
            if(_authenticationservice==null)
            {
                _authenticationservice= new AuthenticationServiceClient();
            }
            if(_customerservice==null)
            {
             _customerservice = new CustomerServiceClient();
            }

            
        }

        //public static ServiceFacade GetInstance()
        //{
        //    if(_instance==null)
        //    {
        //        _instance= new ServiceFacade();
        //    }
        //    return _instance;
        //}

        #region Authentication

        public void Login(string username,string Password, bool persistent)
        {
            _authenticationservice.LoginAsync(username,Password,null,persistent);
        }
        public void Logout()
        {
            _authenticationservice.LogoutAsync();
        }

        public CustomerServiceClient CustomerServiceClient
        {
            get { return _customerservice; }
        }

        public event EventHandler<LoginCompletedEventArgs> LoginCompletedEvent
        {
            add { _authenticationservice.LoginCompleted += value; }
            remove { _authenticationservice.LoginCompleted -= value;}
        }

        public event EventHandler<AsyncCompletedEventArgs> LogoutCompletedEvent
        {
            add { _authenticationservice.LogoutCompleted += value; }
            remove { _authenticationservice.LogoutCompleted -= value; }
        }
        #endregion
    }
}
