using ObjectValidator.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjectValidator.Interfaces
{
    public interface IValidateRule
    {
        string RuleSet { get; set; }

        List<IValidateRule> NextRuleList { get; set; }

        string ValueName { get; set; }

        string Error { get; set; }

        Func<ValidateContext, bool> Condition { get; set; }

        Func<ValidateContext, string, string, Task<IValidateResult>> ValidateAsyncFunc { get; set; }

        Task<IValidateResult> ValidateAsync(ValidateContext context);
    }
}