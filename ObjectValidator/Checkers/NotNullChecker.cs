using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public class NotNullChecker<T, TProperty> : BaseChecker<T, TProperty>
    {
        public override IValidateResult Validate(IValidateResult result, TProperty value, string name, string error)
        {
            if (value == null)
            {
                AddFailure(result, name, value, error ?? "Can't be null");
            }
            return result;
        }
    }
}