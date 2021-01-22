using System;
using System.ComponentModel;
using HsrOrderApp.UI.Silverlight.CustomerService;
using HsrOrderApp.UI.Silverlight.ViewModels.Base;
using Microsoft.Practices.Prism.Commands;


namespace HsrOrderApp.UI.Silverlight.ViewModels
{
    public class CustomerDetailViewModel : ViewModelBase
    {
        private CustomerDTO _customer;
        private DelegateCommand<object> _saveCustomerCommand;
        private IServiceFacade _facade;
        public CustomerDetailViewModel()
        {
            LoadData();
        }

        public override void LoadData()
        {
            //Get the ServiceFacade
            _facade= ((ServiceLocator)App.Current.Resources["ServiceLocator"]).ServiceFacade;
            _facade.CustomerServiceClient.GetCustomerCompleted += new System.EventHandler<GetCustomerCompletedEventArgs>(CustomerServiceClient_GetCustomerCompleted);
            _facade.CustomerServiceClient.GetCustomerAsync();
            SaveCustomerCommand = new DelegateCommand<object>(SaveCustomer);
            //Commands
            
            
            DataLoaded = false;
        }

        private void SaveCustomer(object obj)
        {
            _facade.CustomerServiceClient.StoreCustomerCompleted += new EventHandler<StoreCustomerCompletedEventArgs>(CustomerServiceClient_StoreCustomerCompleted);
            _facade.CustomerServiceClient.StoreCustomerAsync(new StoreCustomerRequest(){Customer =Customer});
        }

        void CustomerServiceClient_StoreCustomerCompleted(object sender, StoreCustomerCompletedEventArgs e)
        {
            
         
        }

        void CustomerServiceClient_GetCustomerCompleted(object sender, GetCustomerCompletedEventArgs e)
        {
            _customer = e.Result.Customer;
            this.RaisePropertyChanged(cust=> cust.Customer);
            //InvokePropertyChanged(new PropertyChangedEventArgs("Customer"));
            DataLoaded = true;
        }

        private bool dataLoaded;
        public bool DataLoaded
        {
            get{return dataLoaded;}
            private set{dataLoaded = value; RaisePropertyChanged("DataLoaded");}
        }

        public CustomerDTO Customer
        {
            get
            {
                return _customer;
            }
        }

        public DelegateCommand<object> SaveCustomerCommand
        {
            get { return _saveCustomerCommand; }
            set { _saveCustomerCommand = value; }
        }
    }
}
