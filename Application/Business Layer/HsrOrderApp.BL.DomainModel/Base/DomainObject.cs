#region

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel.HelperObjects
{
    public class DomainObject : IValidableObject
    {
        [RangeValidator(typeof (ulong), "0", RangeBoundaryType.Inclusive, null, RangeBoundaryType.Ignore)]
        public ulong Version { get; set; }

        public virtual bool IsNew
        {
            get { return Version == 0; }
        }

        #region IValidableObject Members

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

        #endregion
    }
}