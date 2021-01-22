#region

using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Validation;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Base
{
    public interface IValidateDTO
    {
        bool IsValid { get; }
        ValidationResults Validate();
        string GetValidationResultsAsString(CultureInfo cultureInfo);
    }
}