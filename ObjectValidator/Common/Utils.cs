using Microsoft.Extensions.DependencyInjection;
using ObjectValidator.Interfaces;
using System;
using System.Linq.Expressions;

namespace ObjectValidator.Common
{
    public static class Utils
    {
        public static IRuleBuilder<T, TProperty> RuleFor<T, TProperty>(this Validation validation, Expression<Func<T, TProperty>> expression)
        {
            ParamHelper.CheckParamNull(expression, "expression", "Can't be null");
            var builder = validation.Provider.GetService<IRuleBuilder<T, TProperty>>();
            builder.SetValueGetter(expression);
            return builder;
        }
    }
}