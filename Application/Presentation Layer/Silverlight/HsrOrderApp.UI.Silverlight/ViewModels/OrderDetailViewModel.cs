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
using HsrOrderApp.UI.Silverlight.CustomerService;
using HsrOrderApp.UI.Silverlight.ViewModels.Base;
using Microsoft.Practices.Prism.Events;

namespace HsrOrderApp.UI.Silverlight.ViewModels
{
    public class OrderDetailViewModel : ViewModelBase
    {
        private IEventAggregator _messageBus;
        private IServiceFacade _serviceFacade;
        public OrderDetailViewModel(IEventAggregator messageBus)
        {
            _messageBus = messageBus;
            _messageBus.GetEvent<Messages.OrderDetailMessage>().Subscribe(Load);

        }

        public void Navigate(Uri obj)
        {
            throw new NotImplementedException();
        }

        public void Load(OrderListDTO obj)
        {
           _serviceFacade = ((ServiceLocator)App.Current.Resources["ServiceLocator"]).ServiceFacade;
           _serviceFacade.CustomerServiceClient.GetOrderByIdCompleted += new EventHandler<GetOrderByIdCompletedEventArgs>(CustomerServiceClient_GetOrderByIdCompleted);
           _serviceFacade.CustomerServiceClient.GetOrderByIdAsync(new GetOrderRequest() { Id = obj.Id });
        }

        void CustomerServiceClient_GetOrderByIdCompleted(object sender, GetOrderByIdCompletedEventArgs e)
        {
            Order = e.Result.Order;
        }


        private OrderDTO _order;

        public OrderDTO Order
        {
            get { return _order; }
            private set { _order = value; RaisePropertyChanged("Order"); }
        }

        public override void LoadData()
        {

        }

        public void Unscribe()
        {
          _messageBus.GetEvent<Messages.OrderDetailMessage>().Unsubscribe(Load);
          _serviceFacade.CustomerServiceClient.GetOrderByIdCompleted -= CustomerServiceClient_GetOrderByIdCompleted;
        }
    }
}
