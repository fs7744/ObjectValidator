using ObjectValidator.Entities;
using System;

namespace ObjectValidator.Interfaces
{
    public interface IValidateRule
    {
        string RuleSet { get; set; }

        IValidateRule NextRule { get; set; }

        string ValueName { get; set; }

        string Error { get; set; }

        Func<ValidateContext, bool> Condition { get; set; }

        Func<ValidateContext, string, string, IValidateResult> ValidateFunc { get; set; }

        IValidateResult Validate(ValidateContext context);
    }
}