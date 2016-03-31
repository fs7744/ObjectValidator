using System;

namespace ObjectValidator.Interfaces
{
    public interface IRuleValueGetterBuilder<T, out TValue> : IValidateRuleBuilder, IRuleMessageBuilder<T, TValue>, IFluentRuleBuilder<T, TValue>
    {
        Func<object, TValue> ValueGetter { get; }
    }
}