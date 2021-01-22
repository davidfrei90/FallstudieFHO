#region

using System;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;

#endregion

namespace HsrOrderApp.UI.PresentationLogic.ExceptionHandlers
{
    [ConfigurationElementType(typeof (CustomHandlerData))]
    public class ExceptionNotifier : IExceptionHandler
    {
        public ExceptionNotifier(NameValueCollection ignore)
        {
        }

        #region IExceptionHandler Members

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            ExceptionHandler.GetInstance().HandleException(exception);
            return new UIException();
        }

        #endregion
    }
}