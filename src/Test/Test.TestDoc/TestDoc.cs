namespace NADS.TestDoc
{
    /// <summary>
    /// Test class doc.
    /// </summary>
    public class TestClass
    {
        /// <summary>
        /// Nested class doc.
        /// </summary>
        public class NestedClass
        {
            /// <summary>
            /// Nested nested class doc.
            /// </summary>
            public class NestedNestedClass
            {

            }

            /// <summary>
            /// Nested field doc.
            /// </summary>
            public readonly int NestedField;

            /// <summary>
            /// Nested property doc.
            /// </summary>
            public int NestedProperty => NestedField;

            /// <summary>
            /// Nested method doc.
            /// </summary>
            /// <param name="param">Nested method param.</param>
            /// <returns>Returns doc.</returns>
            public int NestedMethod(int param) => param;
        }

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
        /// Indexer doc.
        /// </summary>
        public int this[int index] => IntField;
        
        /// <summary>
        /// Constructor doc.
        /// </summary>
        /// <param name="ctorParam">Constructor param.</param>
        public TestClass(int ctorParam)
        {

        }

        /// <summary><inheritdoc /></summary>
        public static TestClass operator +(TestClass c) => c;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator -(TestClass c) => c;
        /// <summary><inheritdoc /></summary>
        public static bool operator !(TestClass c) => false;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator ~(TestClass c) => c;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator ++(TestClass c) => c;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator --(TestClass c) => c;
        /// <summary><inheritdoc /></summary>
        public static bool operator true(TestClass c) => true;
        /// <summary><inheritdoc /></summary>
        public static bool operator false(TestClass c) => false;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator +(TestClass a, TestClass b) => a;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator -(TestClass a, TestClass b) => a;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator *(TestClass a, TestClass b) => a;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator /(TestClass a, TestClass b) => a;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator %(TestClass a, TestClass b) => a;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator |(TestClass a, TestClass b) => a;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator ^(TestClass a, TestClass b) => a;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator <<(TestClass c, int p) => c;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator >>(TestClass c, int p) => c;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator ==(TestClass a, TestClass b) => a == b;
        /// <summary><inheritdoc /></summary>
        public static TestClass operator !=(TestClass a, TestClass b) => a != b;
        /// <summary><inheritdoc /></summary>
        public static bool operator <(TestClass a, TestClass b) => true;
        /// <summary><inheritdoc /></summary>
        public static bool operator >(TestClass a, TestClass b) => true;
        /// <summary><inheritdoc /></summary>
        public static bool operator <=(TestClass a, TestClass b) => true;
        /// <summary><inheritdoc /></summary>
        public static bool operator >=(TestClass a, TestClass b) => true;
        /// <summary><inheritdoc /></summary>
        public static explicit operator int(TestClass c) => 666;
        /// <summary><inheritdoc /></summary>
        public static implicit operator float(TestClass c) => 666;

        /// <summary>
        /// Overrided method doc.
        /// </summary>
        /// <param name="obj">Obj param.</param>
        /// <returns>Returns doc.</returns>
        public override bool Equals(object obj) => base.Equals(obj);

        /// <summary><inheritdoc /></summary>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Virtual method doc.
        /// </summary>
        /// <param name="intParam">Int param.</param>
        public virtual void  VirtualMethod(int intParam)
        {

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
        /// No param method doc.
        /// </summary>
        public void NoParamMethod()
        {
            
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
        /// Generic nested class doc.
        /// </summary>
        /// <typeparam name="Q">Typeparam Q.</typeparam>
        public class GenericNestedClass<Q>
        {
            /// <summary>
            /// Generic nested field doc.
            /// </summary>
            public readonly T NestedField;

            /// <summary>
            /// Generic nested property doc.
            /// </summary>
            public T NestedProperty => NestedField;

            /// <summary>
            /// Generic nested method doc.
            /// </summary>
            /// <param name="classParam">Class param.</param>
            /// <param name="nestedParam">Nested class param.</param>
            /// <param name="methodParam">Method param.</param>
            /// <typeparam name="V">Typeparam V.</typeparam>
            /// <returns>Returns doc.</returns>
            public V NestedMethod<V>(T classParam, Q nestedParam, V methodParam)
                => methodParam;
            
            /// <summary>
            /// The monster method doc.
            /// <para>If we can parse this, we can parse anything.</para>
            /// </summary>
            /// <param name="classParam">Class param.</param>
            /// <param name="nestedParam">Nested param.</param>
            /// <param name="methodParam">Method param.</param>
            /// <param name="constructedParam">Constructed generic ref array param.</param>
            /// <param name="genericArrayParam">Generic ref array param.</param>
            /// <param name="multiArrayParam">Multidimensional generic ref array param.</param>
            /// <typeparam name="V">Typeparam V.</typeparam>
            /// <returns>Returns doc.</returns>
            public V TheMonsterMethod<V>(T classParam, Q nestedParam,
                V methodParam, ref GenericClass<int>.GenericNestedClass<V>[][] constructedParam,
                ref V[][] genericArrayParam, ref Q[,,] multiArrayParam)
                => methodParam;
        }

        /// <summary>
        /// Generic field doc.
        /// </summary>
        public readonly T GenericField;

        /// <summary>
        /// Generic array field doc.
        /// </summary>
        public readonly T[] GenericArrayField;

        /// <summary>
        /// Constructed field doc.
        /// </summary>
        public readonly GenericClass<int> ConstructedField;

        /// <summary>
        /// Generic property doc.
        /// </summary>
        public T GenericProperty => GenericField;

        /// <summary>
        /// Generic array property doc.
        /// </summary>
        public T[] GenericArrayProperty => GenericArrayField;

        /// <summary>
        /// Constructed property doc.
        /// </summary>
        public GenericClass<int> ConstructedProperty => ConstructedField;

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

        /// <summary>
        /// Constructed method doc.
        /// </summary>
        /// <param name="classParam">Class param.</param>
        /// <param name="methodParam">Method param.</param>
        /// <param name="constructedParam">Constructed param.</param>
        /// <typeparam name="Q">Typeparam Q.</typeparam>
        /// <returns>Returns doc.</returns>
        public GenericClass<int> ConstructedMethod<Q>(T classParam, Q methodParam,
            GenericClass<int> constructedParam)
        {
            return constructedParam;
        }
    }

    /// <summary>
    /// Test struct doc.
    /// </summary>
    public readonly struct TestStruct
    {
        /// <summary>
        /// Test struct field doc.
        /// </summary>
        public readonly int Field;

        /// <summary>
        /// Test struct method doc.
        /// </summary>
        /// <param name="s">Test struct param.</param>
        /// <returns>Returns doc.</returns>
        public ref readonly int Method(in TestStruct s)
        {
            return ref s.Field;
        }
    }

    /// <summary>
    /// Unsafe struct doc.
    /// </summary>
    public unsafe struct UnsafeStruct
    {
        /// <summary>
        /// Pointer field doc.
        /// </summary>
        public readonly int* PointerField;

        /// <summary>
        /// Fixed array doc.
        /// </summary>
        public fixed int FixedArray[69];

        /// <summary>
        /// Pointer method doc.
        /// </summary>
        /// <param name="a">Pointer a.</param>
        /// <param name="b">Pointer b.</param>
        /// <returns>Returns doc.</returns>
        public int* PointerMethod(int* a, float* b) => a;
    }
}