using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Common;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Base
{
    public class ValidateRule_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_ValidateAsyncByFunc()
        {
            var rule = new ValidateRule(_Validation);
            rule.ValueName = "a";
            Func<ValidateContext, string, string, Task<IValidateResult>> failed = (context, name, error) =>
            {
                var f = new ValidateFailure()
                {
                    Name = name,
                    Error = error,
                    Value = context
                };
                return Task.FromResult<IValidateResult>(new ValidateResult(new List<ValidateFailure>() { f }));
            };
            rule.ValidateAsyncFunc = failed;
            var result = await rule.ValidateAsyncByFunc(new ValidateContext());
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);

            rule.NextRuleList.Add(new ValidateRule(_Validation) { ValueName = "b", ValidateAsyncFunc = failed });
            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.StopOnFirstFailure });
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);

            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.Continue });
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.Equal(2, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);
            Assert.Equal("b", result.Failures[1].Name);

            Func<ValidateContext, string, string, Task<IValidateResult>> successed = (context, name, error) =>
            {
                return Task.FromResult<IValidateResult>(new ValidateResult());
            };
            rule.NextRuleList[0].ValidateAsyncFunc = successed;
            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.StopOnFirstFailure });
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);

            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.Continue });
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);

            rule.ValidateAsyncFunc = successed;
            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.StopOnFirstFailure });
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.Equal(0, result.Failures.Count);

            rule.NextRuleList.Add(new ValidateRule(_Validation) { ValueName = "c", ValidateAsyncFunc = failed });
            rule.NextRuleList.Add(new ValidateRule(_Validation) { ValueName = "d", ValidateAsyncFunc = failed });
            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.StopOnFirstFailure });
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("c", result.Failures[0].Name);
            rule.NextRuleList.RemoveAt(1);
            rule.NextRuleList.RemoveAt(1);

            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.Continue });
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.Equal(0, result.Failures.Count);

            rule.NextRuleList[0].ValidateAsyncFunc = failed;
            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.StopOnFirstFailure });
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b", result.Failures[0].Name);

            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.Continue });
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("b", result.Failures[0].Name);

            rule.NextRuleList.Clear();
            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.Continue });
            Assert.Equal(true, result.IsValid);
            Assert.Equal(0, result.Failures.Count);

            result = await rule.ValidateAsyncByFunc(new ValidateContext() { Option = ValidateOption.StopOnFirstFailure });
            Assert.Equal(true, result.IsValid);
            Assert.Equal(0, result.Failures.Count);
        }

        [Fact]
        public async void Test_Validate()
        {
            var rule = new ValidateRule(_Validation);
            rule.ValueName = "a";
            Func<ValidateContext, string, string, Task<IValidateResult>> failed = (context, name, error) =>
            {
                var f = new ValidateFailure()
                {
                    Name = name,
                    Error = error,
                    Value = context
                };
                return Task.FromResult<IValidateResult>(new ValidateResult(new List<ValidateFailure>() { f }));
            };
            rule.ValidateAsyncFunc = failed;
            var result = await rule.ValidateAsync(new ValidateContext());
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);

            rule.Condition = (context) => { return context.RuleSetList.IsEmptyOrNull(); };
            result = await rule.ValidateAsync(new ValidateContext());
            Assert.NotNull(result);
            Assert.Equal(false, result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("a", result.Failures[0].Name);

            result = await rule.ValidateAsync(new ValidateContext() { RuleSetList = new List<string>() { "A" } });
            Assert.NotNull(result);
            Assert.Equal(true, result.IsValid);
            Assert.Equal(0, result.Failures.Count);
        }
    }
}