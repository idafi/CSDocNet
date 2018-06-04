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
    public class FieldDocGeneratorTest
    {
        FieldDocGenerator gen;
        IDocGeneratorUtility utility;
        ICommentIDGenerator idGen;

        [SetUp]
        public void SetUp()
        {
            utility = Substitute.For<IDocGeneratorUtility>();
            idGen = Substitute.For<ICommentIDGenerator>();
            gen = new FieldDocGenerator(utility, idGen);
        }

        public void TestGenerateFieldDoc()
        {
            FieldInfo field = typeof(FieldTestClass).GetField("ConstField");
            utility.GenerateName(field).Returns("NADS.TestDoc.FieldTestClass.ConstField");
            utility.GenerateAttributes(field).Returns(new MemberRef[0]);
            idGen.GenerateFieldID(field).Returns("F:NADS.TestDoc.FieldTestClass.ConstField");

            var name = gen.GenerateName(field);
            var id = gen.GenerateCommentID(field);
            var access = gen.GenerateAccess(field);
            var modifiers = gen.GenerateModifiers(field);
            var attr = gen.GenerateAttributes(field);
            MemberDoc mDoc = gen.GenerateMemberDoc(field);

            FieldDoc fDoc = gen.GenerateFieldDoc(field);
            Assert.AreEqual(mDoc, fDoc.Member);
            Assert.AreEqual(666, fDoc.ConstValue);
        }

        [Test]
        public void TestGenerateFieldDocThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateFieldDoc(null));
        }

        [Test]
        public void TestGenerateMemberDoc()
        {
            FieldInfo field = typeof(FieldTestClass).GetField("StaticReadonlyField");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[] { new MemberRef(MemberRefType.Class, "System.NonSerializedAttribute") };
            utility.GenerateName(field).Returns("NADS.TestDoc.FieldTestClass.StaticReadonlyField");
            utility.GenerateAttributes(field).Returns(expectedAttr);
            idGen.GenerateFieldID(field).Returns("F:NADS.TestDoc.FieldTestClass.StaticReadonlyField");

            var name = gen.GenerateName(field);
            var id = gen.GenerateCommentID(field);
            var access = gen.GenerateAccess(field);
            var modifiers = gen.GenerateModifiers(field);
            var attr = gen.GenerateAttributes(field);

            MemberDoc mDoc = gen.GenerateMemberDoc(field);
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
        public void TestGenerateConstValue()
        {
            FieldInfo field = typeof(FieldTestClass).GetField("ConstField");
            object val = gen.GenerateConstValue(field);

            Assert.AreEqual(666, val);
        }

        [Test]
        public void TestGenerateConstValueWithNonConstField()
        {
            FieldInfo field = typeof(TestClass).GetField("IntField");
            Assert.IsNull(gen.GenerateConstValue(field));
        }

        [Test]
        public void TestGenerateConstValueThrowsOnNullField()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateConstValue(null));
        }

        [Test]
        public void TestGenerateName()
        {
            FieldInfo field = typeof(TestClass).GetField("IntField");
            gen.GenerateName(field);

            utility.Received().GenerateName(field);
        }

        [Test]
        public void TestGenerateNameThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateName(null));
        }

        [Test]
        public void TestGenerateCommentID()
        {
            FieldInfo field = typeof(TestClass).GetField("IntField");
            gen.GenerateCommentID(field);

            idGen.Received().GenerateFieldID(field);
        }

        [Test]
        public void TestGenerateCommentIDThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateCommentID(null));
        }

        [Test]
        public void TestGenerateAccess()
        {
            Type t = typeof(FieldTestClass);
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            FieldInfo publicField = t.GetField("PublicField", flags);
            FieldInfo protectedInternalField = t.GetField("ProtectedInternalField", flags);
            FieldInfo internalField = t.GetField("InternalField", flags);
            FieldInfo protectedField = t.GetField("protectedField", flags);
            FieldInfo privateProtectedField = t.GetField("privateProtectedField", flags);
            FieldInfo privateField = t.GetField("privateField", flags);
            FieldInfo defaultField = t.GetField("defaultField", flags);

            Assert.AreEqual(AccessModifier.Public, gen.GenerateAccess(publicField));
            Assert.AreEqual(AccessModifier.ProtectedInternal, gen.GenerateAccess(protectedInternalField));
            Assert.AreEqual(AccessModifier.Internal, gen.GenerateAccess(internalField));
            Assert.AreEqual(AccessModifier.Protected, gen.GenerateAccess(protectedField));
            Assert.AreEqual(AccessModifier.PrivateProtected, gen.GenerateAccess(privateProtectedField));
            Assert.AreEqual(AccessModifier.Private, gen.GenerateAccess(privateField));
            Assert.AreEqual(AccessModifier.Private, gen.GenerateAccess(defaultField));
        }

        [Test]
        public void TestGenerateAccessThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAccess(null));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            Type t = typeof(FieldTestClass);

            FieldInfo normalField = t.GetField("PublicField");
            FieldInfo constField = t.GetField("ConstField");
            FieldInfo readonlyField = t.GetField("ReadonlyField");
            FieldInfo staticField = t.GetField("StaticField");
            FieldInfo volatileField = t.GetField("VolatileField");
            FieldInfo staticReadonlyField = t.GetField("StaticReadonlyField");
            FieldInfo staticVolatileField = t.GetField("StaticVolatileField");

            Assert.AreEqual(Modifier.None, gen.GenerateModifiers(normalField));
            Assert.AreEqual(Modifier.Const, gen.GenerateModifiers(constField));
            Assert.AreEqual(Modifier.Readonly, gen.GenerateModifiers(readonlyField));
            Assert.AreEqual(Modifier.Static, gen.GenerateModifiers(staticField));
            Assert.AreEqual(Modifier.Volatile, gen.GenerateModifiers(volatileField));
            Assert.AreEqual(Modifier.Static | Modifier.Readonly, gen.GenerateModifiers(staticReadonlyField));
            Assert.AreEqual(Modifier.Static | Modifier.Volatile, gen.GenerateModifiers(staticVolatileField));
        }

        [Test]
        public void TestGenerateModifiersThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateModifiers(null));
        }

        [Test]
        public void TestGenerateAttributes()
        {
            FieldInfo field = typeof(FieldTestClass).GetField("PublicField");
            gen.GenerateAttributes(field);

            utility.Received().GenerateAttributes(field);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
        }
    }
}