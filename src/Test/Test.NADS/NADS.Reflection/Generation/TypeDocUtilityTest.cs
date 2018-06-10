using System;
using System.Reflection;
using NUnit.Framework;
using NSubstitute;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class PublicClass { }
    internal class InternalClass { }
    class DefaultClass { }

    [TestFixture]
    public class TypeDocUtilityTest
    {
        public class PublicNestedClass { }
        protected internal class ProtectedInternalClass { }
        internal class InternalNestedClass { }
        protected class ProtectedClass { }
        private protected class PrivateProtectedClass { }
        private class PrivateClass { }
        class DefaultNestedClass { }

        abstract class AbstractClass { }
        sealed class SealedClass { }
        static class StaticClass { }
        readonly struct ReadonlyStruct { }
        ref struct RefStruct { }
        readonly ref struct ReadonlyRefStruct { }

        TypeDocUtility typeUtility;
        IDocGeneratorUtility docUtility;

        [SetUp]
        public void SetUp()
        {
            docUtility = Substitute.For<IDocGeneratorUtility>();
            typeUtility = new TypeDocUtility(docUtility);
        }

        [Test]
        public void TestGenerateAccess()
        {
            Assert.AreEqual(AccessModifier.Public, typeUtility.GenerateAccess(typeof(PublicClass)));
            Assert.AreEqual(AccessModifier.Internal, typeUtility.GenerateAccess(typeof(InternalClass)));
            Assert.AreEqual(AccessModifier.Internal, typeUtility.GenerateAccess(typeof(DefaultClass)));

            Assert.AreEqual(AccessModifier.Public, typeUtility.GenerateAccess(typeof(PublicNestedClass)));
            Assert.AreEqual(AccessModifier.ProtectedInternal, typeUtility.GenerateAccess(typeof(ProtectedInternalClass)));
            Assert.AreEqual(AccessModifier.Internal, typeUtility.GenerateAccess(typeof(InternalNestedClass)));
            Assert.AreEqual(AccessModifier.Protected, typeUtility.GenerateAccess(typeof(ProtectedClass)));
            Assert.AreEqual(AccessModifier.PrivateProtected, typeUtility.GenerateAccess(typeof(PrivateProtectedClass)));
            Assert.AreEqual(AccessModifier.Private, typeUtility.GenerateAccess(typeof(PrivateClass)));
            Assert.AreEqual(AccessModifier.Private, typeUtility.GenerateAccess(typeof(DefaultNestedClass)));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            docUtility.IsReadOnly(typeof(ReadonlyStruct)).Returns(true);
            docUtility.IsReadOnly(typeof(ReadonlyRefStruct)).Returns(true);

            Assert.AreEqual(Modifier.Abstract, typeUtility.GenerateModifiers(typeof(AbstractClass)));
            Assert.AreEqual(Modifier.Sealed, typeUtility.GenerateModifiers(typeof(SealedClass)));
            Assert.AreEqual(Modifier.Static, typeUtility.GenerateModifiers(typeof(StaticClass)));
            Assert.AreEqual(Modifier.Readonly, typeUtility.GenerateModifiers(typeof(ReadonlyStruct)));
            Assert.AreEqual(Modifier.Ref, typeUtility.GenerateModifiers(typeof(RefStruct)));
            Assert.AreEqual(Modifier.Readonly | Modifier.Ref, typeUtility.GenerateModifiers(typeof(ReadonlyRefStruct)));
        }
    }
}