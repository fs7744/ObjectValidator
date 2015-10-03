using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator.Checkers
{
    public class NotEqualChecker<T, TProperty> : BaseChecker<T, TProperty>
    {
        private TProperty m_EqualEalue;

        public NotEqualChecker(TProperty value)
        {
            m_EqualEalue = value;
        }

        public override IRuleBuilder<T, TProperty> SetValidateFunc(IRuleBuilder<T, TProperty> builder)
        {
            builder.ValidateFunc = (context, name, error) =>
            {
                var value = builder.ValueGetter(context.ValidateObject);
                var result = Container.Resolve<IValidateResult>();
                if (Compare(m_EqualEalue, value))
                {
                    result.Failures.Add(new ValidateFailure()
                    {
                        Name = name,
                        Value = value,
                        Error = error ?? string.Format("The value is equal {0}", m_EqualEalue)
                    });
                }
                return result;
            };
            return builder;
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