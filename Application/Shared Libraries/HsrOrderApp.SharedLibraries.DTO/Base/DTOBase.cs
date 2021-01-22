#region

using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Base
{
    [DataContract]
    public class DTOBase : IValidateDTO, IDataErrorInfo, INotifyPropertyChanged
    {
        public DTOBase()
        {
            this.Id = default(int);
        }

        private int _id;

        [DataMember]
        [NotNullValidator]
        public virtual int Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    this._id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }

        #region IDataErrorInfo Members

        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get { return DoValidation(columnName); }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IValidateDTO Members

        public virtual bool IsValid
        {
            get
            {
                try
                {
                    return this.Validate().IsValid;
                }
                catch
                {
                    return false;
                }
            }
        }

        public virtual ValidationResults Validate()
        {
            Validator validator = ValidationFactory.CreateValidator(this.GetType());
            return validator.Validate(this);
        }


        public string GetValidationResultsAsString(CultureInfo cultureInfo)
        {
            Validator validator = ValidationFactory.CreateValidator(this.GetType());
            ValidationResults results = validator.Validate(this);
            if (results.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            foreach (ValidationResult result in results)
            {
                builder.AppendLine(
                    string.Format(cultureInfo,
                                  "{0}: {1}",
                                  result.Key,
                                  result.Message));
            }
            return builder.ToString();
        }

        #endregion

        protected virtual string DoValidation(string columnName)
        {
            var results = Validate();
            foreach (var result in results)
                if (result.Key.Equals(columnName))
                    return result.Message;
            return string.Empty;
        }

        public static string GetPropertySymbol<TResult>(Expression<Func<TResult>> expr)
        {
            return ((MemberExpression) expr.Body).Member.Name;
        }

        protected virtual void OnPropertyChanged<TPropertyType>(Expression<Func<TPropertyType>> propertyExpr)
        {
            string propertyName = GetPropertySymbol(propertyExpr);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class SymbolExtensions
    {
        public static string GetPropertySymbol<T, TR>(this T obj, Expression<Func<T, TR>> expr)
        {
            return ((MemberExpression) expr.Body).Member.Name;
        }
    }
}