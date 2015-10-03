using ObjectValidator.Entities;
using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public class NotNullChecker<T, TProperty> : BaseChecker<T, TProperty>
    {
        public override IRuleBuilder<T, TProperty> SetValidateFunc(IRuleBuilder<T, TProperty> builder)
        {
            builder.ValidateFunc = (context, name, error) =>
            {
                var value = builder.ValueGetter(context.ValidateObject);
                var result = Container.Resolve<IValidateResult>();
                if (value != null)
                {
                    result.Failures.Add(new ValidateFailure()
                    {
                        Name = name,
                        Value = value,
                        Error = error ?? "Can't be null"
                    });
                }
                return result;
            };
            return builder;
        }
    }
}