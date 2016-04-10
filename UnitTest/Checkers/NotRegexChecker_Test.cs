using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System.Text.RegularExpressions;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class NotRegexChecker_Test
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
        public void Test_RegexChecker()
        {
            var checker = new NotRegexChecker<ValidateContext>(Syntax.EmailRegex, RegexOptions.IgnoreCase);
            var result = checker.Validate(checker.GetResult(), null, "a", "b");
            Assert.IsNotNull(result);
            Assert.True(result.IsValid);

            checker = new NotRegexChecker<ValidateContext>(Syntax.EmailRegex);
            result = checker.Validate(checker.GetResult(), "1331@24.com", "a", null);
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual("1331@24.com", result.Failures[0].Value);
            Assert.AreEqual("The value must be not match regex", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "1331@244.com", "a1", "c");
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("1331@244.com", result.Failures[0].Value);
            Assert.AreEqual("c", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "133124.com", "a", null);
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(0, result.Failures.Count);
        }
    }
}