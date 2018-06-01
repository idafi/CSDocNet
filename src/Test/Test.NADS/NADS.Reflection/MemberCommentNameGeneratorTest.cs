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

        [Test]
        public void TestGeneratePropertyName()
        {
            var p = typeof(TestClass).GetProperty("IntProperty");
            string name = generator.GeneratePropertyName(p);
            Assert.AreEqual("P:NADS.TestDoc.TestClass.IntProperty", name);
        }

        [Test]
        public void TestGenerateMutablePropertyName()
        {
            var p = typeof(TestClass).GetProperty("MutableProperty");
            string name = generator.GeneratePropertyName(p);
            Assert.AreEqual("P:NADS.TestDoc.TestClass.MutableProperty", name);
        }

        [Test]
        public void TestGenerateIndexerPropertyName()
        {
            var p = typeof(TestClass).GetProperty("Item");
            string name = generator.GeneratePropertyName(p);
            Assert.AreEqual("P:NADS.TestDoc.TestClass.Item(System.Int32)", name);
        }

        [Test]
        public void TestGenerateNestedPropertyName()
        {
            var p = typeof(TestClass.NestedClass).GetProperty("NestedProperty");
            string name = generator.GeneratePropertyName(p);
            Assert.AreEqual("P:NADS.TestDoc.TestClass.NestedClass.NestedProperty", name);
        }

        [Test]
        public void TestGenerateGenericPropertyName()
        {
            var p = typeof(GenericClass<>).GetProperty("GenericProperty");
            string name = generator.GeneratePropertyName(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.GenericProperty", name);
        }

        [Test]
        public void TestGenerateGenericArrayPropertyName()
        {
            var p = typeof(GenericClass<>).GetProperty("GenericArrayProperty");
            string name = generator.GeneratePropertyName(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.GenericArrayProperty", name);
        }

        [Test]
        public void TestGenerateConstructedGenericPropertyName()
        {
            var p = typeof(GenericClass<>).GetProperty("ConstructedProperty");
            string name = generator.GeneratePropertyName(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.ConstructedProperty", name);
        }

        [Test]
        public void TestGenerateNestedGenericPropertyName()
        {
            var p = typeof(GenericClass<>.GenericNestedClass<>).GetProperty("NestedProperty");
            string name = generator.GeneratePropertyName(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.GenericNestedClass`1.NestedProperty", name);
        }

        [Test]
        public void TestGenerateGenericIndexerName()
        {
            var p = typeof(GenericClass<>).GetProperty("Item", new Type[] { typeof(GenericClass<>) });
            string name = generator.GeneratePropertyName(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.Item(NADS.TestDoc.GenericClass{`0})", name);
        }

        [Test]
        public void TestGenerateConstructedGenericIndexerName()
        {
            var p = typeof(GenericClass<>).GetProperty("Item", typeof(int));
            string name = generator.GeneratePropertyName(p);
            Assert.AreEqual("P:NADS.TestDoc.GenericClass`1.Item(NADS.TestDoc.GenericClass{System.Int32})", name);
        }

        [Test]
        public void TestGenerateMethodName()
        {
            var m = typeof(TestClass).GetMethod("IntMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.IntMethod(System.Int32)", name);
        }

        [Test]
        public void TestGenerateNestedMethodName()
        {
            var m = typeof(TestClass.NestedClass).GetMethod("NestedMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.NestedClass.NestedMethod(System.Int32)", name);
        }

        [Test]
        public void TestGenerateOperatorMethodName()
        {
            var m = typeof(TestClass).GetMethod("op_Addition");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.op_Addition(NADS.TestDoc.TestClass,NADS.TestDoc.TestClass)", name);
        }

        [Test]
        public void TestGenerateExplicitOperatorMethodName()
        {
            var m = typeof(TestClass).GetMethod("op_Explicit");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.op_Explicit(NADS.TestDoc.TestClass)~System.Int32", name);
        }

        [Test]
        public void TestGenerateImplicitOperatorMethodName()
        {
            var m = typeof(TestClass).GetMethod("op_Implicit");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.op_Implicit(NADS.TestDoc.TestClass)~System.Single", name);
        }

        [Test]
        public void TestGenerateOverrideMethodName()
        {
            var m = typeof(TestClass).GetMethod("Equals");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.Equals(System.Object)", name);
        }

        [Test]
        public void TestGenerateVirtualMethodName()
        {
            var m = typeof(TestClass).GetMethod("VirtualMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.VirtualMethod(System.Int32)", name);
        }

        [Test]
        public void TestGenerateNoParamMethodName()
        {
            var m = typeof(TestClass).GetMethod("NoParamMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.NoParamMethod", name);
        }

        [Test]
        public void TestGenerateNullableMethodName()
        {
            var m = typeof(TestClass).GetMethod("NullableMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.NullableMethod(System.Nullable{System.Int32})", name);
        }

        [Test]
        public void TestGenerateRefMethodName()
        {
            var m = typeof(TestClass).GetMethod("RefMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.RefMethod(System.Int32@,System.Int32@,System.Int32@)", name);
        }

        [Test]
        public void TestGenerateArrayMethodName()
        {
            var m = typeof(TestClass).GetMethod("ArrayMethod");
            string expected = "M:NADS.TestDoc.TestClass.ArrayMethod(System.Int32[],System.Int32[0:,0:],System.Int32[][])";
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual(expected, name);
        }

        [Test]
        public void TestGenerateGenericMethodName()
        {
            var m = typeof(TestClass).GetMethod("GenericMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.GenericMethod``1(``0)", name);
        }

        [Test]
        public void TestGenerateGenericClassMethodName()
        {
            var m = typeof(GenericClass<>).GetMethod("GenericMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.GenericClass`1.GenericMethod``1(`0,``0)", name);
        }

        [Test]
        public void TestGenerateGenericClassConstructedMethodName()
        {
            var m = typeof(GenericClass<>).GetMethod("ConstructedMethod");
            string expected = "M:NADS.TestDoc.GenericClass`1.ConstructedMethod``1(`0,``0,NADS.TestDoc.GenericClass{System.Int32})";
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual(expected, name);
        }

        [Test]
        public void TestGenerateGenericClassNestedMethodName()
        {
            var m = typeof(GenericClass<>.GenericNestedClass<>).GetMethod("NestedMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.GenericClass`1.GenericNestedClass`1.NestedMethod``1(`0,`1,``0)", name);
        }

        [Test]
        public void TestGenerateConversionOperatorWithGenericParamName()
        {
            var m = typeof(GenericClass<>).GetMethod("op_Explicit");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.GenericClass`1.op_Explicit(NADS.TestDoc.GenericClass{`0})~System.Int32", name);
        }

        public void TestGenerateConversionOperatorWithGenericReturnName()
        {
            var m = typeof(GenericClass<>).GetMethod("op_Implicit");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.GenericClass`1.op_Implicit(System.Int32)~NADS.TestDoc.GenericClass{`0}", name);
        }

        [Test]
        public void TestGenerateTheMonsterMethodName()
        {
            // hold onto your butts !!
            string expected =
                "M:NADS.TestDoc.GenericClass`1.GenericNestedClass`1.TheMonsterMethod``1(`0,`1,``0,NADS.TestDoc.GenericClass{System.Int32}.GenericNestedClass{``0}[][]@,``0[][]@,`1[0:,0:,0:]@)";

            var m = typeof(GenericClass<>.GenericNestedClass<>).GetMethod("TheMonsterMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual(expected, name);
        }

        [Test]
        public void TestGeneratePointerMethodName()
        {
            var m = typeof(UnsafeStruct).GetMethod("PointerMethod");
            string name = generator.GenerateMethodName(m);
            Assert.AreEqual("M:NADS.TestDoc.UnsafeStruct.PointerMethod(System.Int32*,System.Single*)", name);
        }

        [Test]
        public void TestGenerateConstructorName()
        {
            var c = typeof(TestClass).GetConstructor(new Type[] { typeof(int) });
            string name = generator.GenerateMethodName(c);
            Assert.AreEqual("M:NADS.TestDoc.TestClass.#ctor(System.Int32)", name);
        }

        [Test]
        public void TestGenerateGenericConstructorName()
        {
            Type genericType = typeof(GenericClass<>);
            Type typeParam = genericType.GetGenericArguments()[0];

            var c = typeof(GenericClass<>).GetConstructor(new Type[] { typeParam, typeof(GenericClass<int>) });
            string name = generator.GenerateMethodName(c);
            Assert.AreEqual("M:NADS.TestDoc.GenericClass`1.#ctor(`0,NADS.TestDoc.GenericClass{System.Int32})", name);
        }

        [Test]
        public void TestGenerateEventName()
        {
            var e = typeof(TestClass).GetEvent("ActionEvent");
            string name = generator.GenerateEventName(e);
            Assert.AreEqual("E:NADS.TestDoc.TestClass.ActionEvent", name);
        }

        [Test]
        public void TestGenerateEventWithTypeParamsName()
        {
            var e = typeof(TestClass).GetEvent("IntActionEvent");
            string name = generator.GenerateEventName(e);
            Assert.AreEqual("E:NADS.TestDoc.TestClass.IntActionEvent", name);
        }

        [Test]
        public void TestGenerateGenericEventName()
        {
            var e = typeof(GenericClass<>).GetEvent("GenericEvent");
            string name = generator.GenerateEventName(e);
            Assert.AreEqual("E:NADS.TestDoc.GenericClass`1.GenericEvent", name);
        }

        [Test]
        public void TestGenerateConstructedGenericEventName()
        {
            var e = typeof(GenericClass<>).GetEvent("ConstructedEvent");
            string name = generator.GenerateEventName(e);
            Assert.AreEqual("E:NADS.TestDoc.GenericClass`1.ConstructedEvent", name);
        }
    }
}