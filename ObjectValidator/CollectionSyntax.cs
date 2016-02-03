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
            var checker = new EachChecker<T, TProperty>();
            checker.SetValidate(builder);
            var a = new CollectionRuleBuilder<T, TProperty>();
            a.EachChecker = checker;
            var b = Container.Resolve<IRuleBuilder<TProperty, TProperty>>();
            b.SetValueGetter(i => i);
            a.ElementRuleBuilderList.Add(b);
            var build = builder as IRuleValueGetterBuilder<T, IEnumerable<TProperty>>;
            build.NextRuleBuilderList.Add(a);
            return b;
        }

        #endregion RuleChecker
    }
}