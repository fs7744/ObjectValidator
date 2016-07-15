using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class CustomChecker<T, TProperty> : BaseChecker<T, TProperty>
    {
        private Func<TProperty, IEnumerable<ValidateFailure>> m_Func;

        public CustomChecker(Func<TProperty, IEnumerable<ValidateFailure>> func)
        {
            ParamHelper.CheckParamNull(func, "func", "Can't be null");
            m_Func = func;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, TProperty value, string name, string error)
        {
            var res = m_Func(value);
            if (res != null)
                result.Merge(res);
            return Task.FromResult(result);
        }
    }
}