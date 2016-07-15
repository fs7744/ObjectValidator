using ObjectValidator.Interfaces;
using System;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class NullableNotNullChecker<T, TProperty> : BaseChecker<T, Nullable<TProperty>> where TProperty : struct
    {
        public override Task<IValidateResult> ValidateAsync(IValidateResult result, Nullable<TProperty> value, string name, string error)
        {
            if (!value.HasValue)
            {
                AddFailure(result, name, value, error ?? "Can't be null");
            }
            return Task.FromResult(result);
        }
    }
}