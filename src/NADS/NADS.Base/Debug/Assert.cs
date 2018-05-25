using System;
using System.Diagnostics;
using SDebug = System.Diagnostics.Debug;

namespace NADS.Debug
{
    public static class Assert
    {
        public static void Cond(bool condition, string msg)
        {
            SDebug.Assert(condition, msg);
        }

        public static void Ref(object reference)
        {
            SDebug.Assert(reference != null, "reference is null");
        }

        public static void Ref(params object[] references)
        {
			SDebug.Assert(references != null, "reference is null");

			for(int i = 0; i < references.Length; i++)
			{ SDebug.Assert(references[i] != null, $"reference {i} is null"); }
        }

        public static void CanCastTo<T>(object obj)
        {
            SDebug.Assert(obj is T, $"{obj.GetType().FullName} cannot be casted to {typeof(T).FullName}");
        }

        public static void CanCastTo(object obj, Type type)
        {
            SDebug.Assert(type.IsAssignableFrom(obj.GetType()), $"{obj.GetType().FullName} cannot be casted to {type.FullName}");
        }

		public static void Index(int i, int max, bool allowNegative = false)
		{
			string expected = allowNegative ? $"less than {max - 1}" : $"within 0 to {max - 1}";
			SDebug.Assert((i > -1 || allowNegative) && i < max, $"invalid index: expected {expected}, got {i}");
		}

		public static void Count(int ct, int max)
		{
            SDebug.Assert(ct > -1 && ct <= max, $"invalid count: expected within 0 to {max}, got {ct}");
		}

        public static void Sign(long l)
		{
            SDebug.Assert(l >= 0, $"expected positive integral, got {l}");
		}

        public static void Sign(double d)
		{
			SDebug.Assert(d >= 0.0, $"expected positive floating point, got {d}");
		}

        public static void Sign(sbyte s) => Sign((long)(s));
        public static void Sign(short s) => Sign((long)(s));
        public static void Sign(int i) => Sign((long)(i));
        public static void Sign(float f) => Sign((double)(f));
    }
}