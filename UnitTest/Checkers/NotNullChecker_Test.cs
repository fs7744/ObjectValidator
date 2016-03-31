using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class NotNullChecker_Test
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
        public void Test_NotNullChecker()
        {
            var checker = new NotNullChecker<ValidateContext, string>();
            var result = checker.Validate(checker.GetResult(), null, "a", "b");
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

            result = checker.Validate(checker.GetResult(), string.Empty, "a", "b");
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(0, result.Failures.Count);
        }
    }
}