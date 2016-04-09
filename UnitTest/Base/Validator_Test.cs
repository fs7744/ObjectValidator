using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Entities;
using System.Collections.Generic;

namespace UnitTest.Base
{
    [TestFixture]
    public class Validator_Test
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
        public void Test_Validator_SetRules()
        {
            Assert.Catch(() =>
            {
                new Validator().SetRules(null);
            });
        }

        [Test]
        public void Test_Validator_Validate()
        {
            var v = new Validator();
            var rule = new ValidateRule()
            {
                ValidateFunc = (c, name, error) =>
                {
                    var f = new ValidateFailure()
                    {
                        Name = name,
                        Error = error,
                        Value = c
                    };
                    return new ValidateResult(new List<ValidateFailure>() { f });
                }
            };

            var context = new ValidateContext() { RuleSelector = new RuleSelector() };
            var result = v.Validate(context);
            Assert.IsNotNull(result);
            Assert.True(result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(0, result.Failures.Count);

            v.SetRules(new List<ValidateRule>() { rule });
            result = v.Validate(context);
            Assert.IsNotNull(result);
            Assert.False(result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);

            rule.ValidateFunc = (c, name, error) =>
            {
                return new ValidateResult();
            };

            result = v.Validate(context);
            Assert.IsNotNull(result);
            Assert.True(result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(0, result.Failures.Count);
        }
    }
}