using ObjectValidator.Interfaces;
using System;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class GreaterThanDateTimeChecker<T> : BaseChecker<T, DateTime>
    {
        private DateTime m_Value;

        public GreaterThanDateTimeChecker(DateTime value)
        {
            m_Value = value;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, DateTime value, string name, string error)
        {
            if (value <= m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must greater than {0}", m_Value));
            }

            return Task.FromResult(result);
        }
    }
}