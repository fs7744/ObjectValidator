using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System.Linq;

namespace ObjectValidator.Base
{
    public class RuleSelector : IRuleSelector
    {
        public bool CanExecute(IValidateRule rule, ValidateContext context)
        {
            return string.IsNullOrEmpty(rule.RuleSet)
                || context.RuleSetList.IsEmptyOrNull()
                    ? true
                    : context.RuleSetList.Contains(rule.RuleSet);
        }
    }
}