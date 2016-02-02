using ObjectValidator.Checkers;
using ObjectValidator.Common;
using ObjectValidator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ObjectValidator
{
    public static class Syntax
    {
        public const string EmailRegex = @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?";

        #region RuleChecker

        public static IRuleMessageBuilder<T, TProperty> Must<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder, Func<TProperty, bool> func)
        {
            return new MustChecker<T, TProperty>(func).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, TProperty> MustNot<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder, Func<TProperty, bool> func)
        {
            return new MustNotChecker<T, TProperty>(func).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, TProperty> NotNull<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder)
        {
            return new NotNullChecker<T, TProperty>().SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, TProperty> NotEqual<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder, TProperty value)
        {
            return new NotEqualChecker<T, TProperty>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, TProperty> Equal<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder, TProperty value)
        {
            return new EqualChecker<T, TProperty>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> Length<T>(this IFluentRuleBuilder<T, string> builder, int min, int max)
        {
            return new LengthChecker<T>(min, max).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> Regex<T>(this IFluentRuleBuilder<T, string> builder, string pattern)
        {
            return new RegexChecker<T>(pattern).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> Regex<T>(this IFluentRuleBuilder<T, string> builder, string pattern, RegexOptions options)
        {
            return new RegexChecker<T>(pattern, options).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> Regex<T>(this IFluentRuleBuilder<T, string> builder, Regex regex)
        {
            return new RegexChecker<T>(regex).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> Email<T>(this IFluentRuleBuilder<T, string> builder)
        {
            return new RegexChecker<T>(EmailRegex).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> NotNullOrEmpty<T, string>(this IFluentRuleBuilder<T, string> builder)
        {
            return new NotNullOrEmptyChecker<T>().SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, int> Between<T, int>(this IFluentRuleBuilder<T, int> builder, int min, int max)
        {
            return new BetweenIntChecker<T>(min, max).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, double> Between<T, double>(this IFluentRuleBuilder<T, double> builder, double min, double max)
        {
            return new BetweenDoubleChecker<T>(min, max).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, decimal> Between<T, decimal>(this IFluentRuleBuilder<T, decimal> builder, decimal min, decimal max)
        {
            return new BetweenDecimalChecker<T>(min, max).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, long> Between<T, long>(this IFluentRuleBuilder<T, long> builder, long min, long max)
        {
            return new BetweenLongChecker<T>(min, max).SetValidate(builder);
        }

        #endregion RuleChecker

        #region RuleMessageSetter

        public static IRuleMessageBuilder<T, TProperty> When<T, TProperty>(this IRuleMessageBuilder<T, TProperty> builder, Func<TProperty, bool> func)
        {
            ParamHelper.CheckParamNull(func, "func", "Can't be null");
            var ruleBuilder = builder as IRuleBuilder<T, TProperty>;
            ruleBuilder.Condition = (context) =>
            {
                var value = ruleBuilder.ValueGetter(context.ValidateObject);
                return func(value);
            };
            return builder;
        }

        public static IRuleMessageBuilder<T, TProperty> OverrideName<T, TProperty>(this IRuleMessageBuilder<T, TProperty> builder, string name)
        {
            (builder as IValidateRuleBuilder).ValueName = name;
            return builder;
        }

        public static IRuleMessageBuilder<T, TProperty> OverrideError<T, TProperty>(this IRuleMessageBuilder<T, TProperty> builder, string error)
        {
            (builder as IValidateRuleBuilder).Error = error;
            return builder;
        }

        #endregion RuleMessageSetter

        #region NextRuleChecker

        public static IRuleMessageBuilder<T, TProperty> Must<T, TProperty>(this IRuleMessageBuilder<T, TProperty> builder, Func<TProperty, bool> func)
        {
            var ruleBuilder = builder as IRuleBuilder<T, TProperty>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Must(func);
        }

        public static IRuleMessageBuilder<T, TProperty> MustNot<T, TProperty>(this IRuleMessageBuilder<T, TProperty> builder, Func<TProperty, bool> func)
        {
            var ruleBuilder = builder as IRuleBuilder<T, TProperty>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).MustNot(func);
        }

        public static IRuleMessageBuilder<T, string> Length<T>(this IRuleMessageBuilder<T, string> builder, int min, int max)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Length(min, max);
        }

        public static IRuleMessageBuilder<T, TProperty> NotEqual<T, TProperty>(this IRuleMessageBuilder<T, TProperty> builder, TProperty value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, TProperty>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).NotEqual(value);
        }

        public static IRuleMessageBuilder<T, TProperty> Equal<T, TProperty>(this IRuleMessageBuilder<T, TProperty> builder, TProperty value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, TProperty>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Equal(value);
        }

        public static IRuleMessageBuilder<T, string> Regex<T>(this IRuleMessageBuilder<T, string> builder, string pattern)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Regex(pattern);
        }

        public static IRuleMessageBuilder<T, string> Regex<T>(this IRuleMessageBuilder<T, string> builder, string pattern, RegexOptions options)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Regex(pattern, options);
        }

        public static IRuleMessageBuilder<T, string> Regex<T>(this IRuleMessageBuilder<T, string> builder, Regex regex)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Regex(regex);
        }

        public static IRuleMessageBuilder<T, string> Email<T>(this IRuleMessageBuilder<T, string> builder)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Email();
        }

        public static IRuleMessageBuilder<T, int> Between<T, int>(this IRuleMessageBuilder<T, int> builder, int min, int max)
        {
            var ruleBuilder = builder as IRuleBuilder<T, int>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Between(min, max);
        }

        public static IRuleMessageBuilder<T, double> Between<T, double>(this IRuleMessageBuilder<T, double> builder, double min, double max)
        {
            var ruleBuilder = builder as IRuleBuilder<T, double>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Between(min, max);
        }

        public static IRuleMessageBuilder<T, decimal> Between<T, decimal>(this IRuleMessageBuilder<T, decimal> builder, decimal min, decimal max)
        {
            var ruleBuilder = builder as IRuleBuilder<T, decimal>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Between(min, max);
        }

        public static IRuleMessageBuilder<T, long> Between<T, long>(this IRuleMessageBuilder<T, long> builder, long min, long max)
        {
            var ruleBuilder = builder as IRuleBuilder<T, long>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Between(min, max);
        }

        #endregion NextRuleChecker
    }
}