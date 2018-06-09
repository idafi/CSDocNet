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
    public class MethodBaseUtilityTest
    {
        MethodBaseUtility methodUtility;
        IDocGeneratorUtility docUtility;
        ICommentIDGenerator idGen;

        [SetUp]
        public void SetUp()
        {
            docUtility = Substitute.For<IDocGeneratorUtility>();
            idGen = Substitute.For<ICommentIDGenerator>();
            methodUtility = new MethodBaseUtility(docUtility, idGen);
        }

        [Test]
        public void TestGenerateMemberDoc()
        {
            MethodBase method = typeof(MethodTestClass).GetMethod("Method");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[] { new MemberRef(MemberRefType.Class, typeof(STAThreadAttribute).MetadataToken) };
            docUtility.GenerateName(method).Returns("NADS.TestDoc.MethodTestClass.Method");
            docUtility.GenerateAttributes(method).Returns(expectedAttr);
            idGen.GenerateMemberID(method).Returns("M:NADS.TestDoc.MethodTestClass.Method");

            var name = methodUtility.GenerateName(method);
            var id = methodUtility.GenerateCommentID(method);
            var access = methodUtility.GenerateAccess(method);
            var modifiers = methodUtility.GenerateModifiers(method);
            var attr = methodUtility.GenerateAttributes(method);

            MemberDoc mDoc = methodUtility.GenerateMemberDoc(method);
            Assert.AreEqual(name, mDoc.Name);
            Assert.AreEqual(id, mDoc.CommentID);
            Assert.AreEqual(access, mDoc.Access);
            Assert.AreEqual(modifiers, mDoc.Modifiers);
            Assert.That(mDoc.Attributes, Is.EquivalentTo(attr));
        }

        [Test]
        public void TestGenerateMemberDocThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => methodUtility.GenerateMemberDoc(null));
        }

        [Test]
        public void TestGenerateName()
        {
            MethodBase method = typeof(MethodTestClass).GetMethod("Method");
            methodUtility.GenerateName(method);

            docUtility.Received().GenerateName(method);
        }

        [Test]
        public void TestGenerateNameThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => methodUtility.GenerateName(null));
        }

        [Test]
        public void TestGenerateCommentID()
        {
            MethodBase method = typeof(MethodTestClass).GetMethod("Method");
            methodUtility.GenerateCommentID(method);

            idGen.Received().GenerateMemberID(method);
        }

        [Test]
        public void TestGenerateCommentIDThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => methodUtility.GenerateCommentID(null));
        }

        [Test]
        public void TestGenerateAccess()
        {
            Type t = typeof(MethodTestClass);
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            MethodBase publicMethod = t.GetMethod("PublicMethod", flags);
            MethodBase protectedInternalMethod = t.GetMethod("ProtectedInternalMethod", flags);
            MethodBase internalMethod = t.GetMethod("InternalMethod", flags);
            MethodBase protectedMethod = t.GetMethod("ProtectedMethod", flags);
            MethodBase privateProtectedMethod = t.GetMethod("PrivateProtectedMethod", flags);
            MethodBase privateMethod = t.GetMethod("PrivateMethod", flags);
            MethodBase defaultMethod = t.GetMethod("DefaultMethod", flags);

            Assert.AreEqual(AccessModifier.Public, methodUtility.GenerateAccess(publicMethod));
            Assert.AreEqual(AccessModifier.ProtectedInternal, methodUtility.GenerateAccess(protectedInternalMethod));
            Assert.AreEqual(AccessModifier.Internal, methodUtility.GenerateAccess(internalMethod));
            Assert.AreEqual(AccessModifier.Protected, methodUtility.GenerateAccess(protectedMethod));
            Assert.AreEqual(AccessModifier.PrivateProtected, methodUtility.GenerateAccess(privateProtectedMethod));
            Assert.AreEqual(AccessModifier.Private, methodUtility.GenerateAccess(privateMethod));
            Assert.AreEqual(AccessModifier.Private, methodUtility.GenerateAccess(defaultMethod));
        }

        [Test]
        public void TestGenerateAccessThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => methodUtility.GenerateAccess(null));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            Type t = typeof(MethodTestClass);
            Type i = typeof(MethodTestClass.Impl);

            MethodBase abstractMethod = t.GetMethod("AbstractMethod");
            MethodBase asyncMethod = t.GetMethod("AsyncMethod");
            MethodBase externMethod = t.GetMethod("ExternMethod");
            MethodBase staticMethod = t.GetMethod("StaticMethod");
            MethodBase virtualMethod = t.GetMethod("VirtualMethod");
            MethodBase staticAsyncMethod = t.GetMethod("StaticAsyncMethod");
            MethodBase overrideAbstractMethod = i.GetMethod("AbstractMethod");
            MethodBase overrideVirtualMethod = i.GetMethod("VirtualMethod");

            Assert.AreEqual(Modifier.Abstract, methodUtility.GenerateModifiers(abstractMethod));
            Assert.AreEqual(Modifier.Async, methodUtility.GenerateModifiers(asyncMethod));
            Assert.AreEqual(Modifier.Static | Modifier.Extern, methodUtility.GenerateModifiers(externMethod));
            Assert.AreEqual(Modifier.Static, methodUtility.GenerateModifiers(staticMethod));
            Assert.AreEqual(Modifier.Virtual, methodUtility.GenerateModifiers(virtualMethod));
            Assert.AreEqual(Modifier.Static | Modifier.Async, methodUtility.GenerateModifiers(staticAsyncMethod));
            Assert.AreEqual(Modifier.Sealed | Modifier.Override, methodUtility.GenerateModifiers(overrideAbstractMethod));
            Assert.AreEqual(Modifier.Override, methodUtility.GenerateModifiers(overrideVirtualMethod));
        }

        [Test]
        public void TestGenerateModifiersThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => methodUtility.GenerateModifiers(null));
        }

        [Test]
        public void TestGenerateAttributes()
        {
            MethodBase method = typeof(MethodTestClass).GetMethod("Method");
            methodUtility.GenerateAttributes(method);

            docUtility.Received().GenerateAttributes(method);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => methodUtility.GenerateAttributes(null));
        }

        [Test]
        public void TestGenerateParams()
        {
            MethodBase method = typeof(MethodTestClass).GetMethod("Method");
            MemberRef intRef = new MemberRef(MemberRefType.Struct, typeof(int).MetadataToken);
            docUtility.MakeMemberRef(null).ReturnsForAnyArgs(intRef);

            IReadOnlyList<Param> expected = new Param[]
            {
                new Param(ParamModifier.In, intRef, false, -1, false, null),
                new Param(ParamModifier.Out, intRef, false, -1, false, null),
                new Param(ParamModifier.Ref, intRef, false, -1, false, null),
                new Param(ParamModifier.None, intRef, false, -1, true, 666)
            };

            var actual = methodUtility.GenerateParams(method);
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestGenerateParamsThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => methodUtility.GenerateParams(null));
        }

        [Test]
        public void TestGenerateTypeParams()
        {
            MethodBase method = typeof(MethodTestClass).GetMethod("GenericMethod");
            var typeParamTypes = method.GetGenericArguments();
            var typeParams = methodUtility.GenerateTypeParams(typeof(MethodTestClass).GetMethod("GenericMethod"));
            
            Assert.AreEqual(typeParamTypes.Length, typeParams.Count);
            for(int i = 0; i < typeParams.Count; i++)
            {
                docUtility.Received().GetGenericParamModifier(typeParamTypes[i].GenericParameterAttributes);
                docUtility.Received().GetTypeParamConstraints(typeParamTypes[i]);
            }

            Assert.AreEqual("T", typeParamTypes[0].Name);
            Assert.AreEqual("U", typeParamTypes[1].Name);
            Assert.AreEqual("V", typeParamTypes[2].Name);
        }

        [Test]
        public void TestGenerateTypeParamsThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => methodUtility.GenerateTypeParams(null));
        }
    }
}