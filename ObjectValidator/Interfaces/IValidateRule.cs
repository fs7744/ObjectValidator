using ObjectValidator.Entities;
using System;
using System.Collections.Generic;

namespace ObjectValidator.Interfaces
{
    public interface IValidateRule
    {
        string RuleSet { get; set; }

        List<IValidateRule> NextRuleList { get; set; }

        string ValueName { get; set; }

        string Error { get; set; }

        Func<ValidateContext, bool> Condition { get; set; }

        Func<ValidateContext, string, string, IValidateResult> ValidateFunc { get; set; }

        IValidateResult Validate(ValidateContext context);
    }
}