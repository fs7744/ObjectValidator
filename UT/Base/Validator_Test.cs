using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Base
{
    public class Validator_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public void Test_Validator_SetRules()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Validator(_Validation).SetRules(null);
            });
        }

        [Fact]
        public async void Test_Validator_Validate()
        {
            var v = new Validator(_Validation);
            var rule = new ValidateRule(_Validation)
            {
                ValidateAsyncFunc = (c, name, error) =>
                {
                    var f = new ValidateFailure()
                    {
                        Name = name,
                        Error = error,
                        Value = c
                    };
                    return Task.FromResult<IValidateResult>(new ValidateResult(new List<ValidateFailure>() { f }));
                }
            };

            var context = new ValidateContext() { RuleSelector = new RuleSelector() };
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);

            v.SetRules(new List<ValidateRule>() { rule });
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(1, result.Failures.Count);

            rule.ValidateAsyncFunc = (c, name, error) =>
            {
                return Task.FromResult<IValidateResult>(new ValidateResult());
            };

            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.NotNull(result.Failures);
            Assert.Equal(0, result.Failures.Count);
        }

        [Fact]
        public async void Test_Validator_Exception()
        {
            var v = new Validator(_Validation);
            var rule = new ValidateRule(_Validation)
            {
                ValidateAsyncFunc = (c, name, error) =>
                {
                    throw new Exception();
                }
            };
            v.SetRules(new List<ValidateRule>() { rule });

            var context = new ValidateContext() { RuleSelector = new RuleSelector() };
            await Assert.ThrowsAsync<AggregateException>(async () => { var a = await v.ValidateAsync(context); });
        }
    }
}