using Microsoft.Extensions.DependencyInjection;
using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjectValidator.Base
{
    public class ValidateRule : IValidateRule
    {
        protected Validation Validation { get; set; }

        public ValidateRule(Validation validation)
        {
            Validation = validation;
            NextRuleList = new List<IValidateRule>();
        }

        public List<IValidateRule> NextRuleList { get; set; }

        public string RuleSet { get; set; }

        public string ValueName { get; set; }

        public string Error { get; set; }

        public Func<ValidateContext, bool> Condition { get; set; }

        public Func<ValidateContext, string, string, Task<IValidateResult>> ValidateAsyncFunc { get; set; }

        public async virtual Task<IValidateResult> ValidateAsync(ValidateContext context)
        {
            ParamHelper.CheckParamNull(ValidateAsyncFunc, nameof(ValidateAsyncFunc), "Can't be null");
            ParamHelper.CheckParamNull(context, nameof(context), "Can't be null");
            IValidateResult result = null;
            if (Condition == null || Condition(context))
            {
                result = await ValidateAsyncByFunc(context);
            }
            else
            {
                result = Validation.Provider.GetService<IValidateResult>();
            }
            return result;
        }

        public async Task<IValidateResult> ValidateAsyncByFunc(ValidateContext context)
        {
            IValidateResult result = await ValidateAsyncFunc(context, ValueName, Error);
            if (NextRuleList.IsEmptyOrNull() || (!result.IsValid && context.Option != ValidateOption.Continue)) return result;

            ValidateNextRuleList(context, result);

            return result;
        }

        private async void ValidateNextRuleList(ValidateContext context, IValidateResult result)
        {
            foreach (var nextRule in NextRuleList)
            {
                if (result.IsValid || context.Option == ValidateOption.Continue)
                {
                    var nextResult = await nextRule.ValidateAsync(context);
                    result.Merge(nextResult.Failures);
                }
                else
                    break;
            }
        }
    }
}