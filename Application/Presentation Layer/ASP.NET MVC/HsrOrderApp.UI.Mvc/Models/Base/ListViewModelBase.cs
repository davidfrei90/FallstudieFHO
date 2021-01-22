using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.UI.Mvc.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HsrOrderApp.UI.Mvc.Models
{
    public abstract class ListViewModelBase<TC> : ViewModelBase where TC : DTOBase
    {
        #region Fields

        private List<TC> _items;
        private TC _selectedItem;
        protected DTOParentObject ParentObject;

        #endregion

        #region Constructor

        protected ListViewModelBase() { }

        protected ListViewModelBase(DTOParentObject parentObject)
        {
            ParentObject = parentObject;
        }

        #endregion

        #region Item List

        public TC SelectedItem
        {
            get
            {
                if (_selectedItem == null)
                    _selectedItem = Items.FirstOrDefault();
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                }
            }
        }

        public List<TC> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<TC>();
                }
                return _items;
            }
            set { _items = value; }
        }

        #endregion
    }
}