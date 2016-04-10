using NUnit.Framework;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using System.Collections.Generic;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class CustomChecker_Test
    {
        [Test]
        public void Test_CustomChecker()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CustomChecker<Student, Student>(null));
            Assert.AreEqual("func", ex.ParamName);
            Assert.True(ex.Message.Contains("Can't be null"));

            var checker = new CustomChecker<Student, Student>(i => i.Age > 18 ? null : new List<ValidateFailure>() { new ValidateFailure() { Value = i.Age, Error = "age error", Name = "age" } });
            var result = checker.Validate(new ValidateResult(), new Student() { Age = 19 }, "", "");
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = checker.Validate(new ValidateResult(), new Student() { Age = 10 }, "", "");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(1, result.Failures.Count);
            Assert.AreEqual("age", result.Failures[0].Name);
            Assert.AreEqual("age error", result.Failures[0].Error);
            Assert.AreEqual(10, result.Failures[0].Value);
        }
    }
}