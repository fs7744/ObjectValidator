using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using Xunit;

namespace UnitTest.Checkers
{
    public class EqualChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_EqualChecker()
        {
            var checker = new EqualChecker<ValidateContext, string>("a", _Validation);
            var result = await checker.ValidateAsync(checker.GetResult(), "b", "a", null);
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal("b", result.Failures[0].Value);
            Assert.Equal("The value is not equal a", result.Failures[0].Error);

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