using ObjectValidator.Common;
using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator.Checkers
{
    public class MustChecker<T, TProperty> : BaseChecker<T, TProperty>
    {
        private Func<TProperty, bool> m_MustBeTrue;

        public MustChecker(Func<TProperty, bool> func)
        {
            ParamHelper.CheckParamNull(func, "func", "Can't be null");
            m_MustBeTrue = func;
        }

        public override IValidateResult Validate(IValidateResult result, TProperty value, string name, string error)
        {
            if (!m_MustBeTrue(value))
            {
                AddFailure(result, name, value, error);
            }
            return result;
        }
    }
}