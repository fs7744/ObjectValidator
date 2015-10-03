using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Base;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;

namespace UnitTest.Base
{
    [TestFixture]
    public class RuleSelector_Test
    {
        public class TestValidateRule : IValidateRule
        {
            public Func<ValidateContext, bool> Condition { get; set; }

            public string Error { get; set; }

            public IValidateRule NextRule { get; set; }

            public string RuleSet { get; set; }

            public Func<ValidateContext, string, string, IValidateResult> ValidateFunc { get; set; }

            public string ValueName { get; set; }

            public IValidateResult Validate(ValidateContext context)
            {
                throw new NotImplementedException();
            }
        }

        private static List<Tuple<string, IEnumerable<string>, bool>> m_Data
            = new List<Tuple<string, IEnumerable<string>, bool>>()
            {
                Tuple.Create<string, IEnumerable<string>, bool>("",null,true)
                ,Tuple.Create<string, IEnumerable<string>, bool>(null,null,true)
                ,Tuple.Create<string, IEnumerable<string>, bool>(string.Empty,null,true)
                ,Tuple.Create<string, IEnumerable<string>, bool>("A",null,true)
                ,Tuple.Create<string, IEnumerable<string>, bool>("",new List<string>(),true)
                ,Tuple.Create<string, IEnumerable<string>, bool>(null,new List<string>(),true)
                ,Tuple.Create<string, IEnumerable<string>, bool>(string.Empty,new List<string>(),true)
                ,Tuple.Create<string, IEnumerable<string>, bool>("A",new List<string>(),true)
                ,Tuple.Create<string, IEnumerable<string>, bool>("",new List<string>() { "B"},true)
                ,Tuple.Create<string, IEnumerable<string>, bool>(null,new List<string>(){ "B"},true)
                ,Tuple.Create<string, IEnumerable<string>, bool>(string.Empty,new List<string>(){ "B"},true)
                ,Tuple.Create<string, IEnumerable<string>, bool>("A",new List<string>(){ "B"},false)
                ,Tuple.Create<string, IEnumerable<string>, bool>("A",new List<string>(){ "A"},true)
                ,Tuple.Create<string, IEnumerable<string>, bool>("A",new List<string>(){ "A","B"},true)
                ,Tuple.Create<string, IEnumerable<string>, bool>("A",new List<string>(){ "a","B"},false)
            };

        [Test]
        public void Test_RuleSelector()
        {
            var rule = new TestValidateRule();
            var context = new ValidateContext();
            var selector = new RuleSelector();
            m_Data.ForEach(i =>
            {
                rule.RuleSet = i.Item1;
                context.RuleSetList = i.Item2;

                Assert.AreEqual(i.Item3, selector.CanExecute(rule, context));
            });
        }
    }
}