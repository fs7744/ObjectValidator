using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;

namespace ObjectValidator.Checkers
{
    public class CustomChecker<T, TProperty> : BaseChecker<T, TProperty>
    {
        private Func<TProperty, string, string, IEnumerable<ValidateFailure>> m_Func;

        public CustomChecker(Func<TProperty, string, string, IEnumerable<ValidateFailure>> func)
        {
            m_Func = func;
        }

        public override IValidateResult Validate(IValidateResult result, TProperty value, string name, string error)
        {
            var res = m_Func(value, name, error);
            if (res != null)
                result.Merge(res);
            return result;
        }
    }
}
