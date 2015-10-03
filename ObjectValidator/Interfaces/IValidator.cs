using ObjectValidator.Entities;

namespace ObjectValidator.Interfaces
{
    public interface IValidator
    {
        IValidateResult Validate(ValidateContext context);
    }
}