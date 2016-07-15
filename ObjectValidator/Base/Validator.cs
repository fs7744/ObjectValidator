using Microsoft.Extensions.DependencyInjection;
using ObjectValidator.Common;
using ObjectValidator.Entities;

using ObjectValidator.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectValidator.Base
{
    public class Validator : IValidatorSetter
    {
        private List<IValidateRule> m_Rules
           = new List<IValidateRule>();

        private Validation Validation { get; set; }

        public Validator(Validation validation)
        {
            Validation = validation;
        }

        public void SetRules(IEnumerable<IValidateRule> rules)
        {
            ParamHelper.CheckParamNull(rules, "rules", "Can't be null");
            m_Rules.AddRange(rules);
        }

        public Task<IValidateResult> ValidateAsync(ValidateContext context)
        {
            ParamHelper.CheckParamNull(context, "context", "Can't be null");
            var list = context.RuleSetList;
            if (!list.IsEmptyOrNull())
            {
                context.RuleSetList = list.Where(i => !string.IsNullOrEmpty(i)).Select(i => i.ToUpper()).ToArray();
            }
            var rules = m_Rules.Where(i => context.RuleSelector.CanExecute(i, context)).ToArray();
            var result = Validation.Provider.GetService<IValidateResult>();
            if (!rules.IsEmptyOrNull())
            {
                var tasks = rules.Select(async i => await i.ValidateAsync(context)).ToArray();
                var failures = tasks.Where(i => i.IsCompleted)
                                    .SelectMany(i => i.Result.Failures);
                result.Merge(failures);
            }

            return Task.FromResult(result);
        }
    }
}