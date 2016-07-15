using ObjectValidator.Interfaces;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class LessThanLongChecker<T> : BaseChecker<T, long>
    {
        private long m_Value;

        public LessThanLongChecker(long value)
        {
            m_Value = value;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, long value, string name, string error)
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