using Microsoft.Extensions.DependencyInjection;
using ObjectValidator.Common;

using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectValidator.Checkers
{
    public class EachChecker<T, TProperty> : BaseChecker<T, IEnumerable<TProperty>>
    {
        public IValidateRule ValidateRule { get; set; }

        public EachChecker(Validation validation) : base(validation)
        {
        }

        public override IRuleMessageBuilder<T, IEnumerable<TProperty>> SetValidate(IFluentRuleBuilder<T, IEnumerable<TProperty>> builder)
        {
            ParamHelper.CheckParamNull(builder, "builder", "Can't be null");
            var build = builder as IRuleValueGetterBuilder<T, IEnumerable<TProperty>>;
            build.ValidateAsyncFunc = async (context, name, error) =>
            {
                ValidateRule.ValueName = name;
                var value = build.ValueGetter(context.ValidateObject);
                var result = Validation.Provider.GetService<IValidateResult>();
                var ct = Validation.CreateContext(value, context.Option, context.RuleSetList.ToArray());
                return await ValidateRule.ValidateAsync(ct);
            };
            return build as IRuleMessageBuilder<T, IEnumerable<TProperty>>;
        }

        public override Task<IValidateResult> ValidateAsync(IValidateResult result, IEnumerable<TProperty> value, string name, string error)
        {
            throw new NotImplementedException();
        }
    }
}