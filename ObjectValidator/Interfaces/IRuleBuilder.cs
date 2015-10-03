using System;
using System.Linq.Expressions;

namespace ObjectValidator.Interfaces
{
    public interface IRuleBuilder<T, TValue> : IValidateRuleBuilder, IRuleMessageBuilder<T, TValue>, IFluentRuleBuilder<T, TValue>
    {
        Func<object, TValue> ValueGetter { get; }

        void SetValueGetter(Expression<Func<T, TValue>> expression);
    }
}