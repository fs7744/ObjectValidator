using ObjectValidator.Entities;
using ObjectValidator.Interfaces;
using System.Collections.Generic;

namespace ObjectValidator
{
    public class ValidateContext
    {
        public IRuleSelector RuleSelector { get; set; }

        public IEnumerable<string> RuleSetList { get; set; }

        public ValidateOption Option { get; set; }

        public object ValidateObject { get; set; }
    }
}