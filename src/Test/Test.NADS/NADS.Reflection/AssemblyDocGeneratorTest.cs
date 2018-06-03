using System;
using System.Reflection;
using NUnit.Framework;
using NADS.TestDoc;

namespace NADS.Reflection
{
    internal class InternalClass { }
    class DefaultAccessClass { }

    [TestFixture]
    public class AssemblyDocGeneratorTest
    {
        internal class InternalNestedClass { }
        protected internal class ProtectedInternalClass { }
        protected class ProtectedClass { }
        private protected class PrivateProtectedClass { }
        private class PrivateClass { }
        class DefaultAccessNestedClass { }

        [Serializable]
        class NormalClass { }
        sealed class SealedClass { }
        abstract class AbstractClass { }
        static class StaticClass { }

        struct NormalStruct { }
        readonly struct ReadonlyStruct { }

        class GenericConstrainedClass<T, U, V, W>
        where T : struct
        where U : class, new()
        where V : W
        where W : NormalClass { }

        interface GenericInterface<in T, out V, W> { }

        AssemblyDocGenerator gen;

        [SetUp]
        public void SetUp()
        {
            gen = new AssemblyDocGenerator();
        }

        [Test]
        public void TestGetTypeAccess()
        {
            Assert.AreEqual(AccessModifier.Public, gen.GetTypeAccess(typeof(TestClass)));
            Assert.AreEqual(AccessModifier.Internal, gen.GetTypeAccess(typeof(InternalClass)));
            Assert.AreEqual(AccessModifier.ProtectedInternal, gen.GetTypeAccess(typeof(ProtectedInternalClass)));
            Assert.AreEqual(AccessModifier.Protected, gen.GetTypeAccess(typeof(ProtectedClass)));
            Assert.AreEqual(AccessModifier.PrivateProtected, gen.GetTypeAccess(typeof(PrivateProtectedClass)));
            Assert.AreEqual(AccessModifier.Private, gen.GetTypeAccess(typeof(PrivateClass)));
            Assert.AreEqual(AccessModifier.Internal, gen.GetTypeAccess(typeof(DefaultAccessClass)));

            Assert.AreEqual(AccessModifier.Public, gen.GetTypeAccess(typeof(TestClass.NestedClass)));
            Assert.AreEqual(AccessModifier.Internal, gen.GetTypeAccess(typeof(InternalNestedClass)));
            Assert.AreEqual(AccessModifier.Private, gen.GetTypeAccess(typeof(DefaultAccessNestedClass)));
        }

        [Test]
        public void TestGetTypeModifiers()
        {
            Assert.AreEqual(Modifier.None, gen.GetTypeModifiers(typeof(NormalClass)));
            Assert.AreEqual(Modifier.Sealed, gen.GetTypeModifiers(typeof(SealedClass)));
            Assert.AreEqual(Modifier.Abstract, gen.GetTypeModifiers(typeof(AbstractClass)));
            Assert.AreEqual(Modifier.Static, gen.GetTypeModifiers(typeof(StaticClass)));

            Assert.AreEqual(Modifier.None, gen.GetTypeModifiers(typeof(NormalStruct)));
            Assert.AreEqual(Modifier.Readonly, gen.GetTypeModifiers(typeof(ReadonlyStruct)));
        }

        [Test]
        public void TestGetAttributes()
        {
            MemberRef expected = new MemberRef(MemberRefType.Class, "System.SerializableAttribute");
            Assert.That(gen.GetAttributes(typeof(NormalClass)), Is.EquivalentTo(new MemberRef[] { expected } ));
        }

        [Test]
        public void TestGetTypeParams()
        {
            var expected = new TypeParam[]
            {
                new TypeParam("T", ParamModifier.None, new TypeConstraint[]
                    { TypeConstraint.Struct }
                ),
                new TypeParam("U", ParamModifier.None, new TypeConstraint[]
                    { TypeConstraint.Class, TypeConstraint.Ctor }
                ),
                new TypeParam("V", ParamModifier.None, new TypeConstraint[]
                    { TypeConstraint.TypeParam(3) }
                ),
                new TypeParam("W", ParamModifier.None, new TypeConstraint[]
                    { TypeConstraint.Type(new MemberRef(MemberRefType.Class, "NADS.Reflection.AssemblyDocGeneratorTest+NormalClass")) }
                )
            };

            var actual = gen.GetTypeParams(typeof(GenericConstrainedClass<,,,>));

            Assert.AreEqual(expected.Length, actual.Count);
            for(int i = 0; i < expected.Length; i++)
            { AssertTypeParam(expected[i], actual[i]); }
        }

        [Test]
        public void TestGetMemberRefType()
        {
            Assert.AreEqual(MemberRefType.Class, gen.GetMemberRefType(typeof(TestClass)));
            Assert.AreEqual(MemberRefType.Interface, gen.GetMemberRefType(typeof(GenericInterface<,,>)));
            Assert.AreEqual(MemberRefType.Enum, gen.GetMemberRefType(typeof(TestEnum)));
            Assert.AreEqual(MemberRefType.Struct, gen.GetMemberRefType(typeof(TestStruct)));
            Assert.AreEqual(MemberRefType.Delegate, gen.GetMemberRefType(typeof(TestDoc.TestDelegate)));
        }

        [Test]
        public void TestGetGenericParamModifier()
        {
            Type i = typeof(GenericInterface<,,>);
            var typeParams = i.GetGenericArguments();

            Type inParam = typeParams[0];
            Type outParam = typeParams[1];
            Type normalParam = typeParams[2];

            Assert.AreEqual(ParamModifier.In, gen.GetGenericParamModifier(inParam.GenericParameterAttributes));
            Assert.AreEqual(ParamModifier.Out, gen.GetGenericParamModifier(outParam.GenericParameterAttributes));
            Assert.AreEqual(ParamModifier.None, gen.GetGenericParamModifier(normalParam.GenericParameterAttributes));
        }

        void AssertMemberRef(in MemberRef a, in MemberRef b)
        {
            Assert.AreEqual(a.Type, b.Type);
            Assert.AreEqual(a.Name, b.Name);
        }

        void AssertTypeParam(in TypeParam a, in TypeParam b)
        {
            Assert.AreEqual(a.Name, b.Name);
            Assert.AreEqual(a.Modifier, b.Modifier);
            Assert.AreEqual(a.Constraints.Count, b.Constraints.Count);

            for(int c = 0; c < a.Constraints.Count; c++)
            { AssertTypeConstraint(a.Constraints[c], b.Constraints[c]); }
        }

        void AssertTypeConstraint(in TypeConstraint a, in TypeConstraint b)
        {
            Assert.AreEqual(a.Constraint, b.Constraint);
            AssertMemberRef(a.ConstrainedType, b.ConstrainedType);
            Assert.AreEqual(a.ConstrainedTypeParamPosition, b.ConstrainedTypeParamPosition);
        }
    }
}