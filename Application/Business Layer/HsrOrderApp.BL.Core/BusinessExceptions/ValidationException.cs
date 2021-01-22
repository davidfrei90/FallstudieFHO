#region

using System;
using System.Globalization;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;

#endregion

namespace HsrOrderApp.BL.Core.BusinessExceptions
{
    public class ValidationException : Exception, IBusinessException
    {
        private string _businessObjectType;
        private ValidationResults _results;

        public ValidationException(ValidationResults results, string businessObjectType)
        {
            this._results = results;
            this._businessObjectType = businessObjectType;
        }

        public override string Message
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(_businessObjectType + " is not valid:");
                foreach (ValidationResult result in _results)
                {
                    builder.AppendLine(
                        string.Format(CultureInfo.CurrentCulture,
                                      "{0}: {1}",
                                      result.Key,
                                      result.Message));
                }
                return builder.ToString();
            }
        }

        #region IBusinessException Members

        public string ExceptionType
        {
            get { return Consts.Validation; }
        }

        #endregion
    }
}