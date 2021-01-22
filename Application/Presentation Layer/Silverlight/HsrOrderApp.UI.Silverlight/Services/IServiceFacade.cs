using System;
using System.ComponentModel;
using HsrOrderApp.UI.Silverlight.AuthenticationService;
using HsrOrderApp.UI.Silverlight.CustomerService;

namespace HsrOrderApp.UI.Silverlight
{
    public interface IServiceFacade
    {
        void Login(string username,string Password, bool persistent);
        void Logout();
        CustomerServiceClient CustomerServiceClient { get; }
        event EventHandler<LoginCompletedEventArgs> LoginCompletedEvent;
        event EventHandler<AsyncCompletedEventArgs> LogoutCompletedEvent;
    }
}