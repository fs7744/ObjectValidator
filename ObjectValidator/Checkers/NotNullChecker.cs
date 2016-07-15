using ObjectValidator.Interfaces;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class NotNullChecker<T, TProperty> : BaseChecker<T, TProperty>
    {
        public override Task<IValidateResult> ValidateAsync(IValidateResult result, TProperty value, string name, string error)
        {
            if (value == null)
            {
                AddFailure(result, name, value, error ?? "Can't be null");
            }
            return Task.FromResult(result);
        }
    }
}