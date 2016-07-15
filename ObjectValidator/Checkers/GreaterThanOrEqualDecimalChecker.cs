using ObjectValidator.Interfaces;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class GreaterThanOrEqualDecimalChecker<T> : BaseChecker<T, decimal>
    {
        private decimal m_Value;

        public GreaterThanOrEqualDecimalChecker(decimal value, Validation validation) : base(validation)
        {
            m_Value = value;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, decimal value, string name, string error)
        {
            if (value < m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must greater than or equal {0}", m_Value));
            }

            return Task.FromResult(result);
        }
    }
}