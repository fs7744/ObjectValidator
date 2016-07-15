using ObjectValidator.Interfaces;
using System;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class MustNotChecker<T, TProperty> : MustChecker<T, TProperty>
    {
        public MustNotChecker(Func<TProperty, bool> func) : base(func)
        {
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, TProperty value, string name, string error)
        {
            if (m_MustBeTrue(value))
            {
                AddFailure(result, name, value, error);
            }
            return Task.FromResult(result);
        }
    }
}