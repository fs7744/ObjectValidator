using System.Collections;

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