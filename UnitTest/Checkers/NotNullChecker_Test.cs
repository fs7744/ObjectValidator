using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class NotNullChecker_Test
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

            result = checker.Validate(checker.GetResult(), null, "a", null);
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.IsValid);
            Assert.IsNotNull(result.Failures);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("Can't be null", result.Failures[0].Error);

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

        [Test]
        public void Test_NullableNotNullChecker()
        {
            var checker = new NullableNotNullChecker<ValidateContext, int>();
            var result = checker.Validate(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("b", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), null, "a", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("Can't be null", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), 1, "a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Test]
        public void Test_NotNullOrEmptyStringChecker()
        {
            var checker = new NotNullOrEmptyStringChecker<ValidateContext>();
            var result = checker.Validate(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("b", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), null, "a", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "", "a1", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("", result.Failures[0].Value);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), " ", "a1", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = checker.Validate(checker.GetResult(), string.Empty, "a2", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a2", result.Failures[0].Name);
            Assert.AreEqual(string.Empty, result.Failures[0].Value);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "s", "a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Test]
        public void Test_NotNullOrWhiteSpaceChecker()
        {
            var checker = new NotNullOrWhiteSpaceChecker<ValidateContext>();
            var result = checker.Validate(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("b", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), null, "a", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("Can't be null or empty or whitespace", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "", "a1", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("", result.Failures[0].Value);
            Assert.AreEqual("Can't be null or empty or whitespace", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), " ", "a1", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual(" ", result.Failures[0].Value);
            Assert.AreEqual("Can't be null or empty or whitespace", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), string.Empty, "a2", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a2", result.Failures[0].Name);
            Assert.AreEqual(string.Empty, result.Failures[0].Value);
            Assert.AreEqual("Can't be null or empty or whitespace", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "s", "a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Test]
        public void Test_NotNullOrEmptyListChecker()
        {
            var checker = new NotNullOrEmptyListChecker<ValidateContext>();
            var result = checker.Validate(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("b", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), null, "a", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "", "a1", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("", result.Failures[0].Value);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), " ", "a1", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = checker.Validate(checker.GetResult(), string.Empty, "a2", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a2", result.Failures[0].Name);
            Assert.AreEqual(string.Empty, result.Failures[0].Value);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);

            result = checker.Validate(checker.GetResult(), "s", "a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }
    }
}