using System;

namespace NADS
{
    public static class Check
    {
        public static void Cond(bool condition, string msg)
        {
            if(!condition)
            { throw new Exception(msg); }
        }

        public static void Ref(object reference)
        {
            if(reference == null)
            { throw new ArgumentNullException("reference is null"); }
        }

        public static void Ref(params object[] references)
        {
            if(references == null)
            { throw new ArgumentNullException("reference is null"); }

            for(int i = 0; i < references.Length; i++)
            {
                if(references[i] == null)
                { throw new ArgumentNullException($"reference {i} is null"); }
            }
        }

        public static void CanCastTo<T>(object obj)
        {
            if(!(obj is T))
            { throw new InvalidCastException($"{obj.GetType().FullName} cannot be casted to {typeof(T).FullName}"); }
        }

        public static void CanCastTo(object obj, Type type)
        {
            if(!(type.IsAssignableFrom(obj.GetType())))
            { throw new InvalidCastException($"{obj.GetType().FullName} cannot be casted to {type.FullName}"); }
        }

		public static void Index(int i, int max, bool allowNegative = false)
		{
            if((i < 0 && !allowNegative) || i >= max)
            {
                string expected = allowNegative ? $"less than {max - 1}" : $"within 0 to {max - 1}";
                throw new IndexOutOfRangeException($"invalid index: expected {expected}, got {i}");
            }
		}

		public static void Count(int ct, int max)
		{
            if(ct < 0 || ct > max)
			{ throw new ArgumentOutOfRangeException($"invalid count: expected within 0 to {max}, got {ct}"); }
		}

        public static void Sign(long l)
		{
            if(l < 0)
			{ throw new ArgumentOutOfRangeException($"expected positive integral, got {l}"); }
		}

        public static void Sign(double d)
		{
            if(d < 0.0)
			{ throw new ArgumentOutOfRangeException($"expected positive floating point, got {d}"); }
		}

        public static void Sign(sbyte s) => Sign((long)(s));
        public static void Sign(short s) => Sign((long)(s));
        public static void Sign(int i) => Sign((long)(i));
        public static void Sign(float f) => Sign((double)(f));
    }
}