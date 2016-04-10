using NUnit.Framework;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class GreaterThanChecker_Test
    {
        [Test]
        public void Test_GreaterThanDateTimeChecker()
        {
            var checker = new GreaterThanDateTimeChecker<Student>(new DateTime(2017, 3, 3));

            var result = checker.Validate(new ValidateResult(), new DateTime(2018, 3, 3), "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), new DateTime(2016, 3, 3), "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", new DateTime(2017, 3, 3)), result.Failures[0].Error);
            Assert.AreEqual(new DateTime(2016, 3, 3), result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), new DateTime(2017, 3, 3), "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(new DateTime(2017, 3, 3), result.Failures[0].Value);
        }

        [Test]
        public void Test_GreaterThanDecimalChecker()
        {
            var checker = new GreaterThanDecimalChecker<Student>(5m);

            var result = checker.Validate(new ValidateResult(), 6m, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3m, "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", 5m), result.Failures[0].Error);
            Assert.AreEqual(3m, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 5m, "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(5m, result.Failures[0].Value);
        }

        [Test]
        public void Test_GreaterThanDoubleChecker()
        {
            var checker = new GreaterThanDoubleChecker<Student>(5d);

            var result = checker.Validate(new ValidateResult(), 6d, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3d, "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", 5d), result.Failures[0].Error);
            Assert.AreEqual(3d, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 5d, "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(5d, result.Failures[0].Value);
        }

        [Test]
        public void Test_GreaterThanFloatChecker()
        {
            var checker = new GreaterThanFloatChecker<Student>(5f);

            var result = checker.Validate(new ValidateResult(), 6f, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3f, "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", 5f), result.Failures[0].Error);
            Assert.AreEqual(3f, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 5f, "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(5f, result.Failures[0].Value);
        }

        [Test]
        public void Test_GreaterThanIntChecker()
        {
            var checker = new GreaterThanIntChecker<Student>(5);

            var result = checker.Validate(new ValidateResult(), 6, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3, "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", 5), result.Failures[0].Error);
            Assert.AreEqual(3, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 5, "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(5, result.Failures[0].Value);
        }

        [Test]
        public void Test_GreaterThanLongChecker()
        {
            var checker = new GreaterThanLongChecker<Student>(5L);

            var result = checker.Validate(new ValidateResult(), 6L, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3L, "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", 5L), result.Failures[0].Error);
            Assert.AreEqual(3L, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 5L, "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(5L, result.Failures[0].Value);
        }
    }
}