﻿using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System;
using Xunit;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    public class GreaterThanOrEqualChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_GreaterThanOrEqualDateTimeChecker()
        {
            var checker = new GreaterThanOrEqualDateTimeChecker<Student>(new DateTime(2017, 3, 3), _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), new DateTime(2018, 3, 3), "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), new DateTime(2016, 3, 3), "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", new DateTime(2017, 3, 3)), result.Failures[0].Error);
            Assert.Equal(new DateTime(2016, 3, 3), result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), new DateTime(2017, 3, 3), "a1", "c");
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Test_GreaterThanOrEqualDecimalChecker()
        {
            var checker = new GreaterThanOrEqualDecimalChecker<Student>(5m, _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), 6m, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3m, "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", 5m), result.Failures[0].Error);
            Assert.Equal(3m, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 5m, "a1", "c");
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Test_GreaterThanOrEqualDoubleChecker()
        {
            var checker = new GreaterThanOrEqualDoubleChecker<Student>(5d, _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), 6d, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3d, "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", 5d), result.Failures[0].Error);
            Assert.Equal(3d, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 5d, "a1", "c");
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Test_GreaterThanOrEqualFloatChecker()
        {
            var checker = new GreaterThanOrEqualFloatChecker<Student>(5f, _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), 6f, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3f, "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", 5f), result.Failures[0].Error);
            Assert.Equal(3f, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 5f, "a1", "c");
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Test_GreaterThanOrEqualIntChecker()
        {
            var checker = new GreaterThanOrEqualIntChecker<Student>(5, _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), 6, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3, "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", 5), result.Failures[0].Error);
            Assert.Equal(3, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 5, "a1", "c");
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Test_GreaterThanOrEqualLongChecker()
        {
            var checker = new GreaterThanOrEqualLongChecker<Student>(5L, _Validation);

            var result = await checker.ValidateAsync(new ValidateResult(), 6L, "", "");
            Assert.True(result.IsValid);

            result = await checker.ValidateAsync(new ValidateResult(), 3L, "a", null);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", 5L), result.Failures[0].Error);
            Assert.Equal(3L, result.Failures[0].Value);

            result = await checker.ValidateAsync(new ValidateResult(), 5L, "a1", "c");
            Assert.True(result.IsValid);
        }
    }
}