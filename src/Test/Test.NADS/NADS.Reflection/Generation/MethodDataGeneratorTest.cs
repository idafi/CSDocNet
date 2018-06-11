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
    public class MethodDataGeneratorTest
    {
        MethodDataGenerator gen;
        IDataGeneratorUtility docUtility;
        IMethodBaseUtility methodUtility;

        [SetUp]
        public void SetUp()
        {
            docUtility = Substitute.For<IDataGeneratorUtility>();
            methodUtility = Substitute.For<IMethodBaseUtility>();
            gen = new MethodDataGenerator(docUtility, methodUtility);
        }

        [Test]
        public void TestGenerateMethodData()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[] { new MemberRef(MemberRefType.Class, typeof(STAThreadAttribute).MetadataToken) };
            docUtility.GenerateName(method).Returns("NADS.TestDoc.MethodTestClass.Method");
            docUtility.GenerateAttributes(method).Returns(expectedAttr);

            MemberData member = gen.GenerateMemberData(method);
            ReturnValue returnType = gen.GenerateReturnValue(method);
            var parameters = methodUtility.GenerateParams(method);
            var typeParams = methodUtility.GenerateTypeParams(method);

            MethodData mData = gen.GenerateMethodData(method);
            Assert.AreEqual(member, mData.Member);
            Assert.AreEqual(returnType, mData.ReturnValue);
            Assert.That(mData.Params, Is.EquivalentTo(parameters));
            Assert.That(mData.TypeParams, Is.EquivalentTo(typeParams));
        }

        [Test]
        public void TestGenerateMethodDataThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateMethodData(null));
        }

        [Test]
        public void TestGenerateMemberData()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            gen.GenerateMemberData(method);
            methodUtility.Received().GenerateMemberData(method);
        }

        [Test]
        public void TestGenerateMemberDataThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateMemberData(null));
        }

        [Test]
        public void TestGenerateReturnValue()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            ReturnValue val = gen.GenerateReturnValue(method);

            Assert.AreEqual(ReturnModifier.None, val.Modifier);
            docUtility.Received().MakeMemberRef(method.ReturnType);
            Assert.AreEqual(false, val.IsGenericType);
            Assert.AreEqual(-1, val.GenericTypePosition);
        }

        [Test]
        public void TestGenerateGenericReturnValue()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("GenericMethod");
            ReturnValue val = gen.GenerateReturnValue(method);

            Assert.AreEqual(ReturnModifier.None, val.Modifier);
            docUtility.DidNotReceiveWithAnyArgs().MakeMemberRef(null);
            Assert.AreEqual(true, val.IsGenericType);
            Assert.AreEqual(0, val.GenericTypePosition);
        }

        [Test]
        public void TestGenerateByRefReturnValue()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("RefReturnMethod");
            ReturnValue val = gen.GenerateReturnValue(method);

            Assert.AreEqual(ReturnModifier.Ref, val.Modifier);
            docUtility.Received().MakeMemberRef(method.ReturnType);
            Assert.AreEqual(false, val.IsGenericType);
            Assert.AreEqual(-1, val.GenericTypePosition);
        }

        [Test]
        public void TestGenerateByRefReadonlyReturnValue()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("RefReadonlyReturnMethod");
            docUtility.IsReadOnly(method.ReturnParameter).Returns(true);

            ReturnValue val = gen.GenerateReturnValue(method);
            
            Assert.AreEqual(ReturnModifier.RefReadonly, val.Modifier);
            docUtility.Received().MakeMemberRef(method.ReturnType);
            Assert.AreEqual(false, val.IsGenericType);
            Assert.AreEqual(-1, val.GenericTypePosition);
        }

        [Test]
        public void TestGenerateReturnValueThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateReturnValue(null));
        }

        [Test]
        public void TestGenerateName()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            gen.GenerateName(method);
            methodUtility.Received().GenerateName(method);
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
            methodUtility.Received().GenerateCommentID(method);
        }

        [Test]
        public void TestGenerateCommentIDThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateCommentID(null));
        }

        [Test]
        public void TestGenerateAccess()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            gen.GenerateAccess(method);
            methodUtility.Received().GenerateAccess(method);
        }

        [Test]
        public void TestGenerateAccessThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAccess(null));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            MethodInfo method = typeof(MethodTestClass).GetMethod("Method");
            gen.GenerateModifiers(method);
            methodUtility.Received().GenerateModifiers(method);
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
            methodUtility.Received().GenerateAttributes(method);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
        }
    }
}