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
    public class EventDataGeneratorTest
    {
        EventDataGenerator gen;
        IDataGeneratorUtility docUtility;
        IMethodBaseUtility methodUtility;
        ICommentIDGenerator idGen;

        [SetUp]
        public void SetUp()
        {
            docUtility = Substitute.For<IDataGeneratorUtility>();
            idGen = Substitute.For<ICommentIDGenerator>();
            methodUtility = new MethodBaseUtility(docUtility, idGen);
            gen = new EventDataGenerator(docUtility, methodUtility, idGen);
        }

        [Test]
        public void TestGenerateMemberData()
        {
            EventInfo ev = typeof(EventTestClass).GetEvent("PublicEvent");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[] { new MemberRef(MemberRefType.Class, typeof(STAThreadAttribute).MetadataToken) };
            docUtility.GenerateName(ev).Returns("CSDocNet.TestDoc.EventTestClass.Event");
            docUtility.GenerateAttributes(ev).Returns(expectedAttr);
            idGen.GenerateMemberID(ev).Returns("M:CSDocNet.TestDoc.EventTestClass.Event");

            var name = gen.GenerateName(ev);
            var id = gen.GenerateCommentID(ev);
            var access = gen.GenerateAccess(ev);
            var modifiers = gen.GenerateModifiers(ev);
            var attr = gen.GenerateAttributes(ev);

            MemberData mData = gen.GenerateMemberData(ev);
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
            EventInfo ev = typeof(EventTestClass).GetEvent("PublicEvent");
            gen.GenerateName(ev);

            docUtility.Received().GenerateName(ev);
        }

        [Test]
        public void TestGenerateNameThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateName(null));
        }

        [Test]
        public void TestGenerateCommentID()
        {
            EventInfo ev = typeof(EventTestClass).GetEvent("PublicEvent");
            gen.GenerateCommentID(ev);

            idGen.Received().GenerateMemberID(ev);
        }        

        [Test]
        public void TestGenerateAccess()
        {
            Type t = typeof(EventTestClass);
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            EventInfo publicEvent = t.GetEvent("PublicEvent", flags);
            EventInfo protectedInternalEvent = t.GetEvent("ProtectedInternalEvent", flags);
            EventInfo internalEvent = t.GetEvent("InternalEvent", flags);
            EventInfo protectedEvent = t.GetEvent("protectedEvent", flags);
            EventInfo privateProtectedEvent = t.GetEvent("privateProtectedEvent", flags);
            EventInfo privateEvent = t.GetEvent("privateEvent", flags);
            EventInfo defaultEvent = t.GetEvent("defaultEvent", flags);

            Assert.AreEqual(AccessModifier.Public, gen.GenerateAccess(publicEvent));
            Assert.AreEqual(AccessModifier.ProtectedInternal, gen.GenerateAccess(protectedInternalEvent));
            Assert.AreEqual(AccessModifier.Internal, gen.GenerateAccess(internalEvent));
            Assert.AreEqual(AccessModifier.Protected, gen.GenerateAccess(protectedEvent));
            Assert.AreEqual(AccessModifier.PrivateProtected, gen.GenerateAccess(privateProtectedEvent));
            Assert.AreEqual(AccessModifier.Private, gen.GenerateAccess(privateEvent));
            Assert.AreEqual(AccessModifier.Private, gen.GenerateAccess(defaultEvent));
        }

        [Test]
        public void TestGenerateAccessThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAccess(null));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            EventInfo ev = typeof(EventTestClass).GetEvent("PublicEvent");

            Assert.AreEqual(Modifier.None, gen.GenerateModifiers(ev));
        }

        [Test]
        public void TestGenerateModifiersThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateModifiers(null));
        }

        [Test]
        public void TestGenerateAttributes()
        {
            EventInfo ev = typeof(EventTestClass).GetEvent("PublicEvent");
            gen.GenerateAttributes(ev);

            docUtility.Received().GenerateAttributes(ev);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
        }
    }
}