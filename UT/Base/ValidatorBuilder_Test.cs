using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using Xunit;

namespace UnitTest.Base
{
    public class ValidatorBuilder_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public void Test_ValidatorBuilder_RuleFor()
        {
            var builder = new ValidatorBuilder<ValidateContext>(_Validation);
            var rule = builder.RuleFor(i => i.RuleSelector);
            Assert.NotNull(rule);
            Assert.NotNull((rule as IRuleBuilder<ValidateContext, IRuleSelector>).ValueGetter);
            var setter = rule as IRuleMessageBuilder<ValidateContext, IRuleSelector>;
            Assert.NotNull(setter);
            Assert.Equal("RuleSelector", (setter as IValidateRuleBuilder).ValueName);
        }

        [Fact]
        public void Test_ValidatorBuilder_Build()
        {
            var builder = new ValidatorBuilder<ValidateContext>(_Validation);
            var rule = builder.RuleFor(i => i.RuleSelector);
            var v = builder.Build();
            Assert.NotNull(v);
        }

        [Fact]
        public void Test_ValidatorBuilder_RuleSet()
        {
            var builder = new ValidatorBuilder<ValidateContext>(_Validation);
            builder.RuleSet("a", b =>
             {
                 builder.RuleFor(i => i.Option);
                 builder.RuleFor(i => i.RuleSelector);
                 builder.RuleFor(i => i.RuleSelector);
                 builder.Builders.RemoveAt(2);
             });
            Assert.NotNull(builder.Builders);
            Assert.Equal(2, builder.Builders.Count);
            Assert.NotNull(builder.Builders[0]);
            Assert.NotNull(builder.Builders[1]);
            Assert.Equal("A", builder.Builders[0].RuleSet);
            Assert.Equal("A", builder.Builders[1].RuleSet);
        }
    }
}