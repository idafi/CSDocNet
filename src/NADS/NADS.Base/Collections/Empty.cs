using System.Collections.Generic;

namespace NADS.Collections
{
    public static class Empty<T>
    {
        public static IEnumerable<T> EmptyEnumerable => empty;
        public static IReadOnlyCollection<T> EmptyCollection => empty;
        public static IReadOnlyList<T> EmptyList => empty;

        static T[] empty;

        static Empty()
        {
            empty = new T[0];
        }
    }
}