using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ObjectValidator.Base
{
    public class RuleBuilder<T, TValue> : IRuleBuilder<T, TValue>
    {
        public string RuleSet { get; set; }

        public Func<object, TValue> ValueGetter { get; protected set; }

        public string ValueName { get; set; }

        public string Error { get; set; }

        public IValidateRuleBuilder NextRuleBuilder { get; set; }

        public Func<ValidateContext, bool> Condition { get; set; }

        public Func<ValidateContext, string, string, IValidateResult> ValidateFunc { get; set; }

        public void SetValueGetter(Expression<Func<T, TValue>> expression)
        {
            var stack = new Stack<MemberInfo>();
            var memberExp = expression.Body as MemberExpression;
            while (memberExp != null)
            {
                stack.Push(memberExp.Member);
                memberExp = memberExp.Expression as MemberExpression;
            }

            var p = Expression.Parameter(typeof(object), "p");
            var convert = Expression.Convert(p, typeof(T));
            Expression exp = convert;

            if (stack.Count > 0)
            {
                while (stack.Count > 0)
                {
                    exp = Expression.MakeMemberAccess(exp, stack.Pop());
                }

                ValueName = exp.ToString().Replace(convert.ToString() + ".", "");
            }
            else
            {
                ValueName = string.Empty;
            }

            ValueGetter = Expression.Lambda<Func<object, TValue>>(exp, p).Compile();
        }

        public IFluentRuleBuilder<T, TProperty> ThenRuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var builder = Utils.RuleFor(expression);
            NextRuleBuilder = builder as IValidateRuleBuilder;
            return builder;
        }

        public IValidateRule Build()
        {
            var rule = Container.Resolve<IValidateRule>();
            rule.ValueName = ValueName;
            rule.Error = Error;
            rule.ValidateFunc = ValidateFunc;
            rule.Condition = Condition;
            rule.RuleSet = RuleSet;
            var nextBuilder = NextRuleBuilder;
            if (nextBuilder != null)
                rule.NextRule = nextBuilder.Build();
            return rule;
        }
    }
}