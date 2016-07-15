using ObjectValidator.Common;
using ObjectValidator.Interfaces;
using System;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class NotEqualChecker<T, TProperty> : BaseChecker<T, TProperty>
    {
        protected TProperty m_EqualEalue;

        public NotEqualChecker(TProperty value, Validation validation) : base(validation)
        {
            m_EqualEalue = value;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, TProperty value, string name, string error)
        {
            if (Compare(m_EqualEalue, value))
            {
                AddFailure(result, name, value, error ?? string.Format("The value is equal {0}", m_EqualEalue));
            }
            return Task.FromResult(result);
        }

        protected bool Compare(object comparisonValue, object propertyValue)
        {
            if (comparisonValue is IComparable && propertyValue is IComparable)
            {
                return Comparer.GetEqualsResult((IComparable)comparisonValue, (IComparable)propertyValue);
            }

            return comparisonValue == propertyValue;
        }
    }
}