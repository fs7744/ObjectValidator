using Autofac;
using ObjectValidator.Base;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;

namespace ObjectValidator
{
    public static class Container
    {
        public static ILifetimeScope CurrentScope { get; set; }

        public static void Init()
        {
            Clear();
            var builder = new ContainerBuilder();
            builder.RegisterType<RuleSelector>().As<IRuleSelector>().SingleInstance();
            builder.RegisterGeneric(typeof(RuleBuilder<,>)).As(typeof(IRuleBuilder<,>)).InstancePerDependency();
            builder.Register(c => new ValidateContext() { RuleSelector = c.Resolve<IRuleSelector>() });
            builder.RegisterType<ValidateRule>().As<IValidateRule>().InstancePerDependency();
            builder.RegisterType<ValidateResult>().As<IValidateResult>().InstancePerDependency();

            var container = builder.Build();
            CurrentScope = container.BeginLifetimeScope();
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