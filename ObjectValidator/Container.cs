using Microsoft.Extensions.DependencyInjection;
using ObjectValidator.Base;
using ObjectValidator.Checkers;
using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator
{
    public static class Container
    {
        public static IServiceProvider CurrentScope { get; set; }

        public static void Init(Action<IServiceCollection> action)
        {
            ParamHelper.CheckParamNull(action, "action", "Can't be null");
            var builder = new ServiceCollection();
            action(builder);
            CurrentScope = builder.BuildServiceProvider();
        }

        public static void Init()
        {
            Init(builder =>
            {
                builder.AddSingleton<IRuleSelector, RuleSelector>();
                builder.AddTransient(typeof(IRuleBuilder<,>), typeof(RuleBuilder<,>));
                builder.AddTransient(c => new ValidateContext() { RuleSelector = c.GetService<IRuleSelector>() });
                builder.AddTransient<IValidateRule, ValidateRule>();
                builder.AddTransient<IValidateResult, ValidateResult>();
                builder.AddTransient(typeof(IValidatorBuilder<>), typeof(ValidatorBuilder<>));
                builder.AddTransient<IValidatorSetter, Validator>();
                builder.AddTransient<CollectionValidateRule>();
                builder.AddTransient(typeof(EachChecker<,>));
                builder.AddTransient(typeof(CollectionRuleBuilder<,>));
            });
        }

        public static T Resolve<T>()
        {
            return CurrentScope.GetService<T>();
        }
    }
}