using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class MustNotChecker_Test
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
        public void Test_MustChecker()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new MustNotChecker<ValidateContext, string>(null));
            Assert.AreEqual("func", ex.ParamName);
            Assert.True(ex.Message.Contains("Can't be null"));

            var checker = new MustNotChecker<ValidateContext, string>(str => string.IsNullOrEmpty(str));
            var result = checker.Validate(checker.GetResult(), string.Empty, "a", null);
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Empty, result.Failures[0].Value);
            Assert.AreEqual(null, result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), null, "a", "b");
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("b", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "a", "a", "b");
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(0, result.Failures.Count);
        }
    }
}