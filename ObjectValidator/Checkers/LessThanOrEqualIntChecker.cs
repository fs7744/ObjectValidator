﻿using ObjectValidator.Interfaces;

namespace ObjectValidator.Checkers
{
    public class LessThanOrEqualIntChecker<T> : BaseChecker<T, int>
    {
        private int m_Value;

        public LessThanOrEqualIntChecker(int value)
        {
            m_Value = value;
        }

        public override IValidateResult Validate(IValidateResult result, int value, string name, string error)
        {
            if (value > m_Value)
            {
                AddFailure(result, name, value,
                    error ?? string.Format("The value must less than or equal {0}", m_Value));
            }

            return result;
        }
    }
}