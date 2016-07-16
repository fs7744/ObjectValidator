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
    public class InListChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_InListChecker()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new InListChecker<Student, int>(null, _Validation));
            Assert.Equal("value", ex.ParamName);
            Assert.True(ex.Message.Contains("Can't be null"));

            var checker = new InListChecker<Student, int>(new List<int> { 1, 3, 4 }, _Validation);
            var result = await checker.ValidateAsync(new ValidateResult(), 1, "3a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3, "3a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 4, "3a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 5, "23a", "no 5");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("23a", result.Failures[0].Name);
            Assert.Equal("no 5", result.Failures[0].Error);
            Assert.Equal(5, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 0, "3a", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("3a", result.Failures[0].Name);
            Assert.Equal("Not in data array", result.Failures[0].Error);
            Assert.Equal(0, result.Failures[0].Value);
        }
    }
}