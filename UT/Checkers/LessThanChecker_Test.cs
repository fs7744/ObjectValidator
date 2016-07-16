using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using Xunit;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    public class LessThanChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_LessThanDateTimeChecker()
        {
            var checker = new LessThanDateTimeChecker<Student>(new DateTime(2017, 3, 3), _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), new DateTime(2018, 3, 3), "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", new DateTime(2017, 3, 3)), result.Failures[0].Error);
            Assert.Equal(new DateTime(2018, 3, 3), result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), new DateTime(2016, 3, 3), "a", null);
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), new DateTime(2017, 3, 3), "a1", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(new DateTime(2017, 3, 3), result.Failures[0].Value);
        }

        [Fact]
        public async void Test_LessThanDecimalChecker()
        {
            var checker = new LessThanDecimalChecker<Student>(5m, _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), 3m, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 6m, "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", 5m), result.Failures[0].Error);
            Assert.Equal(6m, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 5m, "a1", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(5m, result.Failures[0].Value);
        }

        [Fact]
        public async void Test_LessThanDoubleChecker()
        {
            var checker = new LessThanDoubleChecker<Student>(5d, _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), 3d, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 7d, "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", 5d), result.Failures[0].Error);
            Assert.Equal(7d, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 5d, "a1", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(5d, result.Failures[0].Value);
        }

        [Fact]
        public async void Test_LessThanFloatChecker()
        {
            var checker = new LessThanFloatChecker<Student>(5f, _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), 2f, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 8f, "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", 5f), result.Failures[0].Error);
            Assert.Equal(8f, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 5f, "a1", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(5f, result.Failures[0].Value);
        }

        [Fact]
        public async void Test_LessThanIntChecker()
        {
            var checker = new LessThanIntChecker<Student>(5, _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), 1, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 9, "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", 5), result.Failures[0].Error);
            Assert.Equal(9, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 5, "a1", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(5, result.Failures[0].Value);
        }

        [Fact]
        public async void Test_LessThanLongChecker()
        {
            var checker = new LessThanLongChecker<Student>(5L, _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), 3L, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 8L, "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", 5L), result.Failures[0].Error);
            Assert.Equal(8L, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 5L, "a1", "c");
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a1", result.Failures[0].Name);
            Assert.Equal("c", result.Failures[0].Error);
            Assert.Equal(5L, result.Failures[0].Value);
        }
    }
}