using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectValidator.Interfaces
{
    public interface ICollectionRuleBuilder<T, TValue> : IRuleBuilder<T, IEnumerable<TValue>>
    {
        List<IRuleBuilder<TValue, TValue>> ElementRuleBuilderList { get; }
    }
}
