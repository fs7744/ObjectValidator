using ObjectValidator.Interfaces;
using System.Collections;

namespace ObjectValidator.Checkers
{
    public class NotNullOrEmptyListChecker<T, TProperty> : BaseChecker<T, TProperty> where TProperty : IEnumerable
    {
        public override IValidateResult Validate(IValidateResult result, TProperty value, string name, string error)
        {
            if (value == null || !value.GetEnumerator().MoveNext())
            {
                AddFailure(result, name, value, error ?? "Can't be null or empty");
            }
            return result;
        }
    }
}