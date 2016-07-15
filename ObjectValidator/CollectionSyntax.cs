using Microsoft.Extensions.DependencyInjection;
using ObjectValidator.Base;
using ObjectValidator.Checkers;
using ObjectValidator.Interfaces;
using System.Collections.Generic;

namespace ObjectValidator
{
    public static partial class Syntax
    {
        #region RuleChecker

        public static IFluentRuleBuilder<TProperty, TProperty> Each<T, TProperty>(this IFluentRuleBuilder<T, IEnumerable<TProperty>> builder)
        {
            var checker = builder.Validation.Provider.GetService<EachChecker<T, TProperty>>();
            checker.SetValidate(builder);
            var a = builder.Validation.Provider.GetService<CollectionRuleBuilder<T, TProperty>>();
            a.EachChecker = checker;
            var b = builder.Validation.Provider.GetService<IRuleBuilder<TProperty, TProperty>>();
            b.SetValueGetter(i => i);
            a.ElementRuleBuilderList.Add(b);
            var build = builder as IRuleValueGetterBuilder<T, IEnumerable<TProperty>>;
            build.NextRuleBuilderList.Add(a);
            return b;
        }

        #endregion RuleChecker
    }
}