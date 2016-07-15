using ObjectValidator.Interfaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class RegexChecker<T> : BaseChecker<T, string>
    {
        private Regex m_Regex;

        public RegexChecker(Regex regex, Validation validation) : base(validation)
        {
            m_Regex = regex;
        }

        public RegexChecker(string pattern, RegexOptions options, Validation validation) : this(new Regex(pattern, options), validation)
        {
        }

        public RegexChecker(string pattern, Validation validation) : this(new Regex(pattern), validation)
        {
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, string value, string name, string error)
        {
            if (string.IsNullOrEmpty(value) || !m_Regex.IsMatch(value))
            {
                AddFailure(result, name, value, error ?? "The value no match regex");
            }
            return Task.FromResult(result);
        }
    }
}