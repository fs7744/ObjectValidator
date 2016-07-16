using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using Xunit;

namespace UnitTest.Checkers
{
    public class LengthChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_LengthChecker()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new LengthChecker<ValidateContext>(5, 1, _Validation));
            Assert.Equal("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var checker = new LengthChecker<ValidateContext>(2, -1, _Validation);
            var result = await checker.ValidateAsync(checker.GetResult(), string.Empty, "a", null);
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Empty, result.Failures[0].Value);
            Assert.Equal("The length 0 is not between 2 and -1", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), null, "a", "not");
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("not", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "2", "a", "not");
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal("2", result.Failures[0].Value);
            Assert.Equal("not", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "2221`3`212312312", "a", "not");
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);

            checker = new LengthChecker<ValidateContext>(0, 5, _Validation);

            result = await checker.ValidateAsync(checker.GetResult(), "2", "a", "not");
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);

            result = await checker.ValidateAsync(checker.GetResult(), null, "a", "not");
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);

            result = await checker.ValidateAsync(checker.GetResult(), string.Empty, "a", "not");
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);

            result = await checker.ValidateAsync(checker.GetResult(), "12345", "a", "not");
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);

            result = await checker.ValidateAsync(checker.GetResult(), "123456", "a", "not");
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal("123456", result.Failures[0].Value);
            Assert.Equal("not", result.Failures[0].Error);
        }
    }
}