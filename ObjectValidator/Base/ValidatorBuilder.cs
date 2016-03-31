using ObjectValidator.Common;
using ObjectValidator.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ObjectValidator.Base
{
    public class ValidatorBuilder<T> : IValidatorBuilder<T>
    {
        public ObservableCollection<IValidateRuleBuilder> Builders { get; set; }

        public ValidatorBuilder()
        {
            Builders = new ObservableCollection<IValidateRuleBuilder>();
        }

        public IValidator Build()
        {
            var result = Container.Resolve<IValidatorSetter>();
            result.SetRules(Builders.Select(i => i.Build()));
            return result;
        }

        public IFluentRuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            ParamHelper.CheckParamNull(expression, "expression", "Can't be null");
            var builder = Container.Resolve<IRuleBuilder<T, TProperty>>();
            builder.SetValueGetter(expression);
            Builders.Add(builder as IValidateRuleBuilder);
            return builder;
        }

        public void RuleSet(string ruleSet, Action<IValidatorBuilder<T>> action)
        {
            ParamHelper.CheckParamEmptyOrNull(ruleSet, "ruleSet", "Can't be null");
            ParamHelper.CheckParamNull(action, "action", "Can't be null");

            var upRuleSet = ruleSet.ToUpper();
            var updateRuleSet = new NotifyCollectionChangedEventHandler<IValidateRuleBuilder>((o, e) =>
            {
                if (e.Action != NotifyCollectionChangedAction.Add) return;
                foreach (var item in e.NewItems)
                {
                    item.RuleSet = upRuleSet;
                }
            });
            Builders.CollectionChanged += updateRuleSet;
            action(this);
            Builders.CollectionChanged -= updateRuleSet;
        }
    }
}