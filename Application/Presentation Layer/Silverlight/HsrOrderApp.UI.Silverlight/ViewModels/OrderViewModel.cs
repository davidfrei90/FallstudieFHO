using System;
using System.Collections.ObjectModel;
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
using HsrOrderApp.UI.Silverlight.CustomerService;
using HsrOrderApp.UI.Silverlight.ViewModels.Base;
using HsrOrderApp.UI.Silverlight.Views;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;

namespace HsrOrderApp.UI.Silverlight.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private OrderListDTO order;
        private OrderListDTO _selectedItem;
        private IEventAggregator _messageBus;
        private IServiceFacade _serviceFacade;
        private bool _isLoaded;
        private DelegateCommand<object> _showDetailsCommand;

        public OrderViewModel(IEventAggregator messageBus)
        {
            _messageBus = messageBus;
            _showDetailsCommand= new DelegateCommand<object>(ShowDetailsWindow);
            LoadData();
        }

        private void ShowDetailsWindow(object obj)
        {
            OrderDetailWindow orderDetailWindow = new OrderDetailWindow();
            _messageBus.GetEvent<Messages.OrderDetailMessage>().Publish(SelectedItem);
            orderDetailWindow.Closing +=new EventHandler<CancelEventArgs>(orderDetailWindow_Closing);
            orderDetailWindow.Show();
        }

        private void orderDetailWindow_Closing(object sender, CancelEventArgs e)
        {
           OrderDetailWindow orderDetailWindow = sender as OrderDetailWindow;
            OrderDetailViewModel order = orderDetailWindow.DataContext as OrderDetailViewModel;
            order.Unscribe();
            
           
        }


        public override void LoadData()
        {
            _serviceFacade = ((ServiceLocator)App.Current.Resources["ServiceLocator"]).ServiceFacade;
          _serviceFacade.CustomerServiceClient.GetOrdersByCriteriaCompleted
                += CustomerServiceClient_GetOrdersByCriteriaCompleted;
           _serviceFacade.CustomerServiceClient.GetOrdersByCriteriaAsync(new GetOrdersRequest() { CustomerId = 5, Id = 1 });
        }
        public bool IsLoaded
        {
            get { return _isLoaded; }
            set 
            {
                _isLoaded = value;
                this.RaisePropertyChanged(ord => ord.IsLoaded);
                //InvokePropertyChanged(new PropertyChangedEventArgs("IsLoaded"));
            }
        }
        void CustomerServiceClient_GetOrdersByCriteriaCompleted(object sender, GetOrdersByCriteriaCompletedEventArgs e)
        {
            try
            {
                Orders = new ObservableCollection<OrderListDTO>();
                Orders.AddRange(e.Result.Orders);
            }
            catch (Exception)
            {
                
                throw;
            }
            
            this.RaisePropertyChanged(ord=> ord.Orders);
            //InvokePropertyChanged(new PropertyChangedEventArgs("Orders"));
            _isLoaded = true;
        }

        public ICommand ShowDetailsCommand
        {
            get { return _showDetailsCommand; }
        }

        public ObservableCollection<OrderListDTO> Orders
        {
            get;
            private set;
        }

        public OrderListDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; }

        }


    }
}
