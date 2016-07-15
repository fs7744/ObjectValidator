using ObjectValidator.Interfaces;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class GreaterThanIntChecker<T> : BaseChecker<T, int>
    {
        private int m_Value;

        public GreaterThanIntChecker(int value)
        {
            m_Value = value;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, int value, string name, string error)
        {
            if (value <= m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must greater than {0}", m_Value));
            }

            return Task.FromResult(result);
        }
    }
}