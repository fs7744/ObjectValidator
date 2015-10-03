using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator.Checkers
{
    public class LengthChecker<T> : BaseChecker<T, string>
    {
        private int m_Min;
        private int m_Max;

        public LengthChecker(int min, int max)
        {
            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            m_Min = min;
            m_Max = max;
        }

        public override IRuleBuilder<T, string> SetValidateFunc(IRuleBuilder<T, string> builder)
        {
            builder.ValidateFunc = (context, name, error) =>
            {
                var value = builder.ValueGetter(context.ValidateObject);
                var result = Container.Resolve<IValidateResult>();
                var len = string.IsNullOrEmpty(value) ? 0 : value.Length;
                if ((m_Max != -1 && m_Max < len) || m_Min > len)
                {
                    result.Failures.Add(new ValidateFailure()
                    {
                        Name = name,
                        Value = value,
                        Error = error ?? string.Format("The length {0} is not between {1} and {2}", len, m_Min, m_Max)
                    });
                }
                return result;
            };
            return builder;
        }
    }
}