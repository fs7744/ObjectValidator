using ObjectValidator.Common;
using ObjectValidator.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ObjectValidator.Base
{
    public class ValidatorBuilder<T> : IValidatorBuilder<T>
    {
        private ObservableCollection<IValidateRuleBuilder> m_Builders
          = new ObservableCollection<IValidateRuleBuilder>();

        public IValidator Build()
        {
            var result = Container.Resolve<IValidatorSetter>();
            result.SetRules(m_Builders.Select(i => i.Build()));
            return result;
        }

        public IRuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            ParamHelper.CheckParamNull(expression, "expression", "Can't be null");
            var builder = Container.Resolve<IRuleBuilder<T, TProperty>>();
            builder.SetValueGetter(expression);
            m_Builders.Add(builder);
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
            m_Builders.CollectionChanged += updateRuleSet;
            action(this);
            m_Builders.CollectionChanged -= updateRuleSet;
        }
    }
}