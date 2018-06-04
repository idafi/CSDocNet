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
    }
}