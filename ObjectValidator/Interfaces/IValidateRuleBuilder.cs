using ObjectValidator.Entities;
using System;

namespace ObjectValidator.Interfaces
{
    public interface IValidateRuleBuilder
    {
        string RuleSet { get; set; }

        string ValueName { get; set; }

        string Error { get; set; }

        Func<ValidateContext, bool> Condition { get; set; }

        IValidateRuleBuilder NextRuleBuilder { get; set; }

        Func<ValidateContext, string, string, IValidateResult> ValidateFunc { get; set; }

        IValidateRule Build();
    }
}