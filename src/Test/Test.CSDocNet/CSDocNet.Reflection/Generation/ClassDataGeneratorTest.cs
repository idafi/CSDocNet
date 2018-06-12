using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using NSubstitute;
using CSDocNet.Reflection.Data;
using CSDocNet.TestDoc;

namespace CSDocNet.Reflection.Generation
{
    [TestFixture]
    public class ClassDataGeneratorTest
    {
        const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static
            | BindingFlags.Public | BindingFlags.NonPublic
            | BindingFlags.DeclaredOnly;

        ClassDataGenerator gen;
        IDataGeneratorUtility docUtility;
        ITypeDataUtility typeUtility;
        ICommentIDGenerator idGen;

        [SetUp]
        public void SetUp()
        {
            docUtility = Substitute.For<IDataGeneratorUtility>();
            typeUtility = Substitute.For<ITypeDataUtility>();
            idGen = Substitute.For<ICommentIDGenerator>();
            gen = new ClassDataGenerator(docUtility, typeUtility, idGen);
        }

        [Test]
        public void TestGenerateName()
        {
            Type c = typeof(TestClass);
            gen.GenerateName(c);
            docUtility.Received().GenerateName(c);
        }

        [Test]
        public void TestGenerateNameThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateName(null));
        }

        [Test]
        public void TestGenerateCommentID()
        {
            Type c = typeof(TestClass);
            gen.GenerateCommentID(c);
            idGen.Received().GenerateMemberID(c);
        }

        [Test]
        public void TestGenerateCommentIDThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateCommentID(null));
        }

        [Test]
        public void TestGenerateAccess()
        {
            Type c = typeof(TestClass);
            gen.GenerateAccess(c);
            typeUtility.Received().GenerateAccess(c);
        }

        [Test]
        public void TestGenerateAccessThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAccess(null));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            Type c = typeof(TestClass);
            gen.GenerateModifiers(c);
            typeUtility.Received().GenerateModifiers(c);
        }

        [Test]
        public void TestGenerateModifiersThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateModifiers(null));
        }

        [Test]
        public void TestGenerateAttributes()
        {
            Type c = typeof(TestClass);
            gen.GenerateAttributes(c);
            docUtility.Received().GenerateAttributes(c);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
        }

        [Test]
        public void TestGenerateEvents()
        {
            Type t = typeof(ClassTestClass);
            gen.GenerateEvents(t);
            docUtility.Received().MakeMemberRef(t.GetEvent("EventA", bindingFlags));
            docUtility.Received().MakeMemberRef(t.GetEvent("EventB", bindingFlags));
            docUtility.Received().MakeMemberRef(t.GetEvent("eventC", bindingFlags));
        }

        [Test]
        public void TestGenerateEventsThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateEvents(null));
        }

        [Test]
        public void TestGenerateFields()
        {
            Type t = typeof(ClassTestClass);
            gen.GenerateFields(t);
            docUtility.Received().MakeMemberRef(t.GetField("FieldA", bindingFlags));
            docUtility.Received().MakeMemberRef(t.GetField("FieldB", bindingFlags));
            docUtility.Received().MakeMemberRef(t.GetField("fieldC", bindingFlags));
        }

        [Test]
        public void TestGenerateFieldsThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateEvents(null));
        }

        [Test]
        public void TestGenerateProperties()
        {
            Type t = typeof(ClassTestClass);
            gen.GenerateProperties(t);
            docUtility.Received().MakeMemberRef(t.GetProperty("PropertyA", bindingFlags));
            docUtility.Received().MakeMemberRef(t.GetProperty("PropertyB", bindingFlags));
            docUtility.Received().MakeMemberRef(t.GetProperty("propertyC", bindingFlags));
        }

        [Test]
        public void TestGeneratePropertiesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateEvents(null));
        }

        [Test]
        public void TestGenerateConstructors()
        {
            Type t = typeof(ClassTestClass);
            gen.GenerateConstructors(t);
            docUtility.Received().MakeMemberRef(t.GetConstructor(bindingFlags, null, new Type[0], null));
            docUtility.Received().MakeMemberRef(t.GetConstructor(bindingFlags, null, new Type[] { typeof(int) }, null));
            docUtility.Received().MakeMemberRef(t.GetConstructor(bindingFlags, null, new Type[] { typeof(float) }, null));
        }

        [Test]
        public void TestGenerateConstructorsThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateEvents(null));
        }

        [Test]
        public void TestGenerateClassDataMethods()
        {
            Type t = typeof(ClassTestClass);
            var methods = gen.GenerateClassDataMethods(t);

            docUtility.Received().MakeMemberRef(t.GetMethod("op_LogicalNot", bindingFlags));
            docUtility.Received().MakeMemberRef(t.GetMethod("op_Increment", bindingFlags));
            docUtility.Received().MakeMemberRef(t.GetMethod("MethodA", bindingFlags));
            docUtility.Received().MakeMemberRef(t.GetMethod("MethodB", bindingFlags));
            docUtility.Received().MakeMemberRef(t.GetMethod("MethodC", bindingFlags));

            Assert.AreEqual(2, methods.Operators.Count);
            Assert.AreEqual(4, methods.Methods.Count);  // "4?" don't forget ShutUpCompiler()
        }

        [Test]
        public void TestGenerateClassDataMethodsThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateClassDataMethods(null));
        }
    }
}