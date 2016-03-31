using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator.Checkers
{
    public class MustNotChecker<T, TProperty> : MustChecker<T, TProperty>
    {
        public MustNotChecker(Func<TProperty, bool> func) : base(func)
        {
        }

        public override IValidateResult Validate(IValidateResult result, TProperty value, string name, string error)
        {
            if (m_MustBeTrue(value))
            {
                AddFailure(result, name, value, error);
            }
            return result;
        }
    }
}