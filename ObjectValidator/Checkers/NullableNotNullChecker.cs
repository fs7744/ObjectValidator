using ObjectValidator.Interfaces;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class NullableNotNullChecker<T, TProperty> : BaseChecker<T, TProperty?> where TProperty : struct
    {
        public NullableNotNullChecker(Validation validation) : base(validation)
        {
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, TProperty? value, string name, string error)
        {
            if (!value.HasValue)
            {
                AddFailure(result, name, value, error ?? "Can't be null");
            }
            return Task.FromResult(result);
        }
    }
}