using System;
using System.Linq.Expressions;

namespace ObjectValidator.Interfaces
{
    public interface IValidatorBuilder<T>
    {
        IRuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression);

        void RuleSet(string ruleSet, Action<IValidatorBuilder<T>> action);

        IValidator Build();
    }
}