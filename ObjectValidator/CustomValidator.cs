using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ObjectValidator
{
    public abstract class CustomValidator<T>
    {
        private ObservableCollection<IValidateRuleBuilder> m_Builders
          = new ObservableCollection<IValidateRuleBuilder>();

        private List<IValidateRule> m_Rules
           = new List<IValidateRule>();

        public ValidateOption Option { get; set; }

        public virtual IValidateResult Validate(T entity, params string[] ruleSetList)
        {
            var context = Container.Resolve<ValidateContext>();
            context.Option = Option;
            context.ValidateObject = entity;
            context.RuleSetList = ruleSetList.Where(i => !string.IsNullOrEmpty(i)).Select(i => i.ToUpper()).ToArray();
            var rules = m_Rules.Where(i => context.RuleSelector.CanExecute(i, context)).ToArray();
            var result = Container.Resolve<IValidateResult>();
            if (!rules.IsEmptyOrNull())
            {
                var tasks = rules.Select(i => Task.Factory.StartNew(() => i.Validate(context))).ToArray();
                Task.WaitAll(tasks);

                if (tasks.Any(i => i.IsFaulted))
                    throw new AggregateException(tasks.Where(i => i.IsFaulted).Select(i => i.Exception));

                result.Merge(tasks.Where(i => i.IsCompleted).SelectMany(i => i.Result.Failures));
            }

            return result;
        }

        protected void RuleSet(string ruleSet, Action action)
        {
            ParamHelper.CheckParamEmptyOrNull(ruleSet, "ruleSet", "Can't be null");
            ParamHelper.CheckParamNull(action, "action", "Can't be null");

            var upRuleSet = ruleSet.ToUpper();
            var updateGroup = new NotifyCollectionChangedEventHandler<IValidateRuleBuilder>((o, e) =>
            {
                if (e.Action != NotifyCollectionChangedAction.Add) return;
                foreach (var item in e.NewItems)
                {
                    item.RuleSet = upRuleSet;
                }
            });
            m_Builders.CollectionChanged += updateGroup;
            action();
            m_Builders.CollectionChanged -= updateGroup;
        }

        protected IRuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            ParamHelper.CheckParamNull(expression, "expression", "Can't be null");
            var builder = Container.Resolve<IRuleBuilder<T, TProperty>>();
            builder.SetValueGetter(expression);
            m_Builders.Add(builder);
            return builder;
        }

        protected virtual void Build()
        {
            m_Rules.Clear();
            m_Rules.AddRange(m_Builders.Select(i => i.Build()));
        }
    }
}