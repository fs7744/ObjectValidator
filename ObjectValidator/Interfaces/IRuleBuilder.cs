using System;
using System.Linq.Expressions;

namespace ObjectValidator.Interfaces
{
    public interface IRuleBuilder<T, TValue> : IRuleValueGetterBuilder<T, TValue>
    {
        Expression<Func<T, TValue>> ValueExpression { get; }

        void SetValueGetter(Expression<Func<T, TValue>> expression);
    }
}