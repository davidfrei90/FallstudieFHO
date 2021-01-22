#region

using System;

#endregion

namespace HsrOrderApp.BL.Core.BusinessExceptions
{
    public class ConcurrencyException : Exception, IBusinessException
    {
        public ConcurrencyException()
        {
        }

        public ConcurrencyException(string message) : base(message)
        {
        }

        public ConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override string Message
        {
            get { return "Text: " + base.Message; }
        }

        #region IBusinessException Members

        public string ExceptionType
        {
            get { return Consts.Concurrency; }
        }

        #endregion
    }
}