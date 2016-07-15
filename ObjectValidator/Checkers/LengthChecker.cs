using ObjectValidator.Interfaces;
using System;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class LengthChecker<T> : BaseChecker<T, string>
    {
        private int m_Min;
        private int m_Max;

        public LengthChecker(int min, int max)
        {
            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            m_Min = min;
            m_Max = max;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, string value, string name, string error)
        {
            var len = string.IsNullOrEmpty(value) ? 0 : value.Length;
            if ((m_Max != -1 && m_Max < len) || m_Min > len)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The length {0} is not between {1} and {2}", len, m_Min, m_Max));
            }
            return Task.FromResult(result);
        }
    }
}