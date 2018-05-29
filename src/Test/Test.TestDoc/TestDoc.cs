namespace NADS.TestDoc
{
    /// <summary>
    /// Test class doc.
    /// </summary>
    public class TestClass
    {
        /// <summary>
        /// Int field doc.
        /// </summary>
        public readonly int IntField;

        /// <summary>
        /// Int array field doc.
        /// </summary>
        public readonly int[] IntArrayField;

        /// <summary>
        /// Mutable field doc.
        /// </summary>
        public int MutableField;

        /// <summary>
        /// Int property doc.
        /// </summary>
        public int IntProperty => IntField;

        /// <summary>
        /// Int array property doc.
        /// </summary>
        public int IntArrayProperty => IntArrayProperty;

        /// <summary>
        /// Mutable property doc.
        /// </summary>
        public int MutableProperty
        {
            get => MutableField;
            set => MutableField++;
        }

        /// <summary>
        /// Int method doc.
        /// </summary>
        /// <param name="intParam">Int param.</param>
        /// <returns>Returns doc.</returns>
        public int IntMethod(int intParam)
        {
            return intParam++;
        }

        /// <summary>
        /// Nullable method doc.
        /// </summary>
        /// <param name="nullable">Nullable param.</param>
        /// <returns>Returns doc.</returns>
        public int? NullableMethod(int? nullable)
        {
            return nullable;
        }

        /// <summary>
        /// Ref method doc.
        /// </summary>
        /// <param name="r">Ref param.</param>
        /// <param name="o">Out param.</param>
        /// <param name="i">In param.</param>
        /// <returns>Returns doc.</returns>
        public ref readonly int RefMethod(ref int r, out int o, in int i)
        {
            o = r + i;
            return ref IntField;
        }
        
        /// <summary>
        /// Array method doc.
        /// </summary>
        /// <param name="oneDArray">1-dimensional array.</param>
        /// <param name="twoDArray">2-dimensional array.</param>
        /// <param name="jaggedArray">Jagged array.</param>
        /// <returns>Returns doc.</returns>
        public int[] ArrayMethod(int[] oneDArray, int[,] twoDArray, int[][] jaggedArray)
        {
            return new int[0];
        }

        /// <summary>
        /// Generic method doc.
        /// </summary>
        /// <param name="genericParam">Generic param.</param>
        /// <typeparam name="T">Typeparam T.</typeparam>
        /// <returns>Returns doc.</returns>
        public T GenericMethod<T>(T genericParam)
        {
            return genericParam;
        }
    }

    /// <summary>
    /// Generic class doc.
    /// </summary>
    /// <typeparam name="T">Typeparam T.</typeparam>
    public class GenericClass<T>
    {
        /// <summary>
        /// Generic field doc.
        /// </summary>
        public readonly T GenericField;

        /// <summary>
        /// Generic array field doc.
        /// </summary>
        public readonly T[] GenericArrayField;

        /// <summary>
        /// Generic property doc.
        /// </summary>
        public T GenericProperty => GenericField;

        /// <summary>
        /// Generic array property doc.
        /// </summary>
        public T[] GenericArrayProperty => GenericArrayField;

        /// <summary>
        /// Generic method doc.
        /// </summary>
        /// <param name="classParam">Class param.</param>
        /// <param name="methodParam">Method param.</param>
        /// <typeparam name="Q">Typeparam Q.</typeparam>
        /// <returns>Returns doc.</returns>
        public Q GenericMethod<Q>(T classParam, Q methodParam)
        {
            return methodParam;
        }

    }
}