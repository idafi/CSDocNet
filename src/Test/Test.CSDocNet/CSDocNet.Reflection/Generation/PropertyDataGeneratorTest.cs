using System;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using CSDocNet.Reflection.Data;
using CSDocNet.TestDoc;

namespace CSDocNet.Reflection.Generation
{
    [TestFixture]
    public class PropertyDataGeneratorTest
    {
        PropertyDataGenerator gen;
        IDataGeneratorUtility docUtility;
        IMethodBaseUtility methodUtility;
        ICommentIDGenerator idGen;

        [SetUp]
        public void SetUp()
        {
            docUtility = Substitute.For<IDataGeneratorUtility>();
            idGen = Substitute.For<ICommentIDGenerator>();
            methodUtility = new MethodBaseUtility(docUtility, idGen);
            gen = new PropertyDataGenerator(docUtility, methodUtility, idGen);
        }

        [Test]
        public void TestGeneratePropertyData()
        {
            PropertyInfo property = typeof(PropertyTestClass).GetProperty("GetOnlyProperty");

            MemberData member = gen.GenerateMemberData(property);
            var getAccess = gen.GenerateAccessor(property.GetMethod);

            PropertyData pData = gen.GeneratePropertyData(property);
            Assert.AreEqual(getAccess.Access, pData.GetAccessor.Access);
            Assert.AreEqual(getAccess.IsDefined, pData.GetAccessor.IsDefined);
            Assert.AreEqual(false, pData.SetAccessor.IsDefined);
        }

        [Test]
        public void TestGeneratePropertyDataThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GeneratePropertyData(null));
        }

        [Test]
        public void TestGenerateMemberData()
        {
            PropertyInfo property = typeof(PropertyTestClass).GetProperty("PublicProperty");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[]
            { new MemberRef("STAThreadAttribute", MemberRefType.Class, typeof(STAThreadAttribute).MetadataToken) };
            docUtility.GenerateName(property).Returns("CSDocNet.TestDoc.PropertyTestClass.Property");
            docUtility.GenerateAttributes(property).Returns(expectedAttr);
            idGen.GenerateMemberID(property).Returns("M:CSDocNet.TestDoc.PropertyTestClass.Property");

            var name = gen.GenerateName(property);
            var id = gen.GenerateCommentID(property);
            var access = gen.GenerateAccess(property);
            var modifiers = gen.GenerateModifiers(property);
            var attr = gen.GenerateAttributes(property);

            MemberData mData = gen.GenerateMemberData(property);
            Assert.AreEqual(name, mData.Name);
            Assert.AreEqual(id, mData.CommentID);
            Assert.AreEqual(access, mData.Access);
            Assert.AreEqual(modifiers, mData.Modifiers);
            Assert.That(mData.Attributes, Is.EquivalentTo(attr));
        }

        [Test]
        public void TestGenerateMemberDataThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateMemberData(null));
        }

        [Test]
        public void TestGenerateName()
        {
            PropertyInfo property = typeof(PropertyTestClass).GetProperty("PublicProperty");
            gen.GenerateName(property);

            docUtility.Received().GenerateName(property);
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

            idGen.Received().GenerateMemberID(property);
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

            docUtility.Received().GenerateAttributes(property);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
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
        public void TestGenerateIndexerParams()
        {
            PropertyInfo property = typeof(TestClass).GetProperty("Item");
            MemberRef mRef = new MemberRef("Int32", MemberRefType.Struct, typeof(int).MetadataToken);
            docUtility.MakeMemberRef(typeof(int)).Returns(mRef);

            var iParams = gen.GenerateIndexerParams(property);
            Assert.AreEqual(1, iParams.Count);
            Assert.AreEqual(mRef.ID, iParams[0].Type.ID);
        }

        [Test]
        public void TestGenerateIndexerParamsThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateIndexerParams(null));
        }
    }
}