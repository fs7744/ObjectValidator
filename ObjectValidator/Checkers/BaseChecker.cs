using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public abstract class BaseChecker<T, TProperty>
    {
        public virtual IRuleMessageBuilder<T, TProperty> SetValidate(IFluentRuleBuilder<T, TProperty> builder)
        {
            ParamHelper.CheckParamNull(builder, "builder", "Can't be null");
            var build = builder as IRuleBuilder<T, TProperty>;
            build.ValidateAsyncFunc = async (context, name, error) =>
            {
                var value = build.ValueGetter(context.ValidateObject);
                var result = Container.Resolve<IValidateResult>();
                return await ValidateAsync(result, value, name, error);
            };
            return build as IRuleMessageBuilder<T, TProperty>;
        }

        public IValidateResult GetResult()
        {
            return Container.Resolve<IValidateResult>();
        }

        public void AddFailure(IValidateResult result, string name, object value, string error)
        {
            result.Failures.Add(new ValidateFailure()
            {
                Name = name,
                Value = value,
                Error = error
            });
        }

        public abstract Task<IValidateResult> ValidateAsync(IValidateResult result, TProperty value, string name, string error);
    }
}