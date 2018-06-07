using System;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using NADS.Reflection.Data;
using NADS.TestDoc;

namespace NADS.Reflection.Generation
{
    [TestFixture]
    public class PropertyDocGeneratorTest
    {
        PropertyDocGenerator gen;
        IDocGeneratorUtility utility;
        ICommentIDGenerator idGen;
        MethodDocGenerator methodGen;

        [SetUp]
        public void SetUp()
        {
            utility = Substitute.For<IDocGeneratorUtility>();
            idGen = Substitute.For<ICommentIDGenerator>();
            methodGen = new MethodDocGenerator(utility, idGen);
            gen = new PropertyDocGenerator(utility, idGen, methodGen);
        }

        [Test]
        public void TestGeneratePropertyDoc()
        {
            PropertyInfo property = typeof(PropertyTestClass).GetProperty("GetOnlyProperty");

            MemberDoc member = gen.GenerateMemberDoc(property);
            var getAccess = gen.GenerateAccessor(property.GetMethod);

            PropertyDoc pDoc = gen.GeneratePropertyDoc(property);
            Assert.AreEqual(member, pDoc.Member);
            Assert.AreEqual(getAccess, pDoc.GetAccessor);
            Assert.AreEqual(false, pDoc.SetAccessor.IsDefined);
        }

        [Test]
        public void TestGeneratePropertyDocThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GeneratePropertyDoc(null));
        }

        [Test]
        public void TestGenerateMemberDoc()
        {
            PropertyInfo property = typeof(PropertyTestClass).GetProperty("PublicProperty");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[] { new MemberRef(MemberRefType.Class, "System.STAThreadAttribute") };
            utility.GenerateName(property).Returns("NADS.TestDoc.PropertyTestClass.Property");
            utility.GenerateAttributes(property).Returns(expectedAttr);
            idGen.GeneratePropertyID(property).Returns("M:NADS.TestDoc.PropertyTestClass.Property");

            var name = gen.GenerateName(property);
            var id = gen.GenerateCommentID(property);
            var access = gen.GenerateAccess(property);
            var modifiers = gen.GenerateModifiers(property);
            var attr = gen.GenerateAttributes(property);

            MemberDoc mDoc = gen.GenerateMemberDoc(property);
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
        public void TestGenerateAccessor()
        {
            PropertyInfo property = typeof(PropertyTestClass).GetProperty("ProtectedSetProperty");
            MethodInfo get = property.GetMethod;
            MethodInfo set = property.SetMethod;

            var getAccess = gen.GenerateAccessor(get);
            var setAccess = gen.GenerateAccessor(set);

            Assert.AreEqual(true, getAccess.IsDefined);
            Assert.AreEqual(true, setAccess.IsDefined);
            Assert.AreEqual(AccessModifier.Public, getAccess.Access);
            Assert.AreEqual(AccessModifier.Protected, setAccess.Access);
        }

        [Test]
        public void TestGenerateAccessorThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAccessor(null));
        }

        [Test]
        public void TestGenerateName()
        {
            PropertyInfo property = typeof(PropertyTestClass).GetProperty("PublicProperty");
            gen.GenerateName(property);

            utility.Received().GenerateName(property);
        }

        [Test]
        public void TestGenerateNameThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateName(null));
        }

        [Test]
        public void TestGenerateCommentID()
        {
            PropertyInfo property = typeof(PropertyTestClass).GetProperty("PublicProperty");
            gen.GenerateCommentID(property);

            idGen.Received().GeneratePropertyID(property);
        }        

        [Test]
        public void TestGenerateAccess()
        {
            Type t = typeof(PropertyTestClass);
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            PropertyInfo publicProperty = t.GetProperty("PublicProperty", flags);
            PropertyInfo protectedInternalProperty = t.GetProperty("ProtectedInternalProperty", flags);
            PropertyInfo internalProperty = t.GetProperty("InternalProperty", flags);
            PropertyInfo protectedProperty = t.GetProperty("protectedProperty", flags);
            PropertyInfo privateProtectedProperty = t.GetProperty("privateProtectedProperty", flags);
            PropertyInfo privateProperty = t.GetProperty("privateProperty", flags);
            PropertyInfo defaultProperty = t.GetProperty("defaultProperty", flags);
            PropertyInfo protectedSetProperty = t.GetProperty("ProtectedSetProperty", flags);
            PropertyInfo protectedGetProperty = t.GetProperty("ProtectedGetProperty", flags);

            Assert.AreEqual(AccessModifier.Public, gen.GenerateAccess(publicProperty));
            Assert.AreEqual(AccessModifier.ProtectedInternal, gen.GenerateAccess(protectedInternalProperty));
            Assert.AreEqual(AccessModifier.Internal, gen.GenerateAccess(internalProperty));
            Assert.AreEqual(AccessModifier.Protected, gen.GenerateAccess(protectedProperty));
            Assert.AreEqual(AccessModifier.PrivateProtected, gen.GenerateAccess(privateProtectedProperty));
            Assert.AreEqual(AccessModifier.Private, gen.GenerateAccess(privateProperty));
            Assert.AreEqual(AccessModifier.Private, gen.GenerateAccess(defaultProperty));
            Assert.AreEqual(AccessModifier.Public, gen.GenerateAccess(protectedSetProperty));
            Assert.AreEqual(AccessModifier.Public, gen.GenerateAccess(protectedGetProperty));
        }

        [Test]
        public void TestGenerateAccessThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAccess(null));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            Type t = typeof(PropertyTestClass);
            Type i = typeof(PropertyTestClass.Impl);

            PropertyInfo abstractProperty = t.GetProperty("AbstractProperty");
            PropertyInfo staticProperty = t.GetProperty("StaticProperty");
            PropertyInfo virtualProperty = t.GetProperty("VirtualProperty");
            PropertyInfo overrideAbstractProperty = i.GetProperty("AbstractProperty");
            PropertyInfo overrideVirtualProperty = i.GetProperty("VirtualProperty");

            Assert.AreEqual(Modifier.Abstract, gen.GenerateModifiers(abstractProperty));
            Assert.AreEqual(Modifier.Static, gen.GenerateModifiers(staticProperty));
            Assert.AreEqual(Modifier.Virtual, gen.GenerateModifiers(virtualProperty));
            Assert.AreEqual(Modifier.Sealed | Modifier.Override, gen.GenerateModifiers(overrideAbstractProperty));
            Assert.AreEqual(Modifier.Override, gen.GenerateModifiers(overrideVirtualProperty));
        }

        [Test]
        public void TestGenerateModifiersThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateModifiers(null));
        }

        [Test]
        public void TestGenerateAttributes()
        {
            PropertyInfo property = typeof(PropertyTestClass).GetProperty("PublicProperty");
            gen.GenerateAttributes(property);

            utility.Received().GenerateAttributes(property);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
        }
    }
}