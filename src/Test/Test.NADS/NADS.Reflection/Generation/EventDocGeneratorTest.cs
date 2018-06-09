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
    public class EventDocGeneratorTest
    {
        EventDocGenerator gen;
        IDocGeneratorUtility utility;
        ICommentIDGenerator idGen;
        MethodDocGenerator methodGen;

        [SetUp]
        public void SetUp()
        {
            utility = Substitute.For<IDocGeneratorUtility>();
            idGen = Substitute.For<ICommentIDGenerator>();
            methodGen = new MethodDocGenerator(utility, idGen);
            gen = new EventDocGenerator(utility, idGen, methodGen);
        }

        [Test]
        public void TestGenerateMemberDoc()
        {
            EventInfo ev = typeof(EventTestClass).GetEvent("PublicEvent");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[] { new MemberRef(MemberRefType.Class, typeof(STAThreadAttribute).MetadataToken) };
            utility.GenerateName(ev).Returns("NADS.TestDoc.EventTestClass.Event");
            utility.GenerateAttributes(ev).Returns(expectedAttr);
            idGen.GenerateMemberID(ev).Returns("M:NADS.TestDoc.EventTestClass.Event");

            var name = gen.GenerateName(ev);
            var id = gen.GenerateCommentID(ev);
            var access = gen.GenerateAccess(ev);
            var modifiers = gen.GenerateModifiers(ev);
            var attr = gen.GenerateAttributes(ev);

            MemberDoc mDoc = gen.GenerateMemberDoc(ev);
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
        public void TestGenerateName()
        {
            EventInfo ev = typeof(EventTestClass).GetEvent("PublicEvent");
            gen.GenerateName(ev);

            utility.Received().GenerateName(ev);
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

            utility.Received().GenerateAttributes(ev);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
        }
    }
}