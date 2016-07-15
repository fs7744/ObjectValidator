using Microsoft.Extensions.DependencyInjection;
using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectValidator.Base
{
    public class CollectionValidateRule : ValidateRule
    {
        public CollectionValidateRule(Validation validation) : base(validation)
        {
        }

        public override Task<IValidateResult> ValidateAsync(ValidateContext context)
        {
            ParamHelper.CheckParamNull(context, "context", "Can't be null");
            IValidateResult result = Validation.Provider.GetService<IValidateResult>();
            var list = context.ValidateObject as IEnumerable;
            if (Condition == null || Condition(context))
            {
                if (list != null)
                    ValidateElementList(context, result, list);
            }
            return Task.FromResult(result);
        }

        private void ValidateElementList(ValidateContext context, IValidateResult result, IEnumerable list)
        {
            var index = 0;
            foreach (var item in list)
            {
                if (result.IsValid || context.Option == ValidateOption.Continue)
                {
                    var ct = Validation.CreateContext(item, context.Option, context.RuleSetList.ToArray());
                    ValidateElement(ct, result, index);
                }
                else
                    break;
                index++;
            }
        }

        private void ValidateElement(ValidateContext context, IValidateResult result, int index)
        {
            foreach (var rule in NextRuleList)
            {
                if (result.IsValid || context.Option == ValidateOption.Continue)
                {
                    ValidateNextRule(context, result, index, rule);
                }
                else
                    break;
            }
        }

        private async void ValidateNextRule(ValidateContext context, IValidateResult result, int index, IValidateRule rule)
        {
            var nextResult = await rule.ValidateAsync(context);
            if (!nextResult.IsValid)
            {
                foreach (var failure in nextResult.Failures)
                {
                    failure.Name = string.Format(string.IsNullOrEmpty(failure.Name) ? "{0}[{1}]{2}" : "{0}[{1}].{2}", ValueName, index, failure.Name);
                }
            }
            result.Merge(nextResult.Failures);
        }
    }
}