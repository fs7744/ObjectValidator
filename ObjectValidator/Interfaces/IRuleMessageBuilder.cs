using System;
using System.Linq.Expressions;

namespace ObjectValidator.Interfaces
{
    public interface IRuleMessageBuilder<T, TValue>
    {
        IFluentRuleBuilder<T, TProperty> ThenRuleFor<TProperty>(Expression<Func<T, TProperty>> expression);
    }
}