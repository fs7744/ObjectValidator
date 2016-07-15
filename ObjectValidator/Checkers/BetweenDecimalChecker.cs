using ObjectValidator.Interfaces;
using System;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class BetweenDecimalChecker<T> : BaseChecker<T, decimal>
    {
        private decimal m_Min;
        private decimal m_Max;

        public BetweenDecimalChecker(decimal min, decimal max)
        {
            if (max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            m_Min = min;
            m_Max = max;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, decimal value, string name, string error)
        {
            if (m_Min >= value || m_Max <= value)
            {
                AddFailure(result, name, value, error ??
                    string.Format("The value is not between {0} and {1}", m_Min, m_Max));
            }
            return Task.FromResult(result);
        }
    }
}