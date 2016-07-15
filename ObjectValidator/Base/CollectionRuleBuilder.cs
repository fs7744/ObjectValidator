using Microsoft.Extensions.DependencyInjection;
using ObjectValidator.Checkers;
using ObjectValidator.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ObjectValidator.Base
{
    public class CollectionRuleBuilder<T, TValue> : RuleBuilder<T, IEnumerable<TValue>>, ICollectionRuleBuilder<T, TValue>
    {
        public CollectionRuleBuilder(Validation validation) : base(validation)
        {
            ElementRuleBuilderList = new List<IRuleBuilder<TValue, TValue>>();
        }

        public EachChecker<T, TValue> EachChecker { get; internal set; }
        public List<IRuleBuilder<TValue, TValue>> ElementRuleBuilderList { get; protected set; }

        public override IValidateRule Build()
        {
            var rule = Validation.Provider.GetService<CollectionValidateRule>();
            rule.ValueName = ValueName;
            rule.Error = Error;
            rule.ValidateAsyncFunc = ValidateAsyncFunc;
            rule.Condition = Condition;
            rule.RuleSet = RuleSet;
            EachChecker.ValidateRule = rule;
            rule.NextRuleList = ElementRuleBuilderList.Where(i => i != null).Select(i => i.Build()).ToList();
            return rule;
        }
    }
}