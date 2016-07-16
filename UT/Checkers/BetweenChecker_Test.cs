using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using Xunit;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    public class BetweenChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_BetweenDecimalChecker()
        {
            var checker = new BetweenDecimalChecker<Student>(3m, 10m, _Validation);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BetweenDecimalChecker<Student>(33m, 10m, _Validation));
            Assert.Equal("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var result = await checker.ValidateAsync(new ValidateResult(), 4m, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3m, "a", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(3m, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 1m, "a1", "c1");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c1", result.Failures[0].Error);
            Assert.Equal(1m, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 10m, "b", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.Equal(10m, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 100m, "b1", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b1", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.Equal(100m, result.Failures[0].Value);
        }

        [Fact]
        public async void Test_BetweenDoubleChecker()
        {
            var checker = new BetweenDoubleChecker<Student>(3d, 10d, _Validation);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BetweenDoubleChecker<Student>(33d, 10d, _Validation));
            Assert.Equal("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var result = await checker.ValidateAsync(new ValidateResult(), 4d, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3d, "a", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(3d, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 1d, "a1", "c1");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c1", result.Failures[0].Error);
            Assert.Equal(1d, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 10d, "b", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.Equal(10d, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 100d, "b1", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b1", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.Equal(100d, result.Failures[0].Value);
        }

        [Fact]
        public async void Test_BetweenFloatChecker()
        {
            var checker = new BetweenFloatChecker<Student>(3f, 10f, _Validation);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BetweenFloatChecker<Student>(33f, 10f, _Validation));
            Assert.Equal("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var result = await checker.ValidateAsync(new ValidateResult(), 4f, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3f, "a", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(3f, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 1f, "a1", "c1");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c1", result.Failures[0].Error);
            Assert.Equal(1f, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 10f, "b", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.Equal(10f, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 100f, "b1", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b1", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.Equal(100f, result.Failures[0].Value);
        }

        [Fact]
        public async void Test_BetweenIntChecker()
        {
            var checker = new BetweenIntChecker<Student>(3, 10, _Validation);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BetweenIntChecker<Student>(33, 10, _Validation));
            Assert.Equal("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var result = await checker.ValidateAsync(new ValidateResult(), 4, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3, "a", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(3, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 1, "a1", "c1");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c1", result.Failures[0].Error);
            Assert.Equal(1, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 10, "b", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.Equal(10, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 100, "b1", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b1", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.Equal(100, result.Failures[0].Value);
        }

        [Fact]
        public async void Test_BetweenLongChecker()
        {
            var checker = new BetweenLongChecker<Student>(3L, 10L, _Validation);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new BetweenLongChecker<Student>(33L, 10L, _Validation));
            Assert.Equal("max", ex.ParamName);
            Assert.True(ex.Message.Contains("Max should be larger than min."));

            var result = await checker.ValidateAsync(new ValidateResult(), 4L, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3L, "a", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(3L, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 1L, "a1", "c1");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c1", result.Failures[0].Error);
            Assert.Equal(1L, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 10L, "b", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.Equal(10L, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 100L, "b1", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b1", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 10", result.Failures[0].Error);
            Assert.Equal(100L, result.Failures[0].Value);
        }
    }
}