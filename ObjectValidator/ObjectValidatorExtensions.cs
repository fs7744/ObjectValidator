using Microsoft.Extensions.DependencyInjection;
using ObjectValidator.Base;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;

namespace ObjectValidator
{
    public static class ObjectValidatorExtensions
    {
        public static IServiceCollection AddObjectValidator(this IServiceCollection sc)
        {
            sc.AddSingleton(c => new Validation(c));
            sc.AddSingleton<IRuleSelector, RuleSelector>();
            sc.AddTransient(typeof(IRuleBuilder<,>), typeof(RuleBuilder<,>));
            sc.AddTransient(c => new ValidateContext() { RuleSelector = c.GetService<IRuleSelector>() });
            sc.AddTransient<IValidateRule, ValidateRule>();
            sc.AddTransient<IValidateResult, ValidateResult>();
            sc.AddTransient(typeof(IValidatorBuilder<>), typeof(ValidatorBuilder<>));
            sc.AddTransient<IValidatorSetter, Validator>();
            sc.AddTransient<CollectionValidateRule>();
            sc.AddTransient(typeof(EachChecker<,>));
            sc.AddTransient(typeof(CollectionRuleBuilder<,>));
            return sc;
        }
    }
}