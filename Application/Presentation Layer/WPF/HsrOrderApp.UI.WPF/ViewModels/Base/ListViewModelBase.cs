#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.UI.WPF.Properties;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Base
{
    public abstract class ListViewModelBase<TC> : ViewModelBase where TC : DTOBase
    {
        #region Fields

        private ReadOnlyCollection<CommandViewModel> _commands;
        private ObservableCollection<TC> _items;
        private TC _selectedItem;
        protected DTOParentObject ParentObject;

        #endregion

        #region Constructor

        protected ListViewModelBase()
        {
        }

        protected ListViewModelBase(DTOParentObject parentObject)
        {
            ParentObject = parentObject;
        }

        #endregion

        #region Data Loading

        protected override void Load()
        {
            Items.Clear();
            LoadData();
        }

        protected abstract void LoadData();

        #endregion

        #region Item List

        public TC SelectedItem
        {
            get
            {
                VerifyCalledOnUiThread();
                if (_selectedItem == null)
                    _selectedItem = Items.FirstOrDefault();
                return _selectedItem;
            }
            set
            {
                VerifyCalledOnUiThread();
                if (_selectedItem != value)
                {
                    SendPropertyChanging("SelectedItem");
                    _selectedItem = value;
                    SendPropertyChanged("SelectedItem");
                }
            }
        }

        public ObservableCollection<TC> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<TC>();
                }
                return _items;
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Returns a read-only list of commands 
        /// that the UI can display and execute.
        /// </summary>
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
        }

        protected virtual List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
                       {
                           new CommandViewModel(Strings.NewCommand, new RelayCommand(param => New(), param => CanNew())),
                           new CommandViewModel(Strings.EditCommand, new RelayCommand(param => Edit(), param => CanEdit())),
                           new CommandViewModel(Strings.DeleteCommand, new RelayCommand(param => Delete(), param => CanDelete()))
                       };
        }

        protected abstract void New();

        protected bool CanNew()
        {
            return Service != null;
        }

        protected abstract void Edit();

        protected bool CanEdit()
        {
            return SelectedItem != null && Service != null;
        }

        protected abstract void Delete();

        protected bool CanDelete()
        {
            return _selectedItem != null;
        }

        #endregion
    }
}