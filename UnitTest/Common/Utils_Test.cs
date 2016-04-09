using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;

namespace UnitTest.Common
{
    [TestFixture]
    public class Utils_Test
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
        public void Test_RuleFor()
        {
            var rule = Utils.RuleFor<ValidateFailure, string>(i => i.Error);
            Assert.IsNotNull(rule);
            Assert.IsNotNull(rule.ValueGetter);
            var builder = rule as IRuleMessageBuilder<ValidateFailure, string>;
            Assert.IsNotNull(builder);
            Assert.AreEqual("Error", (builder as IValidateRuleBuilder).ValueName);

            var ex = Assert.Throws<ArgumentNullException>(() => Utils.RuleFor<ValidateFailure, string>(null));
            Assert.NotNull(ex);
            Assert.AreEqual("expression", ex.ParamName);
            Assert.True(ex.Message.Contains("Can't be null"));
        }
    }
}