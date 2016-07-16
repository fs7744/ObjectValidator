﻿using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using ObjectValidator.Entities;
using System.Text.RegularExpressions;
using Xunit;

namespace UnitTest.Checkers
{
    public class RegexChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_RegexChecker()
        {
            var checker = new RegexChecker<ValidateContext>(Syntax.EmailRegex, RegexOptions.IgnoreCase, _Validation);
            var result = await checker.ValidateAsync(checker.GetResult(), null, "a", "b");
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("b", result.Failures[0].Error);

            checker = new RegexChecker<ValidateContext>(Syntax.EmailRegex, _Validation);
            result = await checker.ValidateAsync(checker.GetResult(), "133124.com", "a", null);
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal("133124.com", result.Failures[0].Value);
            Assert.Equal("The value no match regex", result.Failures[0].Error);

            result = await checker.ValidateAsync(checker.GetResult(), "1331@24.com", "a", null);
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);
        }
    }
}