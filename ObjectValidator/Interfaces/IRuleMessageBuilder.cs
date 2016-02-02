using System;
using System.Linq.Expressions;

namespace ObjectValidator.Interfaces
{
    public interface IRuleMessageBuilder<T, out TValue>
    {
        IFluentRuleBuilder<T, TProperty> ThenRuleFor<TProperty>(Expression<Func<T, TProperty>> expression);
    }
}