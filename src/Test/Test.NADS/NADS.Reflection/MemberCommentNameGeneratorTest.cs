using System;
using System.Reflection;
using NUnit.Framework;
using NADS.TestDoc;

namespace NADS.Reflection
{
    [TestFixture]
    public class MemberCommentNameGeneratorTest
    {
        MemberCommentNameGenerator generator;
        
        [SetUp]
        public void SetUp()
        {
            generator = new MemberCommentNameGenerator();
        }

        [Test]
        public void TestGenerateTypeName()
        {
            var t = typeof(TestClass);
            string name = generator.GenerateTypeName(t);
            Assert.AreEqual("T:NADS.TestDoc.TestClass", name);
        }

        [Test]
        public void TestGenerateTypeNameWithNestedType()
        {
            var t = typeof(TestClass.NestedClass);
            string name = generator.GenerateTypeName(t);
            Assert.AreEqual("T:NADS.TestDoc.TestClass.NestedClass", name);
        }

        [Test]
        public void TestGenerateTypeNameWithGenericType()
        {
            var t = typeof(GenericClass<>);
            string name = generator.GenerateTypeName(t);
            Assert.AreEqual("T:NADS.TestDoc.GenericClass`1", name);
        }

        [Test]
        public void TestGenerateTypeNameWithNestedGenericType()
        {
            var t = typeof(GenericClass<>.GenericNestedClass<>);
            string name = generator.GenerateTypeName(t);
            Assert.AreEqual("T:NADS.TestDoc.GenericClass`1.GenericNestedClass`1", name);
        }

        [Test]
        public void TestGenerateTypeNameThrowsIfNull()
        {
            Assert.Throws<ArgumentNullException>(() => generator.GenerateTypeName(null));
        }

        [Test]
        public void TestGenerateFieldName()
        {
            var f = typeof(TestClass).GetField("IntField");
            string name = generator.GenerateFieldName(f);
            Assert.AreEqual("F:NADS.TestDoc.TestClass.IntField", name);
        }

        [Test]
        public void TestGenerateNestedFieldName()
        {
            var f = typeof(TestClass.NestedClass).GetField("NestedField");
            string name = generator.GenerateFieldName(f);
            Assert.AreEqual("F:NADS.TestDoc.TestClass.NestedClass.NestedField", name);
        }
        
        [Test]
        public void TestGenerateFieldOnGenericClass()
        {
            var f = typeof(GenericClass<>).GetField("GenericField");
            string name = generator.GenerateFieldName(f);
            Assert.AreEqual("F:NADS.TestDoc.GenericClass`1.GenericField", name);
        }

        [Test]
        public void TestGenerateNestedFieldNameOnGenericClass()
        {
            var f = typeof(GenericClass<>.GenericNestedClass<>).GetField("NestedField");
            string expected = "F:NADS.TestDoc.GenericClass`1.GenericNestedClass`1.NestedField";
            string name = generator.GenerateFieldName(f);
            Assert.AreEqual(expected, name);
        }

        [Test]
        public void TestGeneratePointerFieldName()
        {
            var f = typeof(UnsafeStruct).GetField("PointerField");
            string name = generator.GenerateFieldName(f);
            Assert.AreEqual("F:NADS.TestDoc.UnsafeStruct.PointerField", name);
        }

        [Test]
        public void TestGenerateFixedBufferFieldName()
        {
            var f = typeof(UnsafeStruct).GetField("FixedArray");
            string name = generator.GenerateFieldName(f);
            Assert.AreEqual("F:NADS.TestDoc.UnsafeStruct.FixedArray", name);
        }
    }
}