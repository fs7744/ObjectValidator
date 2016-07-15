using ObjectValidator.Interfaces;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class LessThanIntChecker<T> : BaseChecker<T, int>
    {
        private int m_Value;

        public LessThanIntChecker(int value)
        {
            m_Value = value;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, int value, string name, string error)
        {
            if (value >= m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must less than {0}", m_Value));
            }

            return Task.FromResult(result);
        }
    }
}