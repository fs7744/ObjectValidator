using NUnit.Framework;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class BetweenChecker_Test
    {
        [Test]
        public void Test_BetweenDecimalChecker()
        {
            var checker = new BetweenDecimalChecker<Student>(3m, 10m);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BetweenDecimalChecker<Student>(33m, 10m));
            Assert.AreEqual("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var result = checker.Validate(new ValidateResult(), 4m, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3m, "a", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(3m, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 1m, "a1", "c1");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c1", result.Failures[0].Error);
            Assert.AreEqual(1m, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 10m, "b", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.AreEqual(10m, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 100m, "b1", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b1", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.AreEqual(100m, result.Failures[0].Value);
        }

        [Test]
        public void Test_BetweenDoubleChecker()
        {
            var checker = new BetweenDoubleChecker<Student>(3d, 10d);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BetweenDoubleChecker<Student>(33d, 10d));
            Assert.AreEqual("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var result = checker.Validate(new ValidateResult(), 4d, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3d, "a", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(3d, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 1d, "a1", "c1");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c1", result.Failures[0].Error);
            Assert.AreEqual(1d, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 10d, "b", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.AreEqual(10d, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 100d, "b1", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b1", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.AreEqual(100d, result.Failures[0].Value);
        }

        [Test]
        public void Test_BetweenFloatChecker()
        {
            var checker = new BetweenFloatChecker<Student>(3f, 10f);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BetweenFloatChecker<Student>(33f, 10f));
            Assert.AreEqual("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var result = checker.Validate(new ValidateResult(), 4f, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3f, "a", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(3f, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 1f, "a1", "c1");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c1", result.Failures[0].Error);
            Assert.AreEqual(1f, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 10f, "b", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.AreEqual(10f, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 100f, "b1", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b1", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.AreEqual(100f, result.Failures[0].Value);
        }

        [Test]
        public void Test_BetweenIntChecker()
        {
            var checker = new BetweenIntChecker<Student>(3, 10);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BetweenIntChecker<Student>(33, 10));
            Assert.AreEqual("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var result = checker.Validate(new ValidateResult(), 4, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3, "a", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(3, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 1, "a1", "c1");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c1", result.Failures[0].Error);
            Assert.AreEqual(1, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 10, "b", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.AreEqual(10, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 100, "b1", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b1", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.AreEqual(100, result.Failures[0].Value);
        }

        [Test]
        public void Test_BetweenLongChecker()
        {
            var checker = new BetweenLongChecker<Student>(3L, 10L);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BetweenLongChecker<Student>(33L, 10L));
            Assert.AreEqual("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var result = checker.Validate(new ValidateResult(), 4L, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3L, "a", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(3L, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 1L, "a1", "c1");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c1", result.Failures[0].Error);
            Assert.AreEqual(1L, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 10L, "b", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.AreEqual(10L, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 100L, "b1", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("b1", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.AreEqual(100L, result.Failures[0].Value);
        }
    }
}