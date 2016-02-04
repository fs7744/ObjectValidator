using ObjectValidator.Interfaces;
using System.Text.RegularExpressions;

namespace ObjectValidator.Checkers
{
    public class NotRegexChecker<T> : BaseChecker<T, string>
    {
        private Regex m_Regex;

        public NotRegexChecker(Regex regex)
        {
            m_Regex = regex;
        }

        public NotRegexChecker(string pattern, RegexOptions options) : this(new Regex(pattern, options))
        {
        }

        public NotRegexChecker(string pattern) : this(new Regex(pattern))
        {
        }

        public override IValidateResult Validate(IValidateResult result, string value, string name, string error)
        {
            if (!string.IsNullOrEmpty(value) && m_Regex.IsMatch(value))
            {
                AddFailure(result, name, value, error ?? "The value must be not match regex");
            }
            return result;
        }
    }
}