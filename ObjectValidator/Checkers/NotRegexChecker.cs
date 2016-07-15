using ObjectValidator.Interfaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class NotRegexChecker<T> : BaseChecker<T, string>
    {
        private Regex m_Regex;

        public NotRegexChecker(Regex regex, Validation validation) : base(validation)
        {
            m_Regex = regex;
        }

        public NotRegexChecker(string pattern, RegexOptions options, Validation validation) : this(new Regex(pattern, options), validation)
        {
        }

        public NotRegexChecker(string pattern, Validation validation) : this(new Regex(pattern), validation)
        {
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, string value, string name, string error)
        {
            if (!string.IsNullOrEmpty(value) && m_Regex.IsMatch(value))
            {
                AddFailure(result, name, value, error ?? "The value must be not match regex");
            }
            return Task.FromResult(result);
        }
    }
}