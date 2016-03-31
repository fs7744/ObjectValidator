using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator.Checkers
{
    public class GreaterThanOrEqualDateTimeChecker<T> : BaseChecker<T, DateTime>
    {
        private DateTime m_Value;

        public GreaterThanOrEqualDateTimeChecker(DateTime value)
        {
            m_Value = value;
        }

        public override IValidateResult Validate(IValidateResult result, DateTime value, string name, string error)
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