using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Interfaces;

namespace UnitTest.Base
{
    [TestFixture]
    public class ValidatorBuilder_Test
    {
        [TestFixtureSetUp]
        public void SetContainer()
        {
            Container.Init();
        }

        [TestFixtureTearDown]
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
            Assert.IsNotNull(rule.ValueGetter);
            var setter = rule as IRuleMessageBuilder<ValidateContext, IRuleSelector>;
            Assert.IsNotNull(setter);
            Assert.AreEqual("RuleSelector", setter.ValueName);
        }

        [Test]
        public void Test_ValidatorBuilder_Build()
        {
            var builder = new ValidatorBuilder<ValidateContext>();
            var rule = builder.RuleFor(i => i.RuleSelector);
            var v = builder.Build();
            Assert.IsNotNull(v);
        }
    }
}