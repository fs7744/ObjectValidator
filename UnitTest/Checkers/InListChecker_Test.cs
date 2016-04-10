using NUnit.Framework;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using System.Collections.Generic;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class InListChecker_Test
    {
        [Test]
        public void Test_InListChecker()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new InListChecker<Student, int>(null));
            Assert.AreEqual("value", ex.ParamName);
            Assert.True(ex.Message.Contains("Can't be null"));

            var checker = new InListChecker<Student, int>(new List<int> { 1, 3, 4 });
            var result = checker.Validate(new ValidateResult(), 1, "3a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 3, "3a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 4, "3a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), 5, "23a", "no 5");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("23a", result.Failures[0].Name);
            Assert.AreEqual("no 5", result.Failures[0].Error);
            Assert.AreEqual(5, result.Failures[0].Value);

            result = checker.Validate(new ValidateResult(), 0, "3a", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("3a", result.Failures[0].Name);
            Assert.AreEqual("Not in data array", result.Failures[0].Error);
            Assert.AreEqual(0, result.Failures[0].Value);
        }
    }
}