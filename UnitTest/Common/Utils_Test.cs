using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;

namespace UnitTest.Common
{
    [TestFixture]
    public class Utils_Test
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
        public void Test_RuleFor()
        {
            var rule = Utils.RuleFor<ValidateFailure, string>(i => i.Error);
            Assert.IsNotNull(rule);
            Assert.IsNotNull(rule.ValueGetter);
            var builder = rule as IRuleMessageBuilder<ValidateFailure, string>;
            Assert.IsNotNull(builder);
            Assert.AreEqual("Error", builder.ValueName);
        }
    }
}