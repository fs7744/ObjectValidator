using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator.Checkers
{
    public class NullableNotNullChecker<T, TProperty> : BaseChecker<T, Nullable<TProperty>>
    {
        public override IValidateResult Validate(IValidateResult result, Nullable<TProperty> value, string name, string error)
        {
            if (!value.HasValue)
            {
                AddFailure(result, name, value, error ?? "Can't be null");
            }
            return result;
        }
    }
}