using ObjectValidator.Checkers;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;

namespace ObjectValidator
{
    public static partial class Syntax
    {
        #region RuleChecker

        public static IRuleMessageBuilder<T, IEnumerable<TProperty>> AllMust<T, TProperty>(this IFluentRuleBuilder<T, IEnumerable<TProperty>> builder, Func<TProperty, bool> func)
        {
            return new AllMustChecker<T, TProperty>(func).SetValidate(builder);
        }

        #endregion RuleChecker
    }
}