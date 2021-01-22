#region

using HsrOrderApp.BL.Core.BusinessExceptions;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using Microsoft.Practices.EnterpriseLibrary.Validation;

#endregion

namespace HsrOrderApp.BL.DTOAdapters.Helper
{
    public class ValidationHelper
    {
        public static void Validate(IValidableObject obj)
        {
            ValidationResults results = obj.Validate();
            if (results.IsValid == false)
            {
                throw new ValidationException(results, obj.GetType().Name);
            }
        }
    }
}