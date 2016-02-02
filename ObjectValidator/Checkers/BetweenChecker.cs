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
            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            m_Min = min;
            m_Max = max;
        }

        public override IValidateResult Validate(IValidateResult result, int value, string name, string error)
        {
            if (m_Min <= value && m_Max >= value)
            {
                AddFailure(result, name, value, error ?? 
                    string.Format("The value is not between {1} and {2}", m_Min, m_Max));
            }
            return result;
        }
    }

    public class BetweenDoubleChecker<T> : BaseChecker<T, double>
    {
        private double m_Min;
        private double m_Max;

        public BetweenDoubleChecker(double min, double max)
        {
            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            m_Min = min;
            m_Max = max;
        }

        public override IValidateResult Validate(IValidateResult result, double value, string name, string error)
        {
            if (m_Min <= value && m_Max >= value)
            {
                AddFailure(result, name, value, error ??
                    string.Format("The value is not between {1} and {2}", m_Min, m_Max));
            }
            return result;
        }
    }

    public class BetweenDecimalChecker<T> : BaseChecker<T, decimal>
    {
        private decimal m_Min;
        private decimal m_Max;

        public BetweenDecimalChecker(decimal min, decimal max)
        {
            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            m_Min = min;
            m_Max = max;
        }

        public override IValidateResult Validate(IValidateResult result, decimal value, string name, string error)
        {
            if (m_Min <= value && m_Max >= value)
            {
                AddFailure(result, name, value, error ??
                    string.Format("The value is not between {1} and {2}", m_Min, m_Max));
            }
            return result;
        }
    }

    public class BetweenLongChecker<T> : BaseChecker<T, long>
    {
        private long m_Min;
        private long m_Max;

        public BetweenLongChecker(long min, long max)
        {
            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            m_Min = min;
            m_Max = max;
        }

        public override IValidateResult Validate(IValidateResult result, long value, string name, string error)
        {
            if (m_Min <= value && m_Max >= value)
            {
                AddFailure(result, name, value, error ??
                    string.Format("The value is not between {1} and {2}", m_Min, m_Max));
            }
            return result;
        }
    }
}
