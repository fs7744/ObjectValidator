using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator.Checkers
{
    public class MustChecker<T, TProperty> : BaseChecker<T, TProperty>
    {
        private Func<TProperty, bool> m_MustBeTrue;

        public MustChecker(Func<TProperty, bool> func)
        {
            ParamHelper.CheckParamNull(func, "func", "Can't be null");
            m_MustBeTrue = func;
        }

        public override IRuleBuilder<T, TProperty> SetValidateFunc(IRuleBuilder<T, TProperty> builder)
        {
            builder.ValidateFunc = (context, name, error) =>
            {
                var value = builder.ValueGetter(context.ValidateObject);
                var result = Container.Resolve<IValidateResult>();
                if (!m_MustBeTrue(value))
                {
                    result.Failures.Add(new ValidateFailure()
                    {
                        Name = name,
                        Value = value,
                        Error = error
                    });
                }
                return result;
            };
            return builder;
        }
    }
}