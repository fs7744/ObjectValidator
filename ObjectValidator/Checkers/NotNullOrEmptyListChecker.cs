using ObjectValidator.Interfaces;
using System.Collections;

namespace ObjectValidator.Checkers
{
    public class NotNullOrEmptyListChecker<T> : BaseChecker<T, IEnumerable>
    {
        public override IValidateResult Validate(IValidateResult result, IEnumerable value, string name, string error)
        {
            if (value == null || !value.GetEnumerator().MoveNext())
            {
                AddFailure(result, name, value, error ?? "Can't be null or empty");
            }
            return result;
        }
    }
}