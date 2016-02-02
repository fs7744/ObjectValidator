using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;

namespace UnitTest.Base
{
    [TestFixture]
    public class RuleBuilder_Test
    {
        [TestFixtureSetUp]
        public void SetContainer()
        {
            Container.Init();
        }

        [TestFixtureTearDown]
        public void ClearContainer()
        {
            Container.Clear();
        }

        [Test]
        public void Test_RuleBuilder_NoProperty()
        {
            var v = new RuleBuilder<ValidateFailure, ValidateFailure>();
            v.SetValueGetter(i => i);
            Assert.AreEqual(string.Empty, v.ValueName);
            var result = new ValidateFailure();
            Assert.AreEqual(result, v.ValueGetter((object)result));
        }

        [Test]
        public void Test_RuleBuilder_OneLevelProperty()
        {
            var v = new RuleBuilder<ValidateFailure, string>();
            v.SetValueGetter(i => i.Error);
            Assert.AreEqual("Error", v.ValueName);
            var result = new ValidateFailure()
            {
                Error = "a"
            };
            Assert.AreEqual(result.Error, v.ValueGetter((object)result));
        }

        public class TestRuleBuilderClass
        {
            public ValidateFailure Failure { get; set; }
        }

        [Test]
        public void Test_RuleBuilder_TwoLevelProperty()
        {
            var v = new RuleBuilder<TestRuleBuilderClass, string>();
            v.SetValueGetter(i => i.Failure.Error);
            Assert.AreEqual("Failure.Error", v.ValueName);
            var result = new TestRuleBuilderClass()
            {
                Failure = new ValidateFailure()
                {
                    Error = "ab"
                }
            };
            Assert.AreEqual(result.Failure.Error, v.ValueGetter((object)result));
        }

        [Test]
        public void Test_RuleBuilder_OverrideName()
        {
            var v = new RuleBuilder<TestRuleBuilderClass, string>();
            v.SetValueGetter(i => i.Failure.Error);
            Assert.AreEqual("Failure.Error", v.ValueName);
            var name = "go";
            v.ValueName = name;
            Assert.AreEqual(name, v.ValueName);
        }

        [Test]
        public void Test_RuleBuilder_OverrideError()
        {
            var v = new RuleBuilder<TestRuleBuilderClass, string>();
            v.SetValueGetter(i => i.Failure.Error);
            Assert.AreEqual(null, v.Error);
            var error = "go";
            v.Error = error;
            Assert.AreEqual(error, v.Error);
        }

        [Test]
        public void Test_RuleBuilder_ThenRuleFor()
        {
            var v = new RuleBuilder<TestRuleBuilderClass, string>();
            var next = v.ThenRuleFor(i => i.Failure);
            Assert.IsNotNull(next);
            Assert.AreNotEqual(v, next);
            Assert.IsNotNull((next as IRuleBuilder<TestRuleBuilderClass, ValidateFailure>).ValueGetter);
            var builder = next as IRuleMessageBuilder<TestRuleBuilderClass, ValidateFailure>;
            Assert.IsNotNull(builder);
            Assert.AreEqual("Failure", (builder as IValidateRuleBuilder).ValueName);
        }

        [Test]
        public void Test_RuleBuilder_Build()
        {
            var v = new RuleBuilder<TestRuleBuilderClass, string>();
            v.ValidateFunc = (e, o, s) => { return null; };
            v.SetValueGetter(i => i.Failure.Error);
            v.Condition = (d) => { return false; };
            v.Error = "ok";
            var next = v.ThenRuleFor(i => i.Failure);
            var r = v.Build();
            Assert.IsNotNull(r);
            Assert.IsNotNull(r.NextRuleList);
            Assert.IsNotNull(r.ValidateFunc);
            Assert.IsNotNull(r.Condition);
            Assert.AreEqual(v.ValidateFunc, r.ValidateFunc);
            Assert.AreEqual(v.Condition, r.Condition);
            Assert.AreEqual("Failure.Error", r.ValueName);
            Assert.AreEqual("ok", r.Error);
        }
    }
}