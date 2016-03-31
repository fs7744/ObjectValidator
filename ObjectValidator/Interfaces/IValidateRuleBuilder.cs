using ObjectValidator.Entities;
using System;
using System.Collections.Generic;

namespace ObjectValidator.Interfaces
{
    public interface IValidateRuleBuilder
    {
        string RuleSet { get; set; }

        string ValueName { get; set; }

        string Error { get; set; }

        Func<ValidateContext, bool> Condition { get; set; }

        List<IValidateRuleBuilder> NextRuleBuilderList { get; set; }

        Func<ValidateContext, string, string, IValidateResult> ValidateFunc { get; set; }

        IValidateRule Build();
    }
}