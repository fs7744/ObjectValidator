using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public class GreaterThanDecimalChecker<T> : BaseChecker<T, decimal>
    {
        private decimal m_Value;

        public GreaterThanDecimalChecker(decimal value)
        {
            m_Value = value;
        }

        public override IValidateResult Validate(IValidateResult result, decimal value, string name, string error)
        {
            if (value <= m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must greater than {0}", m_Value));
            }

            return result;
        }
    }
}