#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging, IDisposable
    {
        #region Fields

        private readonly Dispatcher _dispatcher;
        private string _displayName;
        private CommandViewModel _loadCommand;
        private PropertyChangedEventHandler _propertyChangedEvent;
        private PropertyChangingEventHandler _propertyChangingEvent;
        private IServiceFacade _service;

        #endregion

        #region Constructor

        protected ViewModelBase()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public string DisplayName
        {
            get { return this._displayName; }
            protected set
            {
                if (value != _displayName)
                {
                    SendPropertyChanging("DisplayName");
                    _displayName = value;
                    SendPropertyChanged("DisplayName");
                }
            }
        }

        public IServiceFacade Service
        {
            get
            {
                if (this._service == null)
                {
                    this._service = ServiceFacade.GetInstance();
                }
                return this._service;
            }
            set { this._service = value; }
        }

        #endregion

        #region Load command

        public CommandViewModel LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new CommandViewModel(Strings.SaveCommand, new RelayCommand(
                                                                                 param => Load(),
                                                                                 param => CanLoad()
                                                                                 ));
                }
                return _loadCommand;
            }
        }

        protected virtual void Load()
        {
            // Nothing to do here. Override this method to implement Load-Command.
        }

        protected virtual bool CanLoad()
        {
            // Nothing to do here. Override this method to implement Load-Command.
            return true;
        }

        #endregion

        #region Property change handling

        #region Events

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                VerifyCalledOnUiThread();
                _propertyChangedEvent += value;
            }
            remove
            {
                VerifyCalledOnUiThread();
                _propertyChangedEvent -= value;
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging
        {
            add
            {
                VerifyCalledOnUiThread();
                _propertyChangingEvent += value;
            }
            remove
            {
                VerifyCalledOnUiThread();
                _propertyChangingEvent -= value;
            }
        }

        #endregion

        #endregion

        public Dispatcher Dispatcher
        {
            get { return _dispatcher; }
        }

        [Conditional("Debug")]
        protected void VerifyCalledOnUiThread()
        {
            Debug.Assert(Dispatcher.CurrentDispatcher == _dispatcher, "Call must be made on UI thread.");
        }

        protected void SendPropertyChanging(string propertyName)
        {
            VerifyCalledOnUiThread();
            if (_propertyChangingEvent != null)
            {
                _propertyChangingEvent(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        protected void SendPropertyChanged(string propertyName)
        {
            VerifyCalledOnUiThread();
            if (_propertyChangedEvent != null)
            {
                _propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _service.Dispose();
        }

        #endregion
    }
}