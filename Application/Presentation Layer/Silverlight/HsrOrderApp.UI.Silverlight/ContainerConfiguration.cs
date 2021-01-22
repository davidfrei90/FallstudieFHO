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
using HsrOrderApp.UI.Silverlight.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Events;

namespace HsrOrderApp.UI.Silverlight
{
    public class ContainerConfiguration
    {
        private UnityContainer _container;

        public IUnityContainer Container
        {
            get { return _container; }
        }

        public ContainerConfiguration()
        {
            _container = new UnityContainer();
            _container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IServiceFacade, ServiceFacade>(new ContainerControlledLifetimeManager());
            _container.RegisterType<MainPageViewModel>();
            _container.RegisterType<LoginViewModel>();
            _container.RegisterType<OrderViewModel>();
            _container.RegisterType<OrderDetailViewModel>();
            _container.RegisterType<CustomerDetailViewModel>();


        }
    }
}
