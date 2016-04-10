using NUnit.Framework;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class LessThanChecker_Test
    {
        [Test]
        public void Test_LessThanDateTimeChecker()
        {
            var checker = new LessThanDateTimeChecker<Student>(new DateTime(2017, 3, 3));

            var result = checker.Validate(new ValidateResult(), new DateTime(2018, 3, 3), "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", new DateTime(2017, 3, 3)), result.Failures[0].Error);
            Assert.AreEqual(new DateTime(2018, 3, 3), result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), new DateTime(2016, 3, 3), "a", null);
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), new DateTime(2017, 3, 3), "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(new DateTime(2017, 3, 3), result.Failures[0].Value);
        }

        [Test]
        public void Test_LessThanDecimalChecker()
        {
            var checker = new LessThanDecimalChecker<Student>(5m);

            var result = checker.Validate(new ValidateResult(), 3m, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 6m, "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", 5m), result.Failures[0].Error);
            Assert.AreEqual(6m, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 5m, "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(5m, result.Failures[0].Value);
        }

        [Test]
        public void Test_LessThanDoubleChecker()
        {
            var checker = new LessThanDoubleChecker<Student>(5d);

            var result = checker.Validate(new ValidateResult(), 3d, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 7d, "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", 5d), result.Failures[0].Error);
            Assert.AreEqual(7d, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 5d, "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(5d, result.Failures[0].Value);
        }

        [Test]
        public void Test_LessThanFloatChecker()
        {
            var checker = new LessThanFloatChecker<Student>(5f);

            var result = checker.Validate(new ValidateResult(), 2f, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 8f, "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", 5f), result.Failures[0].Error);
            Assert.AreEqual(8f, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 5f, "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(5f, result.Failures[0].Value);
        }

        [Test]
        public void Test_LessThanIntChecker()
        {
            var checker = new LessThanIntChecker<Student>(5);

            var result = checker.Validate(new ValidateResult(), 1, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 9, "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", 5), result.Failures[0].Error);
            Assert.AreEqual(9, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 5, "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(5, result.Failures[0].Value);
        }

        [Test]
        public void Test_LessThanLongChecker()
        {
            var checker = new LessThanLongChecker<Student>(5L);

            var result = checker.Validate(new ValidateResult(), 3L, "", "");
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 8L, "a", null);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", 5L), result.Failures[0].Error);
            Assert.AreEqual(8L, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 5L, "a1", "c");
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("a1", result.Failures[0].Name);
            Assert.AreEqual("c", result.Failures[0].Error);
            Assert.AreEqual(5L, result.Failures[0].Value);
        }
    }
}