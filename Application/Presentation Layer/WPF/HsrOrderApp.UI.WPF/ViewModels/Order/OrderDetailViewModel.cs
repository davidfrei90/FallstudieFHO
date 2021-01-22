#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Order
{
    public class OrderDetailViewModel : DetailViewModelBase<OrderDTO>
    {
        #region Fields

        private IList<CustomerListDTO> _customers;
        private OrderItemViewModel _listViewModel;

        #endregion

        public OrderDetailViewModel(OrderDTO order, bool isNew) : base(order, isNew)
        {
            this.DisplayName = Strings.OrderDetailViewModel_DisplayName;
        }

        public int CustomerId
        {
            get { return this.Model.CustomerId; }
            set
            {
                if (value != this.Model.CustomerId)
                {
                    SendPropertyChanging("CustomerId");
                    CustomerListDTO cdto = _customers.FirstOrDefault(c => c.Id == value);
                    if (cdto != null)
                    {
                        this.Model.CustomerId = value;
                        this.Model.CustomerName = cdto.FullName;
                    }
                    SendPropertyChanged("CustomerId");
                }
            }
        }


        public DateTime? OrderDate
        {
            get
            {
                if (!this.Model.OrderDate.HasValue)
                {
                    this.Model.OrderDate = DateTime.Now;
                }
                return this.Model.OrderDate.Value;
            }
            set
            {
                if (value != this.Model.OrderDate)
                {
                    SendPropertyChanging("OrderDate");
                    this.Model.OrderDate = value;
                    SendPropertyChanged("OrderDate");
                }
            }
        }

        public OrderItemViewModel ListViewModel
        {
            get
            {
                if (this._listViewModel == null)
                {
                    this._listViewModel = new OrderItemViewModel(this.Model);
                    this._listViewModel.LoadCommand.Command.Execute(null);
                }
                return _listViewModel;
            }
        }

        protected override void Load()
        {
            base.Load();
            InitaliseAdditionalDataSources();
        }

        protected override void SaveData()
        {
            Service.StoreOrder(Model);
            SaveCommandExecuted();
        }

        #region Additional Datasources

        public IList<CustomerListDTO> Customers
        {
            get { return _customers; }
        }

        private void InitaliseAdditionalDataSources()
        {
            _customers = Service.GetAllCustomers();
        }

        #endregion
    }
}