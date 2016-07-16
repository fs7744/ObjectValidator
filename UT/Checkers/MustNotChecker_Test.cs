using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using Xunit;

namespace UnitTest.Checkers
{
    public class MustNotChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_MustChecker()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new MustNotChecker<ValidateContext, string>(null, _Validation));
            Assert.Equal("func", ex.ParamName);
            Assert.True(ex.Message.Contains("Can't be null"));

            var checker = new MustNotChecker<ValidateContext, string>(str => string.IsNullOrEmpty(str), _Validation);
            var result = await checker.ValidateAsync(checker.GetResult(), string.Empty, "a", null);
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Empty, result.Failures[0].Value);
            Assert.Equal(null, result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("b", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "a", "a", "b");
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);
        }
    }
}