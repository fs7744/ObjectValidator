using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using Xunit;

namespace UnitTest.Checkers
{
    public class NotEqualChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_NotEqualChecker()
        {
            var checker = new NotEqualChecker<ValidateContext, string>("a", _Validation);
            var result = await checker.ValidateAsync(checker.GetResult(), "a", "b", null);
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b", result.Failures[0].Name);
            Assert.Equal("a", result.Failures[0].Value);
            Assert.Equal("The value is equal a", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), null, "a1", "b1");
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(checker.GetResult(), "bb", "a1", "b1");
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }
    }
}