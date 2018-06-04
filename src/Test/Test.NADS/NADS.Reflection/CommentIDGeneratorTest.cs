using System;
using System.Reflection;
using NUnit.Framework;
using NADS.TestDoc;

namespace NADS.Reflection.Generation
{
    [TestFixture]
    public class CommentIDGeneratorTest
    {
        CommentIDGenerator generator;
        
        [SetUp]
        public void SetUp()
        {
            generator = new CommentIDGenerator();
        }

        [Test]
        public void TestGenerateTypeID()
        {
            var t = typeof(TestClass);
            string id = generator.GenerateTypeID(t);
            Assert.AreEqual("T:NADS.TestDoc.TestClass", id);
        }

        [Test]
        public void TestGenerateTypeIDWithNestedType()
        {
            var t = typeof(TestClass.NestedClass);
            string id = generator.GenerateTypeID(t);
            Assert.AreEqual("T:NADS.TestDoc.TestClass.NestedClass", id);
        }

        [Test]
        public void TestGenerateTypeIDWithGenericType()
        {
            var t = typeof(GenericClass<>);
            string id = generator.GenerateTypeID(t);
            Assert.AreEqual("T:NADS.TestDoc.GenericClass`1", id);
        }

        [Test]
        public void TestGenerateTypeIDWithNestedGenericType()
        {
            var t = typeof(GenericClass<>.GenericNestedClass<>);
            string id = generator.GenerateTypeID(t);
            Assert.AreEqual("T:NADS.TestDoc.GenericClass`1.GenericNestedClass`1", id);
        }

        [Test]
        public void TestGenerateTypeIDWithStruct()
        {
            var t = typeof(TestStruct);
            string id = generator.GenerateTypeID(t);
            Assert.AreEqual("T:NADS.TestDoc.TestStruct", id);
        }

        [Test]
        public void TestGenerateTypeIDWithUnsafeStruct()
        {
            var t = typeof(UnsafeStruct);
            string id = generator.GenerateTypeID(t);
            Assert.AreEqual("T:NADS.TestDoc.UnsafeStruct", id);
        }

        [Test]
        public void TestGenerateEnumID()
        {
            var t = typeof(TestEnum);
            string id = generator.GenerateTypeID(t);
            Assert.AreEqual("T:NADS.TestDoc.TestEnum", id);
        }

        [Test]
        public void TestGenerateDelegateID()
        {
            var t = typeof(TestDoc.TestDelegate);
            string id = generator.GenerateTypeID(t);
            Assert.AreEqual("T:NADS.TestDoc.TestDelegate", id);
        }

        [Test]
        public void TestGenerateGenericDelegateID()
        {
            var t = typeof(GenericDelegate<>);
            string id = generator.GenerateTypeID(t);
            Assert.AreEqual("T:NADS.TestDoc.GenericDelegate`1", id);
        }

        [Test]
        public void TestGenerateTypeIDThrowsIfNull()
        {
            Assert.Throws<ArgumentNullException>(() => generator.GenerateTypeID(null));
        }

        [Test]
        public void TestGenerateFieldID()
        {
            var f = typeof(TestClass).GetField("IntField");
            string id = generator.GenerateFieldID(f);
            Assert.AreEqual("F:NADS.TestDoc.TestClass.IntField", id);
        }

        [Test]
        public void TestGenerateNestedFieldID()
        {
            var f = typeof(TestClass.NestedClass).GetField("NestedField");
            string id = generator.GenerateFieldID(f);
            Assert.AreEqual("F:NADS.TestDoc.TestClass.NestedClass.NestedField", id);
        }
        
        [Test]
        public void TestGenerateFieldOnGenericClass()
        {
            var f = typeof(GenericClass<>).GetField("GenericField");
            string id = generator.GenerateFieldID(f);
            Assert.AreEqual("F:NADS.TestDoc.GenericClass`1.GenericField", id);
        }

        [Test]
        public void TestGenerateNestedFieldIDOnGenericClass()
        {
            var f = typeof(GenericClass<>.GenericNestedClass<>).GetField("NestedField");
            string expected = "F:NADS.TestDoc.GenericClass`1.GenericNestedClass`1.NestedField";
            string id = generator.GenerateFieldID(f);
            Assert.AreEqual(expected, id);
        }

        [Test]
        public void TestGeneratePointerFieldID()
        {
            var f = typeof(UnsafeStruct).GetField("PointerField");
            string id = generator.GenerateFieldID(f);
            Assert.AreEqual("F:NADS.TestDoc.UnsafeStruct.PointerField", id);
        }

        [Test]
        public void TestGenerateFixedBufferFieldID()
        {
            var f = typeof(UnsafeStruct).GetField("FixedArray");
            string id = generator.GenerateFieldID(f);
            Assert.AreEqual("F:NADS.TestDoc.UnsafeStruct.FixedArray", id);
        }

        [Test]
        public void TestGenerateFieldIDThrowsIfNull()
        {
            Assert.Throws<ArgumentNullException>(() => generator.GenerateFieldID(null));
        }

        [Test]
        public void TestGeneratePropertyID()
        {
            var p = typeof(TestClass).GetProperty("IntProperty");
            string id = generator.GeneratePropertyID(p);
            Assert.AreEqual("P:NADS.TestDoc.TestClass.IntProperty", id);
        }

        [Test]
        public void TestGenerateMutablePropertyID()
        {
            var p = typeof(TestClass).GetProperty("MutableProperty");
            string id = generator.GeneratePropertyID(p);
            Assert.AreEqual("P:NADS.TestDoc.TestClass.MutableProperty", id);
        }

        [Test]
        public void TestGenerateIndexerPropertyID()
        {
            var p = typeof(TestClass).GetProperty("Item");
            string id = generator.GeneratePropertyID(p);
            Assert.AreEqual("P:NADS.TestDoc.TestClass.Item(System.Int32)", id);
        }

        [Test]
        public void TestGenerateNestedPropertyID()
        {
            var p = typeof(TestClass.NestedClass).GetProperty("NestedProperty");
            string id = generator.GeneratePropertyID(p);
            Assert.AreEqual("P:NADS.TestDoc.TestClass.NestedClass.NestedProperty", id);
        }

        [Test]
        public void TestGenerateGenericPropertyID()
        {
            var p = typeof(GenericClass<>).GetProperty("GenericProperty");
            string id = generator.GeneratePropertyID(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.GenericProperty", id);
        }

        [Test]
        public void TestGenerateGenericArrayPropertyID()
        {
            var p = typeof(GenericClass<>).GetProperty("GenericArrayProperty");
            string id = generator.GeneratePropertyID(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.GenericArrayProperty", id);
        }

        [Test]
        public void TestGenerateConstructedGenericPropertyID()
        {
            var p = typeof(GenericClass<>).GetProperty("ConstructedProperty");
            string id = generator.GeneratePropertyID(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.ConstructedProperty", id);
        }

        [Test]
        public void TestGenerateNestedGenericPropertyID()
        {
            var p = typeof(GenericClass<>.GenericNestedClass<>).GetProperty("NestedProperty");
            string id = generator.GeneratePropertyID(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.GenericNestedClass`1.NestedProperty", id);
        }

        [Test]
        public void TestGenerateGenericIndexerID()
        {
            var p = typeof(GenericClass<>).GetProperty("Item", new Type[] { typeof(GenericClass<>) });
            string id = generator.GeneratePropertyID(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.Item(NADS.TestDoc.GenericClass{`0})", id);
        }

        [Test]
        public void TestGenerateConstructedGenericIndexerID()
        {
            var p = typeof(GenericClass<>).GetProperty("Item", typeof(int));
            string id = generator.GeneratePropertyID(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.Item(NADS.TestDoc.GenericClass{System.Int32})", id);
        }

        [Test]
        public void TestGeneratePropertyIDThrowsIfNull()
        {
            Assert.Throws<ArgumentNullException>(() => generator.GeneratePropertyID(null));
        }

        [Test]
        public void TestGenerateMethodID()
        {
            var m = typeof(TestClass).GetMethod("IntMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.IntMethod(System.Int32)", id);
        }

        [Test]
        public void TestGenerateNestedMethodID()
        {
            var m = typeof(TestClass.NestedClass).GetMethod("NestedMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.NestedClass.NestedMethod(System.Int32)", id);
        }

        [Test]
        public void TestGenerateOperatorMethodID()
        {
            var m = typeof(TestClass).GetMethod("op_Addition");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.op_Addition(NADS.TestDoc.TestClass,NADS.TestDoc.TestClass)", id);
        }

        [Test]
        public void TestGenerateExplicitOperatorMethodID()
        {
            var m = typeof(TestClass).GetMethod("op_Explicit");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.op_Explicit(NADS.TestDoc.TestClass)~System.Int32", id);
        }

        [Test]
        public void TestGenerateImplicitOperatorMethodID()
        {
            var m = typeof(TestClass).GetMethod("op_Implicit");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.op_Implicit(NADS.TestDoc.TestClass)~System.Single", id);
        }

        [Test]
        public void TestGenerateOverrideMethodID()
        {
            var m = typeof(TestClass).GetMethod("Equals");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.Equals(System.Object)", id);
        }

        [Test]
        public void TestGenerateVirtualMethodID()
        {
            var m = typeof(TestClass).GetMethod("VirtualMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.VirtualMethod(System.Int32)", id);
        }

        [Test]
        public void TestGenerateNoParamMethodID()
        {
            var m = typeof(TestClass).GetMethod("NoParamMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.NoParamMethod", id);
        }

        [Test]
        public void TestGenerateNullableMethodID()
        {
            var m = typeof(TestClass).GetMethod("NullableMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.NullableMethod(System.Nullable{System.Int32})", id);
        }

        [Test]
        public void TestGenerateRefMethodID()
        {
            var m = typeof(TestClass).GetMethod("RefMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.RefMethod(System.Int32@,System.Int32@,System.Int32@)", id);
        }

        [Test]
        public void TestGenerateArrayMethodID()
        {
            var m = typeof(TestClass).GetMethod("ArrayMethod");
            string expected = "M:NADS.TestDoc.TestClass.ArrayMethod(System.Int32[],System.Int32[0:,0:],System.Int32[][])";
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual(expected, id);
        }

        [Test]
        public void TestGenerateGenericMethodID()
        {
            var m = typeof(TestClass).GetMethod("GenericMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.GenericMethod``1(``0)", id);
        }

        [Test]
        public void TestGenerateGenericClassMethodID()
        {
            var m = typeof(GenericClass<>).GetMethod("GenericMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.GenericClass`1.GenericMethod``1(`0,``0)", id);
        }

        [Test]
        public void TestGenerateGenericClassConstructedMethodID()
        {
            var m = typeof(GenericClass<>).GetMethod("ConstructedMethod");
            string expected = "M:NADS.TestDoc.GenericClass`1.ConstructedMethod``1(`0,``0,NADS.TestDoc.GenericClass{System.Int32})";
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual(expected, id);
        }

        [Test]
        public void TestGenerateGenericClassNestedMethodID()
        {
            var m = typeof(GenericClass<>.GenericNestedClass<>).GetMethod("NestedMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.GenericClass`1.GenericNestedClass`1.NestedMethod``1(`0,`1,``0)", id);
        }

        [Test]
        public void TestGenerateConversionOperatorWithGenericParamID()
        {
            var m = typeof(GenericClass<>).GetMethod("op_Explicit");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.GenericClass`1.op_Explicit(NADS.TestDoc.GenericClass{`0})~System.Int32", id);
        }

        public void TestGenerateConversionOperatorWithGenericReturnID()
        {
            var m = typeof(GenericClass<>).GetMethod("op_Implicit");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.GenericClass`1.op_Implicit(System.Int32)~NADS.TestDoc.GenericClass{`0}", id);
        }

        [Test]
        public void TestGenerateTheMonsterMethodID()
        {
            // hold onto your butts !!
            string expected =
                "M:NADS.TestDoc.GenericClass`1.GenericNestedClass`1.TheMonsterMethod``1(`0,`1,``0,NADS.TestDoc.GenericClass{System.Int32}.GenericNestedClass{``0}[][]@,``0[][]@,`1[0:,0:,0:]@)";

            var m = typeof(GenericClass<>.GenericNestedClass<>).GetMethod("TheMonsterMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual(expected, id);
        }

        [Test]
        public void TestGeneratePointerMethodID()
        {
            var m = typeof(UnsafeStruct).GetMethod("PointerMethod");
            string id = generator.GenerateMethodID(m);
            Assert.AreEqual("M:NADS.TestDoc.UnsafeStruct.PointerMethod(System.Int32*,System.Single*)", id);
        }

        [Test]
        public void TestGenerateMethodIDThrowsIfNull()
        {
            Assert.Throws<ArgumentNullException>(() => generator.GenerateMethodID((MethodInfo)(null)));
        }

        [Test]
        public void TestGenerateConstructorID()
        {
            var c = typeof(TestClass).GetConstructor(new Type[] { typeof(int) });
            string id = generator.GenerateMethodID(c);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.#ctor(System.Int32)", id);
        }

        [Test]
        public void TestGenerateGenericConstructorID()
        {
            Type genericType = typeof(GenericClass<>);
            Type typeParam = genericType.GetGenericArguments()[0];

            var c = typeof(GenericClass<>).GetConstructor(new Type[] { typeParam, typeof(GenericClass<int>) });
            string id = generator.GenerateMethodID(c);
            Assert.AreEqual("M:NADS.TestDoc.GenericClass`1.#ctor(`0,NADS.TestDoc.GenericClass{System.Int32})", id);
        }

        [Test]
        public void TestGenerateConstructorIDThrowsIfNull()
        {
            Assert.Throws<ArgumentNullException>(() => generator.GenerateMethodID((ConstructorInfo)(null)));
        }

        [Test]
        public void TestGenerateEventID()
        {
            var e = typeof(TestClass).GetEvent("ActionEvent");
            string id = generator.GenerateEventID(e);
            Assert.AreEqual("E:NADS.TestDoc.TestClass.ActionEvent", id);
        }

        [Test]
        public void TestGenerateEventWithTypeParamsID()
        {
            var e = typeof(TestClass).GetEvent("IntActionEvent");
            string id = generator.GenerateEventID(e);
            Assert.AreEqual("E:NADS.TestDoc.TestClass.IntActionEvent", id);
        }

        [Test]
        public void TestGenerateGenericEventID()
        {
            var e = typeof(GenericClass<>).GetEvent("GenericEvent");
            string id = generator.GenerateEventID(e);
            Assert.AreEqual("E:NADS.TestDoc.GenericClass`1.GenericEvent", id);
        }

        [Test]
        public void TestGenerateConstructedGenericEventID()
        {
            var e = typeof(GenericClass<>).GetEvent("ConstructedEvent");
            string id = generator.GenerateEventID(e);
            Assert.AreEqual("E:NADS.TestDoc.GenericClass`1.ConstructedEvent", id);
        }

        [Test]
        public void TestGenerateEventIDThrowsIfNull()
        {
            Assert.Throws<ArgumentNullException>(() => generator.GenerateEventID(null));
        }
    }
}