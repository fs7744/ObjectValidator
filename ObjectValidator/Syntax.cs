using ObjectValidator.Checkers;
using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ObjectValidator
{
    public static partial class Syntax
    {
        public const string EmailRegex = @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?";

        #region RuleChecker

        public static IRuleMessageBuilder<T, TProperty> In<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder, IEnumerable<TProperty> value)
        {
            var a = new List<string>();
            return new InListChecker<T, TProperty>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, TProperty?> NotNull<T, TProperty>(this IFluentRuleBuilder<T, TProperty?> builder) where TProperty : struct
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

        public static IRuleMessageBuilder<T, string> LengthEqual<T>(this IFluentRuleBuilder<T, string> builder, int length)
        {
            return new LengthChecker<T>(length, length).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> LengthGreaterThanOrEqual<T>(this IFluentRuleBuilder<T, string> builder, int length)
        {
            return new LengthChecker<T>(length, -1).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> LengthLessThanOrEqual<T>(this IFluentRuleBuilder<T, string> builder, int length)
        {
            return new LengthChecker<T>(-1, length).SetValidate(builder);
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
            return new RegexChecker<T>(EmailRegex).SetValidate(builder).OverrideError("The value is not email address");
        }

        public static IRuleMessageBuilder<T, string> NotNullOrEmpty<T>(this IFluentRuleBuilder<T, string> builder)
        {
            return new NotNullOrEmptyStringChecker<T>().SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> NotNullOrWhiteSpace<T>(this IFluentRuleBuilder<T, string> builder)
        {
            return new NotNullOrWhiteSpaceChecker<T>().SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, float> Between<T>(this IFluentRuleBuilder<T, float> builder, float min, float max)
        {
            return new BetweenFloatChecker<T>(min, max).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, int> Between<T>(this IFluentRuleBuilder<T, int> builder, int min, int max)
        {
            return new BetweenIntChecker<T>(min, max).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, double> Between<T>(this IFluentRuleBuilder<T, double> builder, double min, double max)
        {
            return new BetweenDoubleChecker<T>(min, max).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, decimal> Between<T>(this IFluentRuleBuilder<T, decimal> builder, decimal min, decimal max)
        {
            return new BetweenDecimalChecker<T>(min, max).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, long> Between<T>(this IFluentRuleBuilder<T, long> builder, long min, long max)
        {
            return new BetweenLongChecker<T>(min, max).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, float> GreaterThan<T>(this IFluentRuleBuilder<T, float> builder, float value)
        {
            return new GreaterThanFloatChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, int> GreaterThan<T>(this IFluentRuleBuilder<T, int> builder, int value)
        {
            return new GreaterThanIntChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, double> GreaterThan<T>(this IFluentRuleBuilder<T, double> builder, double value)
        {
            return new GreaterThanDoubleChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, decimal> GreaterThan<T>(this IFluentRuleBuilder<T, decimal> builder, decimal value)
        {
            return new GreaterThanDecimalChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, long> GreaterThan<T>(this IFluentRuleBuilder<T, long> builder, long value)
        {
            return new GreaterThanLongChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, TProperty> CustomCheck<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder, Func<TProperty, IEnumerable<ValidateFailure>> func)
        {
            return new CustomChecker<T, TProperty>(func).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, TProperty> NotNullOrEmpty<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder) where TProperty : IEnumerable
        {
            return new NotNullOrEmptyListChecker<T, TProperty>().SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, float> GreaterThanOrEqual<T>(this IFluentRuleBuilder<T, float> builder, float value)
        {
            return new GreaterThanOrEqualFloatChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, int> GreaterThanOrEqual<T>(this IFluentRuleBuilder<T, int> builder, int value)
        {
            return new GreaterThanOrEqualIntChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, double> GreaterThanOrEqual<T>(this IFluentRuleBuilder<T, double> builder, double value)
        {
            return new GreaterThanOrEqualDoubleChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, decimal> GreaterThanOrEqual<T>(this IFluentRuleBuilder<T, decimal> builder, decimal value)
        {
            return new GreaterThanOrEqualDecimalChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, long> GreaterThanOrEqual<T>(this IFluentRuleBuilder<T, long> builder, long value)
        {
            return new GreaterThanOrEqualLongChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, float> LessThan<T>(this IFluentRuleBuilder<T, float> builder, float value)
        {
            return new LessThanFloatChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, int> LessThan<T>(this IFluentRuleBuilder<T, int> builder, int value)
        {
            return new LessThanIntChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, double> LessThan<T>(this IFluentRuleBuilder<T, double> builder, double value)
        {
            return new LessThanDoubleChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, decimal> LessThan<T>(this IFluentRuleBuilder<T, decimal> builder, decimal value)
        {
            return new LessThanDecimalChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, long> LessThan<T>(this IFluentRuleBuilder<T, long> builder, long value)
        {
            return new LessThanLongChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, float> LessThanOrEqual<T>(this IFluentRuleBuilder<T, float> builder, float value)
        {
            return new LessThanOrEqualFloatChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, int> LessThanOrEqual<T>(this IFluentRuleBuilder<T, int> builder, int value)
        {
            return new LessThanOrEqualIntChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, double> LessThanOrEqual<T>(this IFluentRuleBuilder<T, double> builder, double value)
        {
            return new LessThanOrEqualDoubleChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, decimal> LessThanOrEqual<T>(this IFluentRuleBuilder<T, decimal> builder, decimal value)
        {
            return new LessThanOrEqualDecimalChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, long> LessThanOrEqual<T>(this IFluentRuleBuilder<T, long> builder, long value)
        {
            return new LessThanOrEqualLongChecker<T>(value).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> NotRegex<T>(this IFluentRuleBuilder<T, string> builder, string pattern)
        {
            return new NotRegexChecker<T>(pattern).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> NotRegex<T>(this IFluentRuleBuilder<T, string> builder, string pattern, RegexOptions options)
        {
            return new NotRegexChecker<T>(pattern, options).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, string> NotRegex<T>(this IFluentRuleBuilder<T, string> builder, Regex regex)
        {
            return new NotRegexChecker<T>(regex).SetValidate(builder);
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

        public static IRuleMessageBuilder<T, string> LengthEqual<T>(this IRuleMessageBuilder<T, string> builder, int length)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LengthEqual(length);
        }

        public static IRuleMessageBuilder<T, string> LengthGreaterThanOrEqual<T>(this IRuleMessageBuilder<T, string> builder, int length)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LengthGreaterThanOrEqual(length);
        }

        public static IRuleMessageBuilder<T, string> LengthLessThanOrEqual<T>(this IRuleMessageBuilder<T, string> builder, int length)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Length(-1, length);
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

        public static IRuleMessageBuilder<T, float> Between<T>(this IRuleMessageBuilder<T, float> builder, float min, float max)
        {
            var ruleBuilder = builder as IRuleBuilder<T, float>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Between(min, max);
        }

        public static IRuleMessageBuilder<T, int> Between<T>(this IRuleMessageBuilder<T, int> builder, int min, int max)
        {
            var ruleBuilder = builder as IRuleBuilder<T, int>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Between(min, max);
        }

        public static IRuleMessageBuilder<T, double> Between<T>(this IRuleMessageBuilder<T, double> builder, double min, double max)
        {
            var ruleBuilder = builder as IRuleBuilder<T, double>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Between(min, max);
        }

        public static IRuleMessageBuilder<T, decimal> Between<T>(this IRuleMessageBuilder<T, decimal> builder, decimal min, decimal max)
        {
            var ruleBuilder = builder as IRuleBuilder<T, decimal>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Between(min, max);
        }

        public static IRuleMessageBuilder<T, long> Between<T>(this IRuleMessageBuilder<T, long> builder, long min, long max)
        {
            var ruleBuilder = builder as IRuleBuilder<T, long>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).Between(min, max);
        }

        public static IRuleMessageBuilder<T, float> GreaterThan<T>(this IRuleMessageBuilder<T, float> builder, float value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, float>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).GreaterThan(value);
        }

        public static IRuleMessageBuilder<T, int> GreaterThan<T>(this IRuleMessageBuilder<T, int> builder, int value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, int>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).GreaterThan(value);
        }

        public static IRuleMessageBuilder<T, double> GreaterThan<T>(this IRuleMessageBuilder<T, double> builder, double value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, double>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).GreaterThan(value);
        }

        public static IRuleMessageBuilder<T, decimal> GreaterThan<T>(this IRuleMessageBuilder<T, decimal> builder, decimal value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, decimal>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).GreaterThan(value);
        }

        public static IRuleMessageBuilder<T, long> GreaterThan<T>(this IRuleMessageBuilder<T, long> builder, long value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, long>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).GreaterThan(value);
        }

        public static IRuleMessageBuilder<T, float> GreaterThanOrEqual<T>(this IRuleMessageBuilder<T, float> builder, float value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, float>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).GreaterThanOrEqual(value);
        }

        public static IRuleMessageBuilder<T, int> GreaterThanOrEqual<T>(this IRuleMessageBuilder<T, int> builder, int value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, int>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).GreaterThanOrEqual(value);
        }

        public static IRuleMessageBuilder<T, double> GreaterThanOrEqual<T>(this IRuleMessageBuilder<T, double> builder, double value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, double>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).GreaterThanOrEqual(value);
        }

        public static IRuleMessageBuilder<T, decimal> GreaterThanOrEqual<T>(this IRuleMessageBuilder<T, decimal> builder, decimal value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, decimal>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).GreaterThanOrEqual(value);
        }

        public static IRuleMessageBuilder<T, long> GreaterThanOrEqual<T>(this IRuleMessageBuilder<T, long> builder, long value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, long>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).GreaterThanOrEqual(value);
        }

        public static IRuleMessageBuilder<T, float> LessThan<T>(this IRuleMessageBuilder<T, float> builder, float value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, float>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LessThan(value);
        }

        public static IRuleMessageBuilder<T, int> LessThan<T>(this IRuleMessageBuilder<T, int> builder, int value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, int>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LessThan(value);
        }

        public static IRuleMessageBuilder<T, double> LessThan<T>(this IRuleMessageBuilder<T, double> builder, double value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, double>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LessThan(value);
        }

        public static IRuleMessageBuilder<T, decimal> LessThan<T>(this IRuleMessageBuilder<T, decimal> builder, decimal value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, decimal>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LessThan(value);
        }

        public static IRuleMessageBuilder<T, long> LessThan<T>(this IRuleMessageBuilder<T, long> builder, long value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, long>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LessThan(value);
        }

        public static IRuleMessageBuilder<T, float> LessThanOrEqual<T>(this IRuleMessageBuilder<T, float> builder, float value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, float>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LessThanOrEqual(value);
        }

        public static IRuleMessageBuilder<T, int> LessThanOrEqual<T>(this IRuleMessageBuilder<T, int> builder, int value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, int>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LessThanOrEqual(value);
        }

        public static IRuleMessageBuilder<T, double> LessThanOrEqual<T>(this IRuleMessageBuilder<T, double> builder, double value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, double>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LessThanOrEqual(value);
        }

        public static IRuleMessageBuilder<T, decimal> LessThanOrEqual<T>(this IRuleMessageBuilder<T, decimal> builder, decimal value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, decimal>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LessThanOrEqual(value);
        }

        public static IRuleMessageBuilder<T, long> LessThanOrEqual<T>(this IRuleMessageBuilder<T, long> builder, long value)
        {
            var ruleBuilder = builder as IRuleBuilder<T, long>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).LessThanOrEqual(value);
        }

        public static IRuleMessageBuilder<T, string> NotRegex<T>(this IRuleMessageBuilder<T, string> builder, string pattern)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).NotRegex(pattern);
        }

        public static IRuleMessageBuilder<T, string> NotRegex<T>(this IRuleMessageBuilder<T, string> builder, string pattern, RegexOptions options)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).NotRegex(pattern, options);
        }

        public static IRuleMessageBuilder<T, string> NotRegex<T>(this IRuleMessageBuilder<T, string> builder, Regex regex)
        {
            var ruleBuilder = builder as IRuleBuilder<T, string>;
            return ruleBuilder.ThenRuleFor(ruleBuilder.ValueExpression).NotRegex(regex);
        }

        #endregion NextRuleChecker
    }
}