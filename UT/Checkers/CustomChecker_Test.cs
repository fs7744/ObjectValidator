using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using System.Collections.Generic;
using Xunit;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    public class CustomChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_CustomChecker()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CustomChecker<Student, Student>(null, _Validation));
            Assert.Equal("func", ex.ParamName);
            Assert.True(ex.Message.Contains("Can't be null"));

            var checker = new CustomChecker<Student, Student>(i => i.Age > 18 ? null : new List<ValidateFailure>() { new ValidateFailure() { Value = i.Age, Error = "age error", Name = "age" } }, _Validation);
            var result = await checker.ValidateAsync(new ValidateResult(), new Student() { Age = 19 }, "", "");
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), new Student() { Age = 10 }, "", "");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("age", result.Failures[0].Name);
            Assert.Equal("age error", result.Failures[0].Error);
            Assert.Equal(10, result.Failures[0].Value);
        }
    }
}