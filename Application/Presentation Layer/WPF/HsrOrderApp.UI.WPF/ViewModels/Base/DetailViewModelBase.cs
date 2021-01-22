#region

using System.Threading;
using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.UI.WPF.Properties;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Base
{
    public delegate void SavingResultEventHandler(bool SaveSuccessFull, string validationErrorMessage);

    public abstract class DetailViewModelBase<T> : ViewModelBase, IDetailViewModelBase where T : DTOBase
    {
        #region Fields

        private bool _isNew = false;
        private T _model;
        private CommandViewModel _saveCommand;

        #endregion

        #region Constructor

        public DetailViewModelBase(T model, bool isNew)
        {
            this._isNew = isNew;
            this._model = model;
        }

        #endregion

        #region Model

        public T Model
        {
            get { return this._model; }
            set
            {
                if (_model != value)
                {
                    SendPropertyChanging("Model");
                    _model = value;
                    SendPropertyChanged("Model");
                }
            }
        }

        #endregion

        #region IsNewFlag

        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }

        #endregion

        #region Data saving event handling

        public CommandViewModel SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new CommandViewModel(Strings.SaveCommand, new RelayCommand(
                                                                                 param => SaveDataValidationProxy(),
                                                                                 param => CanSaveData()
                                                                                 ));
                }
                return _saveCommand;
            }
        }

        public event SavingResultEventHandler SavingResultEvent;

        protected void SaveDataValidationProxy()
        {
            if (_model != null && _model.IsValid)
            {
                SaveData();
            }
            else
            {
                if (_model != null)
                    OnSavingResult(false, _model.GetValidationResultsAsString(Thread.CurrentThread.CurrentCulture));
                else
                    OnSavingResult(false, null);
            }
            _isNew = false;
        }

        protected virtual void SaveData()
        {
            SaveCommandExecuted();
        }

        public void SaveCommandExecuted()
        {
            if (this.SavingResultEvent != null)
            {
                SavingResultEvent(true, null);
            }
        }

        protected bool CanSaveData()
        {
            return _isNew || (_model != null && _model.IsValid);
        }

        public void OnSavingResult(bool result, string validationErrorMessage)
        {
            if (this.SavingResultEvent != null)
            {
                this.SavingResultEvent(result, validationErrorMessage);
            }
        }

        #endregion
    }
}