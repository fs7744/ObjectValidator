using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public class LessThanOrEqualDecimalChecker<T> : BaseChecker<T, decimal>
    {
        private decimal m_Value;

        public LessThanOrEqualDecimalChecker(decimal value)
        {
            m_Value = value;
        }

        public override IValidateResult Validate(IValidateResult result, decimal value, string name, string error)
        {
            if (value > m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must less than or equal {0}", m_Value));
            }

            return result;
        }
    }
}