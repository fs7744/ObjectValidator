using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator
{
    public static class Syntax
    {
        public static IRuleMessageBuilder<T, TProperty> Must<T, TProperty>(this IRuleBuilder<T, TProperty> rule, Func<TProperty, bool> func)
        {
            ParamHelper.CheckParamNull(func, "func", "Can't be null");
            rule.ValidateFunc = (context, name, error) =>
             {
                 var value = rule.ValueGetter(context.ValidateObject);
                 var result = Container.Resolve<IValidateResult>();
                 if (func(value))
                 {
                     result.Failures.Add(new ValidateFailure()
                     {
                         Name = name,
                         Value = value,
                         Error = error
                     });
                 }
                 return result;
             };
            return rule as IRuleMessageBuilder<T, TProperty>;
        }

        public static IRuleMessageBuilder<T, TProperty> When<T, TProperty>(this IRuleMessageBuilder<T, TProperty> rule, Func<TProperty, bool> func)
        {
            ParamHelper.CheckParamNull(func, "func", "Can't be null");
            rule.Condition = (context) =>
            {
                var value = (rule as IRuleBuilder<T, TProperty>).ValueGetter(context.ValidateObject);
                return func(value);
            };
            return rule;
        }

        public static IRuleMessageBuilder<T, TProperty> OverrideName<T, TProperty>(this IRuleMessageBuilder<T, TProperty> rule, string name)
        {
            rule.ValueName = name;
            return rule;
        }

        public static IRuleMessageBuilder<T, TProperty> OverrideError<T, TProperty>(this IRuleMessageBuilder<T, TProperty> rule, string error)
        {
            rule.Error = error;
            return rule;
        }

        //public class rule<T, TProperty> : IRuleBuilder<T, TProperty>
        //{
        //}

        //public Syntax()
        //{
        //    new rule<ValidateContext, string>().Must(i => i.StartsWith("s")).When(i => i.EndsWith("ok")).ThenRuleFor(i => i.Option).
        //}
    }
}