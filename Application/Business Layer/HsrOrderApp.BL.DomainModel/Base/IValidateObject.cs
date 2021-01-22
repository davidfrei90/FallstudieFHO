#region

using Microsoft.Practices.EnterpriseLibrary.Validation;

#endregion

namespace HsrOrderApp.BL.DomainModel.HelperObjects
{
    public interface IValidableObject
    {
        bool IsValid { get; }
        ValidationResults Validate();
    }
}