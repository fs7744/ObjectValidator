using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class LengthChecker_Test
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
        public void Test_LengthChecker()
        {
            var checker = new LengthChecker<ValidateContext>(2, -1);
            var result = checker.Validate(checker.GetResult(), string.Empty, "a", null);
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Empty, result.Failures[0].Value);
            Assert.AreEqual("The length 0 is not between 2 and -1", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), null, "a", "not");
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("not", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "2", "a", "not");
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual("2", result.Failures[0].Value);
            Assert.AreEqual("not", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "2221`3`212312312", "a", "not");
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(0, result.Failures.Count);

            checker = new LengthChecker<ValidateContext>(0, 5);

            result = checker.Validate(checker.GetResult(), "2", "a", "not");
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(0, result.Failures.Count);

            result = checker.Validate(checker.GetResult(), null, "a", "not");
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(0, result.Failures.Count);

            result = checker.Validate(checker.GetResult(), string.Empty, "a", "not");
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(0, result.Failures.Count);

            result = checker.Validate(checker.GetResult(), "12345", "a", "not");
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(0, result.Failures.Count);

            result = checker.Validate(checker.GetResult(), "123456", "a", "not");
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual("123456", result.Failures[0].Value);
            Assert.AreEqual("not", result.Failures[0].Error);
        }
    }
}