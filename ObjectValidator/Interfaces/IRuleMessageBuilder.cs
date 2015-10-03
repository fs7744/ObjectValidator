using System;
using System.Linq.Expressions;

namespace ObjectValidator.Interfaces
{
    public interface IRuleMessageBuilder<T, TValue>
    {
        string ValueName { get; set; }

        string Error { get; set; }

        Func<ValidateContext, bool> Condition { get; set; }

        IRuleBuilder<T, TProperty> ThenRuleFor<TProperty>(Expression<Func<T, TProperty>> expression);
    }
}