using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;
using HsrOrderApp.UI.Silverlight.ViewModels;
using Microsoft.Practices.Prism.Events;

namespace HsrOrderApp.UI.Silverlight
{
    public class ServiceLocator
    {

        private IUnityContainer _container;

        public IServiceFacade ServiceFacade
        {
            get { return _container.Resolve<IServiceFacade>(); }
        }

        public MainPageViewModel MainPageViewModel
        {
            get { return _container.Resolve<MainPageViewModel>(); }
        }

        public LoginViewModel LoginViewModel
        {
            get { return _container.Resolve<LoginViewModel>(); }
        }

        public CustomerDetailViewModel CustomerDetailViewModel
        {
            get { return _container.Resolve<CustomerDetailViewModel>(); }
        }

        public OrderViewModel OrderViewModel
        {
            get { return _container.Resolve<OrderViewModel>(); }
        }


        public OrderDetailViewModel OrderDetailViewModel
        {
            get { return _container.Resolve<OrderDetailViewModel>(); }
        }
        public IEventAggregator MessageBus
        {
            get { return _container.Resolve<IEventAggregator>(); }
        }

        public ServiceLocator()
        {
            _container = new ContainerConfiguration().Container;
        }
    }
}

