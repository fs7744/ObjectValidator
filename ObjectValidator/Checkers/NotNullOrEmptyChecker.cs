using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public class NotNullOrEmptyChecker<T> : BaseChecker<T, string>
    {
        public override IValidateResult Validate(IValidateResult result, string value, string name, string error)
        {
            if (string.IsNullOrEmpty(value))
            {
                AddFailure(result, name, value, error ?? "Can't be null or empty");
            }
            return result;
        }
    }
}
