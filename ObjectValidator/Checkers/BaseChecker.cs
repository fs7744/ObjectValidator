using ObjectValidator.Common;
using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public abstract class BaseChecker<T, TProperty>
    {
        public IRuleMessageBuilder<T, TProperty> SetValidate(IFluentRuleBuilder<T, TProperty> builder)
        {
            ParamHelper.CheckParamNull(builder, "builder", "Can't be null");
            return SetValidateFunc(builder as IRuleBuilder<T, TProperty>) as IRuleMessageBuilder<T, TProperty>;
        }

        public abstract IRuleBuilder<T, TProperty> SetValidateFunc(IRuleBuilder<T, TProperty> builder);
    }
}