#region

using System;
using System.Globalization;
using System.ServiceModel;
using System.Text;
using HsrOrderApp.SharedLibraries.DTO.Faults;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;

#endregion

namespace HsrOrderApp.UI.PresentationLogic.ExceptionHandlers
{
    public delegate void ExceptionOccuredDelegate(string message, string title);

    public class ExceptionHandler
    {
        public event ExceptionOccuredDelegate ExceptionOccured;

        public bool HandleException(Exception ex)
        {
            if (ex is FaultException<ServiceFault>)
            {
                ServiceFault fault = ((FaultException<ServiceFault>) ex).Detail;
                switch (fault.FaultType)
                {
                    case FaultType.General:
                        FireEvent(ex.Message, "Error!");
                        break;
                    case FaultType.Validation:
                        FireEvent(ex.Message, "Validierung auf dem Server fehlgeschlagen!");
                        break;
                    case FaultType.Concurrency:
                        FireEvent("Eine andere Person hat diese Daten in der Zwischenzeit bearbeitet.", "Es ist ein Optimistic-Concurrency-Fehler aufgetreten!");
                        break;
                }
            }
            else if (ex is FaultException<ValidationFault>)
            {
                ValidationFault fault = ((FaultException<ValidationFault>) ex).Detail;
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Validierung auf dem Server fehlgeschlagen!");
                foreach (ValidationDetail result in fault.Details)
                {
                    builder.AppendLine(
                        string.Format(CultureInfo.CurrentCulture,
                                      "{0}: {1}",
                                      result.Key,
                                      result.Message));
                }
                FireEvent(builder.ToString(), "Error!");
            }
            else if (ex is UIException)
            {
                // Exception was already handled.
            }
            else
            {
                FireEvent(ex.Message, "Error!");
            }

            return true;
        }

        private void FireEvent(string message, string title)
        {
            if (this.ExceptionOccured != null)
            {
                this.ExceptionOccured(message, title);
            }
        }

        #region Singleton

        private static ExceptionHandler _instance;

        private ExceptionHandler()
        {
        }

        public static ExceptionHandler GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ExceptionHandler();
            }
            return _instance;
        }

        #endregion
    }
}