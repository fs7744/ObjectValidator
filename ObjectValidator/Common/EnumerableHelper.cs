using System.Collections;
using System.Collections.Generic;

namespace ObjectValidator.Common
{
    public static class EnumerableHelper
    {
        public static bool IsEmptyOrNull(this IEnumerable list)
        {
            return list == null || !list.GetEnumerator().MoveNext();
        }
    }
}