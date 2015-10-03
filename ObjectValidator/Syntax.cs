using ObjectValidator.Checkers;
using ObjectValidator.Common;
using ObjectValidator.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace ObjectValidator
{
    public static class Syntax
    {
        public static IRuleMessageBuilder<T, TProperty> Must<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder, Func<TProperty, bool> func)
        {
            return new MustChecker<T, TProperty>(func).SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, TProperty> NotNull<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder)
        {
            return new NotNullChecker<T, TProperty>().SetValidate(builder);
        }

        public static IRuleMessageBuilder<T, TProperty> NotEqual<T, TProperty>(this IFluentRuleBuilder<T, TProperty> builder, TProperty value)
        {
            return new NotEqualChecker<T, TProperty>(value).SetValidate(builder);
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

        public static IRuleMessageBuilder<T, string> Email<T>(this IFluentRuleBuilder<T, string> builder, Regex regex)
        {
            return new RegexChecker<T>(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$")
                .SetValidate(builder);
        }

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
    }
}