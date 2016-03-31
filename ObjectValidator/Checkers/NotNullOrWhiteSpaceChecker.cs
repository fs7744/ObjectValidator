using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public class NotNullOrWhiteSpaceChecker<T> : BaseChecker<T, string>
    {
        public override IValidateResult Validate(IValidateResult result, string value, string name, string error)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                AddFailure(result, name, value, error ?? "Can't be null or empty or whitespace");
            }
            return result;
        }
    }
}