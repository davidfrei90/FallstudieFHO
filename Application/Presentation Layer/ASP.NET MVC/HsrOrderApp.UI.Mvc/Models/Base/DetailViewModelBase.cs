using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.UI.Mvc.Models.Base;

namespace HsrOrderApp.UI.Mvc.Models
{
    public abstract class DetailViewModelBase<TC> : ViewModelBase where TC : DTOBase
    {
        #region Fields

        private bool _isNew = false;
        private TC _model;

        #endregion

        #region Constructor

        protected DetailViewModelBase() { }
        protected DetailViewModelBase(TC model, bool isNew)
        {
            IsNew = isNew;
            _model = model;
        }

        #endregion

        #region Properties

        public TC Model
        {
            get { return _model; }
            set
            {
                if (_model != value)
                {
                    _model = value;
                }
            }
        }
        public bool IsNew { get; set; }

        #endregion
    }
}