using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator.Checkers
{
    public class BetweenIntChecker<T> : BaseChecker<T, int>
    {
        private int m_Min;
        private int m_Max;

        public BetweenIntChecker(int min, int max)
        {
            if (max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            m_Min = min;
            m_Max = max;
        }

        public override IValidateResult Validate(IValidateResult result, int value, string name, string error)
        {
            if (m_Min >= value || m_Max <= value)
            {
                AddFailure(result, name, value, error ??
                    string.Format("The value is not between {0} and {1}", m_Min, m_Max));
            }
            return result;
        }
    }
}