using ObjectValidator.Interfaces;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class LessThanFloatChecker<T> : BaseChecker<T, float>
    {
        private float m_Value;

        public LessThanFloatChecker(float value)
        {
            m_Value = value;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, float value, string name, string error)
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