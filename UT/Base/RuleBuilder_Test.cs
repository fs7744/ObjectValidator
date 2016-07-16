using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using Xunit;

namespace UnitTest.Base
{
    public class RuleBuilder_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public void Test_RuleBuilder_NoProperty()
        {
            var v = new RuleBuilder<ValidateFailure, ValidateFailure>(_Validation);
            v.SetValueGetter(i => i);
            Assert.Equal(string.Empty, v.ValueName);
            var result = new ValidateFailure();
            Assert.Equal(result, v.ValueGetter((object)result));
        }

        [Fact]
        public void Test_RuleBuilder_OneLevelProperty()
        {
            var v = new RuleBuilder<ValidateFailure, string>(_Validation);
            v.SetValueGetter(i => i.Error);
            Assert.Equal("Error", v.ValueName);
            var result = new ValidateFailure()
            {
                Error = "a"
            };
            Assert.Equal(result.Error, v.ValueGetter(result));
        }

        public class TestRuleBuilderClass
        {
            public ValidateFailure Failure { get; set; }
        }

        [Fact]
        public void Test_RuleBuilder_TwoLevelProperty()
        {
            var v = new RuleBuilder<TestRuleBuilderClass, string>(_Validation);
            v.SetValueGetter(i => i.Failure.Error);
            Assert.Equal("Failure.Error", v.ValueName);
            var result = new TestRuleBuilderClass()
            {
                Failure = new ValidateFailure()
                {
                    Error = "ab"
                }
            };
            Assert.Equal(result.Failure.Error, v.ValueGetter(result));
        }

        [Fact]
        public void Test_RuleBuilder_OverrideName()
        {
            var v = new RuleBuilder<TestRuleBuilderClass, string>(_Validation);
            v.SetValueGetter(i => i.Failure.Error);
            Assert.Equal("Failure.Error", v.ValueName);
            var name = "go";
            v.ValueName = name;
            Assert.Equal(name, v.ValueName);
        }

        [Fact]
        public void Test_RuleBuilder_OverrideError()
        {
            var v = new RuleBuilder<TestRuleBuilderClass, string>(_Validation);
            v.SetValueGetter(i => i.Failure.Error);
            Assert.Equal(null, v.Error);
            var error = "go";
            v.Error = error;
            Assert.Equal(error, v.Error);
        }

        [Fact]
        public void Test_RuleBuilder_ThenRuleFor()
        {
            var v = new RuleBuilder<TestRuleBuilderClass, string>(_Validation);
            var next = v.ThenRuleFor(i => i.Failure);
            Assert.NotNull(next);
            Assert.NotSame(v, next);
            Assert.NotNull((next as IRuleBuilder<TestRuleBuilderClass, ValidateFailure>).ValueGetter);
            var builder = next as IRuleMessageBuilder<TestRuleBuilderClass, ValidateFailure>;
            Assert.NotNull(builder);
            Assert.Equal("Failure", (builder as IValidateRuleBuilder).ValueName);
        }

        [Fact]
        public void Test_RuleBuilder_Build()
        {
            var v = new RuleBuilder<TestRuleBuilderClass, string>(_Validation);
            v.ValidateAsyncFunc = (e, o, s) => { return null; };
            v.SetValueGetter(i => i.Failure.Error);
            v.Condition = (d) => { return false; };
            v.Error = "ok";
            var next = v.ThenRuleFor(i => i.Failure);
            var r = v.Build();
            Assert.NotNull(r);
            Assert.NotNull(r.NextRuleList);
            Assert.NotNull(r.ValidateAsyncFunc);
            Assert.NotNull(r.Condition);
            Assert.Equal(v.ValidateAsyncFunc, r.ValidateAsyncFunc);
            Assert.Equal(v.Condition, r.Condition);
            Assert.Equal("Failure.Error", r.ValueName);
            Assert.Equal("ok", r.Error);
        }
    }
}