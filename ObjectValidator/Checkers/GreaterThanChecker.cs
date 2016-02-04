using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public class IntGreaterThanChecker<T> : BaseChecker<T, int>
    {
        private int m_Value;

        public IntGreaterThanChecker(int value)
        {
            m_Value = value;
        }

        public override IValidateResult Validate(IValidateResult result, int value, string name, string error)
        {
            if (value <= m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must greater than {0}", m_Value));
            }

            return result;
        }
    }

    public class DoubleGreaterThanChecker<T> : BaseChecker<T, double>
    {
        private double m_Value;

        public DoubleGreaterThanChecker(double value)
        {
            m_Value = value;
        }

        public override IValidateResult Validate(IValidateResult result, double value, string name, string error)
        {
            if (value <= m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must greater than {0}", m_Value));
            }

            return result;
        }
    }

    public class LongGreaterThanChecker<T> : BaseChecker<T, long>
    {
        private long m_Value;

        public LongGreaterThanChecker(long value)
        {
            m_Value = value;
        }

        public override IValidateResult Validate(IValidateResult result, long value, string name, string error)
        {
            if (value <= m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must greater than {0}", m_Value));
            }

            return result;
        }
    }

    public class DecimalGreaterThanChecker<T> : BaseChecker<T, decimal>
    {
        private decimal m_Value;

        public DecimalGreaterThanChecker(decimal value)
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
