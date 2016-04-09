using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;

namespace UnitTest.Base
{
    [TestFixture]
    public class ValidatorBuilder_Test
    {
        [OneTimeSetUp]
        public void SetContainer()
        {
            Container.Init();
        }

        [OneTimeTearDown]
        public void ClearContainer()
        {
            Container.Clear();
        }

        [Test]
        public void Test_ValidatorBuilder_RuleFor()
        {
            var builder = new ValidatorBuilder<ValidateContext>();
            var rule = builder.RuleFor(i => i.RuleSelector);
            Assert.IsNotNull(rule);
            Assert.IsNotNull((rule as IRuleBuilder<ValidateContext, IRuleSelector>).ValueGetter);
            var setter = rule as IRuleMessageBuilder<ValidateContext, IRuleSelector>;
            Assert.IsNotNull(setter);
            Assert.AreEqual("RuleSelector", (setter as IValidateRuleBuilder).ValueName);
        }

        [Test]
        public void Test_ValidatorBuilder_Build()
        {
            var builder = new ValidatorBuilder<ValidateContext>();
            var rule = builder.RuleFor(i => i.RuleSelector);
            var v = builder.Build();
            Assert.IsNotNull(v);
        }

        [Test]
        public void Test_ValidatorBuilder_RuleSet()
        {
            var builder = new ValidatorBuilder<ValidateContext>();
            builder.RuleSet("a", b =>
             {
                 builder.RuleFor(i => i.Option);
                 builder.RuleFor(i => i.RuleSelector);
             });
            Assert.IsNotNull(builder.Builders);
            Assert.AreEqual(2, builder.Builders.Count);
            Assert.IsNotNull(builder.Builders[0]);
            Assert.IsNotNull(builder.Builders[1]);
            Assert.AreEqual("A", builder.Builders[0].RuleSet);
            Assert.AreEqual("A", builder.Builders[1].RuleSet);
        }
    }
}