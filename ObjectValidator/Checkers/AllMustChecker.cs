using ObjectValidator.Common;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;

namespace ObjectValidator.Checkers
{
    public class AllMustChecker<T, TProperty> : BaseChecker<T, IEnumerable<TProperty>>
    {
        protected Func<TProperty, bool> m_MustBeTrue;

        public AllMustChecker(Func<TProperty, bool> func)
        {
            ParamHelper.CheckParamNull(func, "func", "Can't be null");
            m_MustBeTrue = func;
        }

        public override IRuleMessageBuilder<T, IEnumerable<TProperty>> SetValidate(IFluentRuleBuilder<T, IEnumerable<TProperty>> builder)
        {
            ParamHelper.CheckParamNull(builder, "builder", "Can't be null");
            var build = builder as IRuleValueGetterBuilder<T, IEnumerable<TProperty>>;
            build.ValidateFunc = (context, name, error) =>
            {
                var value = build.ValueGetter(context.ValidateObject);
                var result = Container.Resolve<IValidateResult>();
                return Validate(result, value as IEnumerable<TProperty>, name, error);
            };
            return builder as IRuleMessageBuilder<T, IEnumerable<TProperty>>;
        }

        public override IValidateResult Validate(IValidateResult result, IEnumerable<TProperty> value, string name, string error)
        {
            var index = 0;
            foreach (var item in value)
            {
                if (!m_MustBeTrue(item))
                {
                    AddFailure(result, string.Format("{0}[{1}]", name, index), item, error);
                }
                index++;
            }
            return result;
        }
    }
}