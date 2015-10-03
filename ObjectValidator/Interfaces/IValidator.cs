using System.Collections.Generic;

namespace ObjectValidator.Interfaces
{
    public interface IValidator
    {
        IValidateResult Validate(ValidateContext context);
    }
}