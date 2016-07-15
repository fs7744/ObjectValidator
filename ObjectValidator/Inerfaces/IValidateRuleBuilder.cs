using ObjectValidator.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObjectValidator.Interfaces
{
    public interface IValidateRuleBuilder
    {
        string RuleSet { get; set; }

        string ValueName { get; set; }

        string Error { get; set; }

        Func<ValidateContext, bool> Condition { get; set; }

        List<IValidateRuleBuilder> NextRuleBuilderList { get; set; }

        Func<ValidateContext, string, string, Task<IValidateResult>> ValidateAsyncFunc { get; set; }

        IValidateRule Build();
    }
}