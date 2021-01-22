using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.Mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HsrOrderApp.UI.Mvc.App_Start
{
    public class WcfUserValidator : IIdentityValidator<WebApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(WebApplicationUser item)
        {
            return Task.Run(() => new IdentityResult(Validate(item.UserDto)));
        }

        private IEnumerable<string> Validate(UserDTO user)
        {
            Validator validator = ValidationFactory.CreateValidator(typeof(UserDTO));

            foreach (ValidationResult validationResult in validator.Validate(user))
            {
                if (validationResult.NestedValidationResults != null)
                {
                    foreach (ValidationResult result in validationResult.NestedValidationResults)
                    {
                        yield return result.Message;
                    }
                }
                yield return validationResult.Message;
            }
        }

    }
}
