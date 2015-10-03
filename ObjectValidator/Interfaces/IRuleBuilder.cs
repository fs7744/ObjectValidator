using System;
using System.Linq.Expressions;

namespace ObjectValidator.Interfaces
{
    public interface IRuleBuilder<T, TValue> : IValidateRuleBuilder
    {
        IValidateRuleBuilder NextRuleBuilder { get; set; }

        Func<object, TValue> ValueGetter { get; }

        Func<ValidateContext, string, string, IValidateResult> ValidateFunc { get; set; }

        void SetValueGetter(Expression<Func<T, TValue>> expression);
    }
}