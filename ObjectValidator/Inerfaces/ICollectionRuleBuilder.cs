using System.Collections.Generic;

namespace ObjectValidator.Interfaces
{
    public interface ICollectionRuleBuilder<T, TValue> : IRuleBuilder<T, IEnumerable<TValue>>
    {
        List<IRuleBuilder<TValue, TValue>> ElementRuleBuilderList { get; }
    }
}