using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public class GreaterThanOrEqualDecimalChecker<T> : BaseChecker<T, decimal>
    {
        private decimal m_Value;

        public GreaterThanOrEqualDecimalChecker(decimal value)
        {
            m_Value = value;
        }

        public override IValidateResult Validate(IValidateResult result, decimal value, string name, string error)
        {
            if (value < m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must greater than or equal {0}", m_Value));
            }

            return result;
        }
    }
}