using Microsoft.Extensions.DependencyInjection;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator
{
    public class Validation 
    {
        public IServiceProvider Provider { get; private set; }

        public Validation(IServiceProvider provider)
        {
            Provider = provider;
        }

        public IValidatorBuilder<T> NewValidatorBuilder<T>()
        {
            return Provider.GetService<IValidatorBuilder<T>>();
        }

        public ValidateContext CreateContext(object validateObject,
            ValidateOption option = ValidateOption.StopOnFirstFailure, params string[] ruleSetList)
        {
            var result = Provider.GetService<ValidateContext>();
            result.Option = option;
            result.RuleSetList = ruleSetList;
            result.ValidateObject = validateObject;
            return result;
        }
    }
}