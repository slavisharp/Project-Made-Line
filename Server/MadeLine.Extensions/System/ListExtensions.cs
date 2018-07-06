namespace System.Collections.Generic
{
    using System.Linq;

    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> item)
        {
            return item == null || item.Any();
        }

        public static bool IsNullOrEmpty<T>(this IList<T> item)
        {
            return item == null || item.Count == 0;
        }

        public static bool IsNotEmpty<T>(this IList<T> item)
        {
            return !item.IsNullOrEmpty();
        }
    }
}
