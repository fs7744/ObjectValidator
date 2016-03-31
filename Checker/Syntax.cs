using ObjectValidator.Checkers;
using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace ObjectValidator
{
    public static partial class Syntax
    {
        public static IRuleMessageBuilder<T, Nullable<TProperty>> NotNull<T, TProperty>(this IFluentRuleBuilder<T, Nullable<TProperty>> builder)
        {
            return new NullableNotNullChecker<T, TProperty>().SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, DateTime> GreaterThan<T>(this IFluentRuleBuilder<T, DateTime> builder, DateTime value)
        {
            return new GreaterThanDateTimeChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, DateTime> GreaterThanOrEqual<T>(this IFluentRuleBuilder<T, DateTime> builder, DateTime value)
        {
            return new GreaterThanOrEqualDateTimeChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, DateTime> LessThan<T>(this IFluentRuleBuilder<T, DateTime> builder, DateTime value)
        {
            return new LessThanDateTimeChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, DateTime> LessThanOrEqual<T>(this IFluentRuleBuilder<T, DateTime> builder, DateTime value)
        {
            return new LessThanOrEqualDateTimeChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, TProperty> In<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder, IEnumerable<TProperty> value)
        {
            var a = new List<string>();
            return new InListChecker<T, TProperty>(value).SetValidate(builder);
        }
    }
}