using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class NotEqualChecker_Test
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
        public void Test_NotEqualChecker()
        {
            var checker = new NotEqualChecker<ValidateContext, string>("a");
            var result = checker.Validate(checker.GetResult(), "a", "b", null);
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b", result.Failures[0].Name);
            Assert.AreEqual("a", result.Failures[0].Value);
            Assert.AreEqual("The value is equal a", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), null, "a1", "b1");
            Assert.IsNotNull(result);
            Assert.True(result.IsValid);

            result = checker.Validate(checker.GetResult(), "bb", "a1", "b1");
            Assert.IsNotNull(result);
            Assert.True(result.IsValid);
        }
    }
}