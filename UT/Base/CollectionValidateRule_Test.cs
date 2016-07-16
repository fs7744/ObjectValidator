using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Base
{
    public class CollectionValidateRule_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_CollectionValidateRule()
        {
            var r = new CollectionValidateRule(_Validation);
            r.NextRuleList.Add(new ValidateRule(_Validation)
            {
                ValueName = "c",
                ValidateAsyncFunc = (c, s, s2) => Task.FromResult<IValidateResult>(new ValidateResult())
            });
            r.NextRuleList.Add(new ValidateRule(_Validation)
            {
                ValueName = "a",
                ValidateAsyncFunc = (c, s, s2) => Task.FromResult<IValidateResult>(new ValidateResult(new List<ValidateFailure>()
                { new ValidateFailure() { Name = s, Error = s2, Value = c.ValidateObject } }))
            });

            r.Condition = c => false;
            var result = await r.ValidateAsync(_Validation.CreateContext(new List<int> { 1, 2 }));
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            r.Condition = null;
            result = await r.ValidateAsync(_Validation.CreateContext(new List<int> { 2, 3 }));
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("[0].a", result.Failures[0].Name);
            Assert.Equal(2, result.Failures[0].Value);

            r.Condition = c => true;
            result = await r.ValidateAsync(_Validation.CreateContext(new List<int> { 1, 2 }, ValidateOption.Continue));
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(2, result.Failures.Count);
            Assert.Equal(1, result.Failures[0].Value);
            Assert.Equal("[0].a", result.Failures[0].Name);
            Assert.Equal(2, result.Failures[1].Value);
            Assert.Equal("[1].a", result.Failures[1].Name);

            r.NextRuleList.Add(new ValidateRule(_Validation)
            {
                ValueName = "b",
                ValidateAsyncFunc = (c, s, s2) => Task.FromResult<IValidateResult>(new ValidateResult(new List<ValidateFailure>()
                { new ValidateFailure() { Name = s, Error = s2, Value = c.ValidateObject } }))
            });
            result = await r.ValidateAsync(_Validation.CreateContext(new List<int> { 1, 2 }));
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Failures.Count);
            Assert.Equal("[0].a", result.Failures[0].Name);
            Assert.Equal(1, result.Failures[0].Value);
        }
    }
}