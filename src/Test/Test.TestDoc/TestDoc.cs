using System;
using System.Threading.Tasks;

namespace NADS.TestDoc
{
    /// <summary>
    /// Test class doc.
    /// </summary>
    [Serializable]
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
        /// Action event doc.
        /// </summary>
        public event Action ActionEvent;

        /// <summary>
        /// Int action event doc.
        /// </summary>
        public event Action<int> IntActionEvent;

        /// <summary>
        /// Int field doc.
        /// </summary>
        public readonly int IntField;

        /// <summary>
        /// Int array field doc.
        /// </summary>
        public readonly int[] IntArrayField;

        /// <summary>
        /// Multi array field doc.
        /// </summary>
        public readonly int[][,,][,] MultiArrayField;

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
            // shut up the compiler
            ActionEvent?.Invoke();
            IntActionEvent?.Invoke(default);
        }

        /// <inheritdoc />
        public static TestClass operator +(TestClass c) => c;
        /// <inheritdoc />
        public static TestClass operator -(TestClass c) => c;
        /// <inheritdoc />
        public static bool operator !(TestClass c) => false;
        /// <inheritdoc />
        public static TestClass operator ~(TestClass c) => c;
        /// <inheritdoc />
        public static TestClass operator ++(TestClass c) => c;
        /// <inheritdoc />
        public static TestClass operator --(TestClass c) => c;
        /// <inheritdoc />
        public static bool operator true(TestClass c) => true;
        /// <inheritdoc />
        public static bool operator false(TestClass c) => false;
        /// <inheritdoc />
        public static TestClass operator +(TestClass a, TestClass b) => a;
        /// <inheritdoc />
        public static TestClass operator -(TestClass a, TestClass b) => a;
        /// <inheritdoc />
        public static TestClass operator *(TestClass a, TestClass b) => a;
        /// <inheritdoc />
        public static TestClass operator /(TestClass a, TestClass b) => a;
        /// <inheritdoc />
        public static TestClass operator %(TestClass a, TestClass b) => a;
        /// <inheritdoc />
        public static TestClass operator |(TestClass a, TestClass b) => a;
        /// <inheritdoc />
        public static TestClass operator ^(TestClass a, TestClass b) => a;
        /// <inheritdoc />
        public static TestClass operator <<(TestClass c, int p) => c;
        /// <inheritdoc />
        public static TestClass operator >>(TestClass c, int p) => c;
        /// <inheritdoc />
        public static TestClass operator ==(TestClass a, TestClass b) => a == b;
        /// <inheritdoc />
        public static TestClass operator !=(TestClass a, TestClass b) => a != b;
        /// <inheritdoc />
        public static bool operator <(TestClass a, TestClass b) => true;
        /// <inheritdoc />
        public static bool operator >(TestClass a, TestClass b) => true;
        /// <inheritdoc />
        public static bool operator <=(TestClass a, TestClass b) => true;
        /// <inheritdoc />
        public static bool operator >=(TestClass a, TestClass b) => true;
        /// <inheritdoc />
        public static explicit operator int(TestClass c) => 666;
        /// <inheritdoc />
        public static implicit operator float(TestClass c) => 666;

        /// <summary>
        /// Overrided method doc.
        /// </summary>
        /// <param name="obj">Obj param.</param>
        /// <returns>Returns doc.</returns>
        public override bool Equals(object obj) => base.Equals(obj);

        /// <inheritdoc />
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
        /// Generic event doc.
        /// </summary>
        public event Action<T> GenericEvent;

        /// <summary>
        /// Constructed generic event doc.
        /// </summary>
        public event Action<GenericClass<int>> ConstructedEvent;

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
        /// Generic indexed doc.
        /// </summary>
        public T this[GenericClass<T> classParam] => default(T);

        /// <summary>
        /// Constructed indexer doc.
        /// </summary>
        public int this[GenericClass<int> classParam] => 666;

        /// <inheritdoc />
        public static explicit operator int(GenericClass<T> classParam) => 666;

        /// <inheritdoc />
        public static implicit operator GenericClass<T>(int intParam) => default;

        /// <summary>
        /// Generic constructor doc.
        /// </summary>
        /// <param name="classParam">Class param.</param>
        /// <param name="constructedParam">Constructed class param.</param>
        public GenericClass(T classParam, GenericClass<int> constructedParam)
        {
            // shut up the compiler
            GenericEvent?.Invoke(default);
            ConstructedEvent?.Invoke(default);
        }      

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
    /// Generic constrainted class.
    /// </summary>
    /// <typeparam name="T">Struct-constrained typeparam T.</typeparam>
    /// <typeparam name="U">Class and new()-constrained typeparam U.</typeparam>
    /// <typeparam name="V">Typeparam W-constrained typeparam V.</typeparam>
    /// <typeparam name="W">TestClass-constrained typeparam W.</typeparam>
    public class GenericConstrainedClass<T, U, V, W>
        where T : struct
        where U : class, new()
        where V : W
        where W : TestClass { }

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
    /// Generic struct doc.
    /// </summary>
    public readonly struct GenericStruct<T>
    {
        /// <summary>
        /// Field doc.
        /// </summary>
        public readonly T Field;

        /// <summary>
        /// Generic method doc.
        /// </summary>
        /// <param name="tParam">Generic param.</param>
        /// <param name="constructedParam">Constructed generic param.</param>
        /// <returns>Returns doc.</returns>
        public T Method(T tParam, in GenericStruct<int> constructedParam)
        {
            return tParam;
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

    /// <summary>
    /// Test enum doc.
    /// </summary>
    public enum TestEnum
    {
        /// <summary>
        /// Value A doc.
        /// </summary>
        ValueA,

        /// <summary>
        /// Value B doc.
        /// </summary>
        ValueB
    }

    /// <summary>
    /// Test interface doc.
    /// </summary>
    public interface TestInterface
    {
        /// <summary>
        /// Action event doc.
        /// </summary>
        event Action ActionEvent;

        /// <summary>
        /// Get only property doc.
        /// </summary>
        int GetOnlyProperty { get; }

        /// <summary>
        /// Set only property doc.
        /// </summary>
        int SetOnlyProperty { set; }

        /// <summary>
        /// Get/set property doc.
        /// </summary>
        int GetSetProperty { get; set; }

        /// <summary>
        /// Method doc.
        /// </summary>
        /// <param name="intParam">Int param.</param>
        /// <returns>Returns doc.</returns>
        int Method(int intParam);
    }

    /// <summary>
    /// Generic interface doc.
    /// </summary>
    /// <typeparam name="T">Contravariant typeparam T.</typeparam>
    /// <typeparam name="V">Covariant typeparam V.</typeparam>
    /// <typeparam name="W">Typeparam W.</typeparam>
    public interface GenericInterface<in T, out V, W>
    {
        /// <summary>
        /// Generic event doc.
        /// </summary>
        event Action<W> GenericEvent;

        /// <summary>
        /// Covariant property doc.
        /// </summary>
        V CovariantProperty { get; }

        /// <summary>
        /// Contravariant method doc.
        /// </summary>
        /// <param name="contraParam">Contravariant param.</param>
        /// <returns>Returns doc.</returns>
        V ContravariantMethod(T contraParam);
    }

    /// <summary>
    /// Test delegate doc.
    /// </summary>
    /// <param name="i">Int param.</param>
    /// <param name="f">Float param.</param>
    /// <returns>Returns doc.</returns>
    public delegate int TestDelegate(int i, float f);

    /// <summary>
    /// Generic delegate doc.
    /// </summary>
    /// <param name="param">Generic param.</param>
    /// <param name="genericClassParam">Generic class param.</param>
    /// <param name="constructedGenericParam">Constructed generic class param.</param>
    /// <typeparam name="T">Typeparam T.</typeparam>
    /// <returns>Returns doc.</returns>
    public delegate T GenericDelegate<T>(T param, GenericClass<T> genericClassParam,
        GenericClass<int> constructedGenericParam);

    /// <summary>
    /// Field test class doc.
    /// </summary>
    public class FieldTestClass
    {
        /// <summary>Public field doc.</summary>
        [NonSerialized]
        public int PublicField;
        /// <summary>Protected internal field doc.</summary>
        protected internal int ProtectedInternalField;
        internal int InternalField;
        /// <summary>Protected field doc.</summary>
        protected int protectedField;
        private protected int privateProtectedField;
        private int privateField;
        int defaultField;

        /// <summary>Const field doc.</summary>
        public const int ConstField = 666;
        /// <summary>Readonly field doc.</summary>
        public readonly int ReadonlyField;
        /// <summary>Static field doc.</summary>
        public static int StaticField;
        /// <summary>Volatile field doc.</summary>
        public volatile int VolatileField;

        /// <summary>Static readonly field doc.</summary>
        [NonSerialized]
        public static readonly int StaticReadonlyField;
        /// <summary>Static volatile field doc.</summary>
        public static volatile int StaticVolatileField;

        /// <summary>
        /// Shut up, compiler !!
        /// </summary>
        public void ShutUpCompiler()
        {
            InternalField++;
            privateField++;
            defaultField++;
        }
    }

    /// <summary>
    /// Property test class doc.
    /// </summary>
    public abstract class PropertyTestClass
    {
        /// <summary>
        /// Property test class implementation.
        /// </summary>
        public class Impl : PropertyTestClass
        {
            /// <inheritdoc />
            public override int VirtualProperty { get; set; }
            /// <inheritdoc />
            public override int AbstractProperty { get; set; }
        }

        /// <summary>Public property.</summary>
        public int PublicProperty { get; set; }
        /// <summary>Protected internal property.</summary>
        protected internal int ProtectedInternalProperty { get; set; }
        internal int InternalProperty { get; set; }
        /// <summary>Protected property.</summary>
        protected int protectedProperty { get; set; }
        private protected int privateProtectedProperty { get; set; }
        private int privateProprety { get; set; }
        int defaultProperty { get; set; }

        /// <summary>Static property.</summary>
        public static int StaticProperty { get; set; }
        /// <summary>Virtual property.</summary>
        public virtual int VirtualProperty { get; set; }
        /// <summary>Abstract property.</summary>
        public abstract int AbstractProperty { get; set; }

        /// <summary>Get-only property.</summary>
        public int GetOnlyProperty { get; }
        /// <summary>Set-only property.</summary>
        public int SetOnlyProperty { set { } }
    }

    /// <summary>
    /// Method test class doc.
    /// </summary>
    public abstract class MethodTestClass
    {
        /// <summary>
        /// Method test class implementation.
        /// </summary>
        public class Impl : MethodTestClass
        {
            /// <inheritdoc />
            public sealed override int AbstractMethod(int param) => param;
            /// <inheritdoc />
            public override int VirtualMethod(int param) => param;
        }

        /// <summary>Public method doc.</summary>
        public void PublicMethod() { }
        /// <summary>Protected internal method doc.</summary>
        protected internal void ProtectedInternalMethod() { }
        internal void InternalMethod() { }
        /// <summary>Protected method doc.</summary>
        protected void ProtectedMethod() { }
        private protected void PrivateProtectedMethod() { }
        private void PrivateMethod() { }
        void DefaultMethod() { }

        /// <summary>Normal method doc.</summary>
        [STAThread]
        public int Method(in int inParam, out int outParam, ref int refParam, int param = 666)
        {
            outParam = inParam;
            return param;
        }

        /// <summary>No-param method doc.</summary>
        public void NoParamMethod() { }

        /// <summary>
        /// Generic param method doc.
        /// </summary>
        /// <param name="param">Non-generic param.</param>
        /// <param name="genericParam">Generic param.</param>
        /// <typeparam name="T">Typeparam T.</typeparam>
        public void GenericParamMethod<T>(int param, T genericParam) { }

        /// <summary>Abstract method doc.</summary>
        public abstract int AbstractMethod(int param);

        /// <summary>Async method doc.</summary>
        public async Task<int> AsyncMethod()
        {
            await Task.Delay(666);
            return 666;
        }

        /// <summary>Extern method doc.</summary>
        [System.Runtime.InteropServices.DllImport("don't call me")]
        public static extern int ExternMethod(int param);

        /// <summary>Static method doc.</summary>
        public static int StaticMethod(int param) => param;

        /// <summary>Virtual method doc.</summary>
        public virtual int VirtualMethod(int param) => param;

        /// <summary>Static async method doc.</summary>
        public static async Task<int> StaticAsyncMethod()
        {
            await Task.Delay(666);
            return 666;
        }
    }
}