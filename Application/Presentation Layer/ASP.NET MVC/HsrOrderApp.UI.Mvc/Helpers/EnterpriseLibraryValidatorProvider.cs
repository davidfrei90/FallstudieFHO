using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace HsrOrderApp.UI.Mvc.Helpers
{
    public class EnterpriseLibraryValidatorProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            Validator validator = ValidationFactory.CreateValidator(metadata.ModelType);

            if (validator != null)
            {
                yield return new EnterpriseLibraryValidatorWrapper(metadata, context, validator);
            }
        }
    }

    public class EnterpriseLibraryValidatorWrapper : ModelValidator
    {
        private Validator _validator;

        public EnterpriseLibraryValidatorWrapper(ModelMetadata metadata, ControllerContext context, Validator validator)
            : base(metadata, context)
        {
            _validator = validator;
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            var validationResults = _validator.Validate(Metadata.Model);
            return ConvertResults(validationResults);
        }

        private IEnumerable<ModelValidationResult> ConvertResults(IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults != null)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    if (validationResult.NestedValidationResults != null)
                    {
                        foreach (ModelValidationResult result in ConvertResults(validationResult.NestedValidationResults))
                        {
                            yield return result;
                        }
                    }

                    yield return new ModelValidationResult
                    {
                        Message = validationResult.Message,
                        MemberName = validationResult.Key
                    };
                }
            }
        }
    }
}