using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using Xunit;

namespace UnitTest.Checkers
{
    public class NotNullChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_NotNullChecker()
        {
            var checker = new NotNullChecker<ValidateContext, string>(_Validation);
            var result = await checker.ValidateAsync(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("b", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), null, "a", null);
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("Can't be null", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "a", "a", "b");
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);

            result = await checker.ValidateAsync(checker.GetResult(), string.Empty, "a", "b");
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);
        }

        [Fact]
        public async void Test_NullableNotNullChecker()
        {
            var checker = new NullableNotNullChecker<ValidateContext, int>(_Validation);
            var result = await checker.ValidateAsync(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("b", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), null, "a", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("Can't be null", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), 1, "a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Test_NotNullOrEmptyStringChecker()
        {
            var checker = new NotNullOrEmptyStringChecker<ValidateContext>(_Validation);
            var result = await checker.ValidateAsync(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("b", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), null, "a", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "", "a1", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("", result.Failures[0].Value);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), " ", "a1", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(checker.GetResult(), string.Empty, "a2", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a2", result.Failures[0].Name);
            Assert.Equal(string.Empty, result.Failures[0].Value);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "s", "a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Test_NotNullOrWhiteSpaceChecker()
        {
            var checker = new NotNullOrWhiteSpaceChecker<ValidateContext>(_Validation);
            var result = await checker.ValidateAsync(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("b", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), null, "a", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("Can't be null or empty or whitespace", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "", "a1", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("", result.Failures[0].Value);
            Assert.Equal("Can't be null or empty or whitespace", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), " ", "a1", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal(" ", result.Failures[0].Value);
            Assert.Equal("Can't be null or empty or whitespace", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), string.Empty, "a2", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a2", result.Failures[0].Name);
            Assert.Equal(string.Empty, result.Failures[0].Value);
            Assert.Equal("Can't be null or empty or whitespace", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "s", "a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Test_NotNullOrEmptyListChecker()
        {
            var checker = new NotNullOrEmptyListChecker<ValidateContext, string>(_Validation);
            var result = await checker.ValidateAsync(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("b", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), null, "a", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "", "a1", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("", result.Failures[0].Value);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), " ", "a1", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(checker.GetResult(), string.Empty, "a2", null);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a2", result.Failures[0].Name);
            Assert.Equal(string.Empty, result.Failures[0].Value);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "s", "a", null);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }
    }
}