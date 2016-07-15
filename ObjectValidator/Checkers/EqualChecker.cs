using ObjectValidator.Interfaces;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class EqualChecker<T, TProperty> : NotEqualChecker<T, TProperty>
    {
        public EqualChecker(TProperty value) : base(value)
        {
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, TProperty value, string name, string error)
        {
            if (!Compare(m_EqualEalue, value))
            {
                AddFailure(result, name, value, error ?? string.Format("The value is not equal {0}", m_EqualEalue));
            }
            return Task.FromResult(result);
        }
    }
}