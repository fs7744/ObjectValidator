using ObjectValidator.Entities;
using ObjectValidator.Interfaces;

namespace ObjectValidator
{
    public static class Validation
    {
        public static IValidatorBuilder<T> NewValidatorBuilder<T>()
        {
            return Container.Resolve<IValidatorBuilder<T>>();
        }

        public static ValidateContext CreateContext(object validateObject,
            ValidateOption option = ValidateOption.StopOnFirstFailure, params string[] ruleSetList)
        {
            var result = Container.Resolve<ValidateContext>();
            result.Option = option;
            result.RuleSetList = ruleSetList;
            result.ValidateObject = validateObject;
            return result;
        }
    }
}