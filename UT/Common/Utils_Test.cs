using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using Xunit;

namespace UnitTest.Common
{
    public class Utils_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public void Test_RuleFor()
        {
            var rule = _Validation.RuleFor<ValidateFailure, string>(i => i.Error);
            Assert.NotNull(rule);
            Assert.NotNull(rule.ValueGetter);
            var builder = rule as IRuleMessageBuilder<ValidateFailure, string>;
            Assert.NotNull(builder);
            Assert.Equal("Error", (builder as IValidateRuleBuilder).ValueName);

            var ex = Assert.Throws<ArgumentNullException>(() => _Validation.RuleFor<ValidateFailure, string>(null));
            Assert.NotNull(ex);
            Assert.Equal("expression", ex.ParamName);
            Assert.True(ex.Message.Contains("Can't be null"));
        }
    }
}