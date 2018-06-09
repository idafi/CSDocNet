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
    public class ConstructorDocGeneratorTest
    {
        ConstructorDocGenerator gen;
        IMethodBaseUtility methodUtility;

        [SetUp]
        public void SetUp()
        {

            methodUtility = Substitute.For<IMethodBaseUtility>();
            gen = new ConstructorDocGenerator(methodUtility);
        }

        [Test]
        public void TestGenerateConstructorDoc()
        {
            ConstructorInfo ctor = typeof(TestClass).GetConstructor(new Type[] { typeof(int) });
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[] { new MemberRef(MemberRefType.Class, typeof(STAThreadAttribute).MetadataToken) };
            methodUtility.GenerateName(ctor).Returns("NADS.TestDoc.TestClass..ctor");
            methodUtility.GenerateAttributes(ctor).Returns(expectedAttr);

            MemberDoc member = gen.GenerateMemberDoc(ctor);
            var parameters = methodUtility.GenerateParams(ctor);
            var typeParams = methodUtility.GenerateTypeParams(ctor);

            MethodDoc cDoc = gen.GenerateConstructorDoc(ctor);
            Assert.AreEqual(member, cDoc.Member);
            Assert.That(cDoc.Params, Is.EquivalentTo(parameters));
            Assert.That(cDoc.TypeParams, Is.EquivalentTo(typeParams));
        }

        [Test]
        public void TestGenerateConstructorDocThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateConstructorDoc(null));
        }

        [Test]
        public void TestGenerateMemberDoc()
        {
            ConstructorInfo ctor = typeof(TestClass).GetConstructor(new Type[] { typeof(int) });
            gen.GenerateMemberDoc(ctor);
            methodUtility.Received().GenerateMemberDoc(ctor);
        }

        [Test]
        public void TestGenerateMemberDocThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateMemberDoc(null));
        }

        [Test]
        public void TestGenerateName()
        {
            ConstructorInfo ctor = typeof(TestClass).GetConstructor(new Type[] { typeof(int) });
            gen.GenerateName(ctor);
            methodUtility.Received().GenerateName(ctor);
        }

        [Test]
        public void TestGenerateNameThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateName(null));
        }

        [Test]
        public void TestGenerateCommentID()
        {
            ConstructorInfo ctor = typeof(TestClass).GetConstructor(new Type[] { typeof(int) });
            gen.GenerateCommentID(ctor);
            methodUtility.Received().GenerateCommentID(ctor);
        }

        [Test]
        public void TestGenerateCommentIDThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateCommentID(null));
        }

        [Test]
        public void TestGenerateAccess()
        {
            ConstructorInfo ctor = typeof(TestClass).GetConstructor(new Type[] { typeof(int) });
            gen.GenerateAccess(ctor);
            methodUtility.Received().GenerateAccess(ctor);
        }

        [Test]
        public void TestGenerateAccessThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAccess(null));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            ConstructorInfo ctor = typeof(TestClass).GetConstructor(new Type[] { typeof(int) });
            gen.GenerateModifiers(ctor);
            methodUtility.Received().GenerateModifiers(ctor);
        }

        [Test]
        public void TestGenerateModifiersThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateModifiers(null));
        }

        [Test]
        public void TestGenerateAttributes()
        {
            ConstructorInfo ctor = typeof(TestClass).GetConstructor(new Type[] { typeof(int) });
            gen.GenerateAttributes(ctor);
            methodUtility.Received().GenerateAttributes(ctor);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
        }
    }
}