﻿using ObjectValidator.Common;
using ObjectValidator.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ObjectValidator.Checkers
{
    public class InListChecker<T, TProperty> : NotEqualChecker<T, TProperty>
    {
        private IEnumerable<TProperty> m_Value;

        public InListChecker(IEnumerable<TProperty> value)
            : base(default(TProperty))
        {
            ParamHelper.CheckParamNull(value, "value", "Can't be null");
            m_Value = value;
        }

        public override IValidateResult Validate(IValidateResult result, TProperty value, string name, string error)
        {
            if (!m_Value.Any(i => Compare(i, value)))
            {
                AddFailure(result, name, value, error ?? "Not in data array");
            }
            return result;
        }
    }
}