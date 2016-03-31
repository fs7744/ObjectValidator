using System;

namespace ObjectValidator.Common
{
    public class ParamHelper
    {
        public static void CheckParamEmptyOrNull(string param, string paramName, string message)
        {
            CheckParamNull(param, paramName, message);
            if (string.IsNullOrEmpty(param))
                throw new ArgumentException(message, paramName);
        }

        public static void CheckParamNull(object param, string paramName, string message)
        {
            if (param == null)
                throw new ArgumentNullException(paramName, message);
        }
    }
}