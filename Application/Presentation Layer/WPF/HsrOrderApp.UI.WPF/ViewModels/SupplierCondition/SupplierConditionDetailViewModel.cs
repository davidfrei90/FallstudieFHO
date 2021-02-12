#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.SupplierCondition
{
    public class SupplierConditionDetailViewModel : DetailViewModelBase<SupplierConditionDTO>
    {
        #region Fields

        private IList<SupplierListDTO> _suppliers;
        //private SupplierConditionItemViewModel _listViewModel;

        #endregion

        public SupplierConditionDetailViewModel(SupplierConditionDTO supplierCondition, bool isNew) : base(supplierCondition, isNew)
        {
            this.DisplayName = Strings.SupplierConditionItemDetailViewModel_DisplayName;
        }

        public int SupplierId
        {
            get { return this.Model.SupplierId; }
            set
            {
                if (value != this.Model.SupplierId)
                {
                    SendPropertyChanging("CustomerId");
                    SupplierListDTO cdto = _suppliers.FirstOrDefault(c => c.Id == value);
                    if (cdto != null)
                    {
                        this.Model.SupplierId = value;
                        this.Model.SupplierName = cdto.Name;
                    }
                    SendPropertyChanged("SupplierId");
                }
            }
        }


        //public DateTime? OrderDate
        //{
        //    get
        //    {
        //        if (!this.Model.OrderDate.HasValue)
        //        {
        //            this.Model.OrderDate = DateTime.Now;
        //        }
        //        return this.Model.OrderDate.Value;
        //    }
        //    set
        //    {
        //        if (value != this.Model.OrderDate)
        //        {
        //            SendPropertyChanging("OrderDate");
        //            this.Model.OrderDate = value;
        //            SendPropertyChanged("OrderDate");
        //        }
        //    }
        //}

        //public SupplierConditionItemViewModel ListViewModel
        //{
        //    get
        //    {
        //        if (this._listViewModel == null)
        //        {
        //            this._listViewModel = new SupplierConditionItemViewModel(this.Model);
        //            this._listViewModel.LoadCommand.Command.Execute(null);
        //        }
        //        return _listViewModel;
        //    }
        //}

        protected override void Load()
        {
            base.Load();
            InitaliseAdditionalDataSources();
        }

        protected override void SaveData()
        {
            Service.StoreSupplierCondition(Model);
            SaveCommandExecuted();
        }

        #region Additional Datasources

        public IList<SupplierListDTO> Suppliers
        {
            get { return _suppliers; }
        }

        private void InitaliseAdditionalDataSources()
        {
            _suppliers = Service.GetAllSuppliers();
        }

        #endregion
    }
}