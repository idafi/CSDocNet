using System.Collections.Generic;

namespace CSDocNet.Collections
{
    public static class Empty<T>
    {
        static readonly T[] empty;

        static Empty()
        {
            empty = new T[0];
        }

        public static T[] Array => empty;
        public static IEnumerable<T> Enumerable => empty;
        public static IReadOnlyCollection<T> Collection => empty;
        public static IReadOnlyList<T> List => empty;
    }

    public static class Empty<TKey, TValue>
    {
        static Dictionary<TKey, TValue> empty;

        static Empty()
        {
            empty = new Dictionary<TKey, TValue>();
        }

        public static IReadOnlyDictionary<TKey, TValue> Dict => empty;
    }
}