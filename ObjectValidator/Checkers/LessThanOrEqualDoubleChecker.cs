using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public class LessThanOrEqualDoubleChecker<T> : BaseChecker<T, double>
    {
        private double m_Value;

        public LessThanOrEqualDoubleChecker(double value)
        {
            m_Value = value;
        }

        public override IValidateResult Validate(IValidateResult result, double value, string name, string error)
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