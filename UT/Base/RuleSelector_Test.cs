using ObjectValidator.Base;
using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Base
{
    public class RuleSelector_Test
    {
        public class TestValidateRule : IValidateRule
        {
            public Func<ValidateContext, bool> Condition { get; set; }

            public string Error { get; set; }

            public List<IValidateRule> NextRuleList { get; set; }

            public string RuleSet { get; set; }

            public Func<ValidateContext, string, string, Task<IValidateResult>> ValidateAsyncFunc { get; set; }

            public string ValueName { get; set; }

            public IValidateResult Validate(ValidateContext context)
            {
                throw new NotImplementedException();
            }

            public Task<IValidateResult> ValidateAsync(ValidateContext context)
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

        [Fact]
        public void Test_RuleSelector()
        {
            var rule = new TestValidateRule();
            var context = new ValidateContext();
            var selector = new RuleSelector();
            m_Data.ForEach(i =>
            {
                rule.RuleSet = i.Item1;
                context.RuleSetList = i.Item2;

                Assert.Equal(i.Item3, selector.CanExecute(rule, context));
            });
        }
    }
}