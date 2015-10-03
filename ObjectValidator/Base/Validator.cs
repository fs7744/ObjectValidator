using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectValidator.Base
{
    public class Validator : IValidatorSetter
    {
        private List<IValidateRule> m_Rules
           = new List<IValidateRule>();

        public void SetRules(IEnumerable<IValidateRule> rules)
        {
            ParamHelper.CheckParamNull(rules, "rules", "Can't be null");
            m_Rules.AddRange(rules);
        }

        public IValidateResult Validate(ValidateContext context)
        {
            ParamHelper.CheckParamNull(context, "context", "Can't be null");
            var list = context.RuleSetList;
            if (!list.IsEmptyOrNull())
            {
                context.RuleSetList = list.Where(i => !string.IsNullOrEmpty(i)).Select(i => i.ToUpper()).ToArray();
            }
            var rules = m_Rules.Where(i => context.RuleSelector.CanExecute(i, context)).ToArray();
            var result = Container.Resolve<IValidateResult>();
            if (!rules.IsEmptyOrNull())
            {
                var tasks = rules.Select(i => Task.Factory.StartNew(() => i.Validate(context))).ToArray();
                Task.WaitAll(tasks);

                if (tasks.Any(i => i.IsFaulted))
                {
                    var exceptions = tasks.Where(i => i.IsFaulted)
                                        .Select(i => i.Exception);
                    throw new AggregateException(exceptions);
                }

                var failures = tasks.Where(i => i.IsCompleted)
                                    .SelectMany(i => i.Result.Failures);
                result.Merge(failures);
            }

            return result;
        }
    }
}