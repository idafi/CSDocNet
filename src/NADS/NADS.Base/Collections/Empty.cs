using System.Collections.Generic;

namespace NADS.Collections
{
    public static class Empty<T>
    {
        static T[] empty;

        static Empty()
        {
            empty = new T[0];
        }

        public static IEnumerable<T> EmptyEnumerable => empty;
        public static IReadOnlyCollection<T> EmptyCollection => empty;
        public static IReadOnlyList<T> EmptyList => empty;
    }

    public static class Empty<TKey, TValue>
    {
        static Dictionary<TKey, TValue> empty;

        static Empty()
        {
            empty = new Dictionary<TKey, TValue>();
        }

        public static IReadOnlyDictionary<TKey, TValue> EmptyDict => empty;
    }
}