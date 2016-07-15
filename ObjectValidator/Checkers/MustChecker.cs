using ObjectValidator.Common;
using ObjectValidator.Interfaces;
using System;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class MustChecker<T, TProperty> : BaseChecker<T, TProperty>
    {
        protected Func<TProperty, bool> m_MustBeTrue;

        public MustChecker(Func<TProperty, bool> func)
        {
            ParamHelper.CheckParamNull(func, "func", "Can't be null");
            m_MustBeTrue = func;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, TProperty value, string name, string error)
        {
            if (!m_MustBeTrue(value))
            {
                AddFailure(result, name, value, error);
            }
            return Task.FromResult(result);
        }
    }
}