using System.Collections.Generic;

namespace ObjectValidator.Interfaces
{
    public interface IValidatorSetter : IValidator
    {
        void SetRules(IEnumerable<IValidateRule> rules);
    }
}