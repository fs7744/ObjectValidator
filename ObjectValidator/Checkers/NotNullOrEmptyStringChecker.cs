using ObjectValidator.Interfaces;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class NotNullOrEmptyStringChecker<T> : BaseChecker<T, string>
    {
        public NotNullOrEmptyStringChecker(Validation validation) : base(validation)
        {
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, string value, string name, string error)
        {
            if (string.IsNullOrEmpty(value))
            {
                AddFailure(result, name, value, error ?? "Can't be null or empty");
            }
            return Task.FromResult(result);
        }
    }
}