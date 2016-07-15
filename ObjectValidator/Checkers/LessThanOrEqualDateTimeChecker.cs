using ObjectValidator.Interfaces;
using System;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class LessThanOrEqualDateTimeChecker<T> : BaseChecker<T, DateTime>
    {
        private DateTime m_Value;

        public LessThanOrEqualDateTimeChecker(DateTime value, Validation validation) : base(validation)
        {
            m_Value = value;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, DateTime value, string name, string error)
        {
            if (value > m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must less than or equal {0}", m_Value));
            }

            return Task.FromResult(result);
        }
    }
}