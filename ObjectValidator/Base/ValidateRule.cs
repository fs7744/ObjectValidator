using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;

namespace ObjectValidator.Base
{
    public class ValidateRule : IValidateRule
    {
        public ValidateRule()
        {
            NextRuleList = new List<IValidateRule>();
        }

        public List<IValidateRule> NextRuleList { get; set; }

        public string RuleSet { get; set; }

        public string ValueName { get; set; }

        public string Error { get; set; }

        public Func<ValidateContext, bool> Condition { get; set; }

        public Func<ValidateContext, string, string, IValidateResult> ValidateFunc { get; set; }

        public IValidateResult Validate(ValidateContext context)
        {
            ParamHelper.CheckParamNull(ValidateFunc, "ValidateFunc", "Can't be null");
            ParamHelper.CheckParamNull(context, "context", "Can't be null");
            IValidateResult result = null;
            if (Condition == null || Condition(context))
            {
                result = ValidateByFunc(context);
            }
            else
            {
                result = Container.Resolve<IValidateResult>();
            }
            return result;
        }

        public IValidateResult ValidateByFunc(ValidateContext context)
        {
            IValidateResult result = ValidateFunc(context, ValueName, Error);
            if (NextRuleList.IsEmptyOrNull() || (!result.IsValid && context.Option != ValidateOption.Continue)) return result;

            ValidateNextRuleList(context, result);

            return result;
        }

        private void ValidateNextRuleList(ValidateContext context, IValidateResult result)
        {
            foreach (var nextRule in NextRuleList)
            {
                if (result.IsValid || context.Option == ValidateOption.Continue)
                {
                    var nextResult = nextRule.Validate(context);
                    result.Merge(nextResult.Failures);
                }
                else
                    break;
            }
        }
    }
}