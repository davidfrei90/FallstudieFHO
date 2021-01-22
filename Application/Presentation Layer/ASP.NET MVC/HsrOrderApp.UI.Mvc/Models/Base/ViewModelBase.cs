using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HsrOrderApp.UI.Mvc.Models.Base
{
    public abstract class ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public string DisplayName { get; set; }

        #endregion
    }
}