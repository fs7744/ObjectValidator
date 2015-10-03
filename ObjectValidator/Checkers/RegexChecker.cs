using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System.Text.RegularExpressions;

namespace ObjectValidator.Checkers
{
    public class RegexChecker<T> : BaseChecker<T, string>
    {
        private Regex m_Regex;

        public RegexChecker(Regex regex)
        {
            m_Regex = regex;
        }

        public RegexChecker(string pattern, RegexOptions options) : this(new Regex(pattern, options))
        {
        }

        public RegexChecker(string pattern) : this(new Regex(pattern))
        {
        }

        public override IRuleBuilder<T, string> SetValidateFunc(IRuleBuilder<T, string> builder)
        {
            builder.ValidateFunc = (context, name, error) =>
            {
                var value = builder.ValueGetter(context.ValidateObject);
                var result = Container.Resolve<IValidateResult>();
                if (!m_Regex.IsMatch(value))
                {
                    result.Failures.Add(new ValidateFailure()
                    {
                        Name = name,
                        Value = value,
                        Error = error ?? "The value no match regex"
                    });
                }
                return result;
            };
            return builder;
        }
    }
}