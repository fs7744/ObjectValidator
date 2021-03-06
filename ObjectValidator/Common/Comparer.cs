﻿using System;

namespace ObjectValidator.Common
{
    public static class Comparer
    {
        public static bool TryCompare(IComparable value, IComparable valueToCompare, out int result)
        {
            try
            {
                Compare(value, valueToCompare, out result);
                return true;
            }
            catch
            {
                result = 0;
            }

            return false;
        }

        private static void Compare(IComparable value, IComparable valueToCompare, out int result)
        {
            try
            {
                result = value.CompareTo(valueToCompare);
            }
            catch (ArgumentException)
            {
                if (value is decimal || valueToCompare is decimal ||
                    value is double || valueToCompare is double ||
                    value is float || valueToCompare is float)
                {
                    result = Convert.ToDouble(value).CompareTo(Convert.ToDouble(valueToCompare));
                }
                else
                {
                    result = Convert.ToInt64(value).CompareTo(Convert.ToInt64(valueToCompare));
                }
            }
        }

        public static bool GetEqualsResult(IComparable value, IComparable valueToCompare)
        {
            int result;
            if (value != valueToCompare && !value.Equals(valueToCompare) && TryCompare(value, valueToCompare, out result))
            {
                return result == 0;
            }

            return true;
        }
    }
}