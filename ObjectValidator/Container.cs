using Autofac;
using ObjectValidator.Base;
using ObjectValidator.Checkers;
using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;

namespace ObjectValidator
{
    public static class Container
    {
        public static ILifetimeScope CurrentScope { get; set; }

        public static void Init(Action<ContainerBuilder> action)
        {
            ParamHelper.CheckParamNull(action, "action", "Can't be null");
            Clear();
            var builder = new ContainerBuilder();
            action(builder);
            var container = builder.Build();
            CurrentScope = container.BeginLifetimeScope();
        }

        public static void Init()
        {
            Init(builder =>
            {
                builder.RegisterType<RuleSelector>().As<IRuleSelector>().SingleInstance();
                builder.RegisterGeneric(typeof(RuleBuilder<,>)).As(typeof(IRuleBuilder<,>)).InstancePerDependency();
                builder.Register(c => new ValidateContext() { RuleSelector = c.Resolve<IRuleSelector>() });
                builder.RegisterType<ValidateRule>().As<IValidateRule>().InstancePerDependency();
                builder.RegisterType<ValidateResult>().As<IValidateResult>().InstancePerDependency();
                builder.RegisterGeneric(typeof(ValidatorBuilder<>)).As(typeof(IValidatorBuilder<>)).InstancePerDependency();
                builder.RegisterType<Validator>().As<IValidatorSetter>().InstancePerDependency();
                builder.RegisterType<CollectionValidateRule>().As<CollectionValidateRule>().InstancePerDependency();
                builder.RegisterGeneric(typeof(EachChecker<,>)).As(typeof(EachChecker<,>)).InstancePerDependency();
                builder.RegisterGeneric(typeof(CollectionRuleBuilder<,>)).As(typeof(CollectionRuleBuilder<,>)).InstancePerDependency();
            });
        }

        public static void Clear()
        {
            var scope = CurrentScope;
            if (scope != null)
                scope.Dispose();
        }

        public static T Resolve<T>()
        {
            return CurrentScope.Resolve<T>();
        }
    }
}