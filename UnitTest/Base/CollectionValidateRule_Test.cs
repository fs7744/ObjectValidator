using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Entities;
using System.Collections.Generic;

namespace UnitTest.Base
{
    [TestFixture]
    public class CollectionValidateRule_Test
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
        public void Test_CollectionValidateRule()
        {
            var r = new CollectionValidateRule();
            r.NextRuleList.Add(new ValidateRule()
            {
                ValueName = "c",
                ValidateFunc = (c, s, s2) => new ValidateResult()
            });
            r.NextRuleList.Add(new ValidateRule()
            {
                ValueName = "a",
                ValidateFunc = (c, s, s2) => new ValidateResult(new List<ValidateFailure>()
                { new ValidateFailure() { Name = s, Error = s2, Value = c.ValidateObject } })
            });

            r.Condition = c => false;
            var result = r.Validate(Validation.CreateContext(new List<int> { 1, 2 }));
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            r.Condition = null;
            result = r.Validate(Validation.CreateContext(new List<int> { 2, 3 }));
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("[0].a", result.Failures[0].Name);
            Assert.AreEqual(2, result.Failures[0].Value);

            r.Condition = c => true;
            result = r.Validate(Validation.CreateContext(new List<int> { 1, 2 }, ValidateOption.Continue));
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(2, result.Failures.Count);
            Assert.AreEqual(1, result.Failures[0].Value);
            Assert.AreEqual("[0].a", result.Failures[0].Name);
            Assert.AreEqual(2, result.Failures[1].Value);
            Assert.AreEqual("[1].a", result.Failures[1].Name);

            r.NextRuleList.Add(new ValidateRule()
            {
                ValueName = "b",
                ValidateFunc = (c, s, s2) => new ValidateResult(new List<ValidateFailure>()
                { new ValidateFailure() { Name = s, Error = s2, Value = c.ValidateObject } })
            });
            result = r.Validate(Validation.CreateContext(new List<int> { 1, 2 }));
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("[0].a", result.Failures[0].Name);
            Assert.AreEqual(1, result.Failures[0].Value);
        }
    }
}