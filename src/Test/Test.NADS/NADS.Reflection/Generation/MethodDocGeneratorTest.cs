using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using NSubstitute;
using NADS.Reflection.Data;
using NADS.TestDoc;

namespace NADS.Reflection.Generation
{
    [TestFixture]
    public class MethodDocGeneratorTest
    {
        MethodDocGenerator gen;
        IDocGeneratorUtility utility;
        ICommentIDGenerator idGen;

        [SetUp]
        public void SetUp()
        {
            utility = Substitute.For<IDocGeneratorUtility>();
            idGen = Substitute.For<ICommentIDGenerator>();
            gen = new MethodDocGenerator(utility, idGen);
        }

        [Test]
        public void TestGenerateMethodDoc()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[] { new MemberRef(MemberRefType.Class, "System.STAThreadAttribute") };
            utility.GenerateName(method).Returns("NADS.TestDoc.MethodTestClass.Method");
            utility.GenerateAttributes(method).Returns(expectedAttr);
            idGen.GenerateMethodID(method).Returns("M:NADS.TestDoc.MethodTestClass.Method");

            MemberDoc member = gen.GenerateMemberDoc(method);
            MemberRef returnType = gen.GenerateReturnType(method);
            var parameters = gen.GenerateParams(method);

            MethodDoc mDoc = gen.GenerateMethodDoc(method);
            Assert.AreEqual(member, mDoc.Member);
            Assert.AreEqual(returnType, mDoc.ReturnType);
            Assert.That(mDoc.Params, Is.EquivalentTo(parameters));
        }

        [Test]
        public void TestGenerateMethodDocThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateMethodDoc(null));
        }

        [Test]
        public void TestGenerateMemberDoc()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[] { new MemberRef(MemberRefType.Class, "System.STAThreadAttribute") };
            utility.GenerateName(method).Returns("NADS.TestDoc.MethodTestClass.Method");
            utility.GenerateAttributes(method).Returns(expectedAttr);
            idGen.GenerateMethodID(method).Returns("M:NADS.TestDoc.MethodTestClass.Method");

            var name = gen.GenerateName(method);
            var id = gen.GenerateCommentID(method);
            var access = gen.GenerateAccess(method);
            var modifiers = gen.GenerateModifiers(method);
            var attr = gen.GenerateAttributes(method);

            MemberDoc mDoc = gen.GenerateMemberDoc(method);
            Assert.AreEqual(name, mDoc.Name);
            Assert.AreEqual(id, mDoc.CommentID);
            Assert.AreEqual(access, mDoc.Access);
            Assert.AreEqual(modifiers, mDoc.Modifiers);
            Assert.That(mDoc.Attributes, Is.EquivalentTo(attr));
        }

        [Test]
        public void TestGenerateMemberDocThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateMemberDoc(null));
        }

        [Test]
        public void TestGenerateReturnType()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            gen.GenerateReturnType(method);

            utility.Received().MakeMemberRef(method.ReturnType);
        }

        [Test]
        public void TestGenerateReturnTypeThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateReturnType(null));
        }

        [Test]
        public void TestGenerateParams()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            MemberRef intRef = new MemberRef(MemberRefType.Struct, "System.Int32");
            utility.MakeMemberRef(null).ReturnsForAnyArgs(intRef);

            IReadOnlyList<Param> expected = new Param[]
            {
                new Param(ParamModifier.In, intRef, false, -1, false, null),
                new Param(ParamModifier.Out, intRef, false, -1, false, null),
                new Param(ParamModifier.Ref, intRef, false, -1, false, null),
                new Param(ParamModifier.None, intRef, false, -1, true, 666)
            };

            var actual = gen.GenerateParams(method);
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestGenerateParamsThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateParams(null));
        }

        [Test]
        public void TestGenerateName()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            gen.GenerateName(method);

            utility.Received().GenerateName(method);
        }

        [Test]
        public void TestGenerateNameThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateName(null));
        }

        [Test]
        public void TestGenerateCommentID()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            gen.GenerateCommentID(method);

            idGen.Received().GenerateMethodID(method);
        }        

        [Test]
        public void TestGenerateAccess()
        {
            Type t = typeof(MethodTestClass);
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            MethodInfo publicMethod = t.GetMethod("PublicMethod", flags);
            MethodInfo protectedInternalMethod = t.GetMethod("ProtectedInternalMethod", flags);
            MethodInfo internalMethod = t.GetMethod("InternalMethod", flags);
            MethodInfo protectedMethod = t.GetMethod("ProtectedMethod", flags);
            MethodInfo privateProtectedMethod = t.GetMethod("PrivateProtectedMethod", flags);
            MethodInfo privateMethod = t.GetMethod("PrivateMethod", flags);
            MethodInfo defaultMethod = t.GetMethod("DefaultMethod", flags);

            Assert.AreEqual(AccessModifier.Public, gen.GenerateAccess(publicMethod));
            Assert.AreEqual(AccessModifier.ProtectedInternal, gen.GenerateAccess(protectedInternalMethod));
            Assert.AreEqual(AccessModifier.Internal, gen.GenerateAccess(internalMethod));
            Assert.AreEqual(AccessModifier.Protected, gen.GenerateAccess(protectedMethod));
            Assert.AreEqual(AccessModifier.PrivateProtected, gen.GenerateAccess(privateProtectedMethod));
            Assert.AreEqual(AccessModifier.Private, gen.GenerateAccess(privateMethod));
            Assert.AreEqual(AccessModifier.Private, gen.GenerateAccess(defaultMethod));
        }

        [Test]
        public void TestGenerateAccessThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAccess(null));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            Type t = typeof(MethodTestClass);
            Type i = typeof(MethodTestClass.Impl);

            MethodInfo abstractMethod = t.GetMethod("AbstractMethod");
            MethodInfo asyncMethod = t.GetMethod("AsyncMethod");
            MethodInfo externMethod = t.GetMethod("ExternMethod");
            MethodInfo staticMethod = t.GetMethod("StaticMethod");
            MethodInfo virtualMethod = t.GetMethod("VirtualMethod");
            MethodInfo staticAsyncMethod = t.GetMethod("StaticAsyncMethod");
            MethodInfo overrideAbstractMethod = i.GetMethod("AbstractMethod");
            MethodInfo overrideVirtualMethod = i.GetMethod("VirtualMethod");

            Assert.AreEqual(Modifier.Abstract, gen.GenerateModifiers(abstractMethod));
            Assert.AreEqual(Modifier.Async, gen.GenerateModifiers(asyncMethod));
            Assert.AreEqual(Modifier.Static | Modifier.Extern, gen.GenerateModifiers(externMethod));
            Assert.AreEqual(Modifier.Static, gen.GenerateModifiers(staticMethod));
            Assert.AreEqual(Modifier.Virtual, gen.GenerateModifiers(virtualMethod));
            Assert.AreEqual(Modifier.Static | Modifier.Async, gen.GenerateModifiers(staticAsyncMethod));
            Assert.AreEqual(Modifier.Sealed | Modifier.Override, gen.GenerateModifiers(overrideAbstractMethod));
            Assert.AreEqual(Modifier.Override, gen.GenerateModifiers(overrideVirtualMethod));
        }

        [Test]
        public void TestGenerateModifiersThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateModifiers(null));
        }

        [Test]
        public void TestGenerateAttributes()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            gen.GenerateAttributes(method);

            utility.Received().GenerateAttributes(method);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
        }
    }
}