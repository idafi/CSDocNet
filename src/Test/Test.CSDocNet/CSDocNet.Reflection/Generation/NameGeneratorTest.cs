using System;
using System.Reflection;
using NUnit.Framework;
using CSDocNet.TestDoc;

namespace CSDocNet.Reflection.Generation
{
    [TestFixture]
    public class NameGeneratorTest
    {
        NameGenerator gen;

        [SetUp]
        public void SetUp()
        {
            gen = new NameGenerator();
        }

        [Test]
        public void TestGenerateMemberName()
        {
            Type t = typeof(TestClass);
            FieldInfo f = typeof(TestClass).GetField("IntField");
            PropertyInfo p = typeof(TestClass).GetProperty("IntProperty");
            MethodInfo m = typeof(TestClass).GetMethod("IntMethod");

            Assert.AreEqual("TestClass", gen.GenerateMemberName(t));
            Assert.AreEqual("IntField", gen.GenerateMemberName(f));
            Assert.AreEqual("IntProperty", gen.GenerateMemberName(p));
            Assert.AreEqual("IntMethod", gen.GenerateMemberName(m));
        }

        [Test]
        public void TestGenerateTypeName()
        {
            Assert.AreEqual("TestClass", gen.GenerateTypeName(typeof(TestClass)));
        }

        [Test]
        public void TestGenerateGenericTypeName()
        {
            Assert.AreEqual("GenericClass<T>", gen.GenerateTypeName(typeof(GenericClass<>)));
        }

        [Test]
        public void TestGenerateConstructedGenericTypeName()
        {
            Type t = typeof(GenericClass<Int32>);
            Assert.AreEqual("GenericClass<Int32>", gen.GenerateTypeName(t));
        }

        [Test]
        public void TestGenerateFieldName()
        {
            FieldInfo f = typeof(TestClass).GetField("IntField");
            Assert.AreEqual("IntField", gen.GenerateFieldName(f));
        }

        [Test]
        public void TestGeneratePropertyName()
        {
            PropertyInfo p = typeof(TestClass).GetProperty("IntProperty");
            Assert.AreEqual("IntProperty", gen.GeneratePropertyName(p));
        }

        [Test]
        public void TestGenerateMethodName()
        {
            MethodInfo m = typeof(TestClass).GetMethod("IntMethod");
            Assert.AreEqual("IntMethod", gen.GenerateMethodName(m));
        }

        [Test]
        public void TestGenerateGenericMethodName()
        {
            MethodInfo m = typeof(TestClass).GetMethod("GenericMethod");
            Assert.AreEqual("GenericMethod<T>", gen.GenerateMethodName(m));
        }

        [Test]
        public void TestGenerateOperatorMethodName()
        {
            MethodInfo m = typeof(TestClass).GetMethod("op_Addition");
            Assert.AreEqual("+", gen.GenerateMethodName(m));
        }

        [Test]
        public void TestGenerateOperatorName()
        {
            Assert.AreEqual("+", gen.GenerateOperatorName("UnaryPlus"));
            Assert.AreEqual("-", gen.GenerateOperatorName("UnaryNegation"));
            Assert.AreEqual("!", gen.GenerateOperatorName("LogicalNot"));
            Assert.AreEqual("~", gen.GenerateOperatorName("OnesComplement"));
            Assert.AreEqual("++", gen.GenerateOperatorName("Increment"));
            Assert.AreEqual("--", gen.GenerateOperatorName("Decrement"));
            Assert.AreEqual("true", gen.GenerateOperatorName("True"));
            Assert.AreEqual("false", gen.GenerateOperatorName("False"));
            Assert.AreEqual("+", gen.GenerateOperatorName("Addition"));
            Assert.AreEqual("-", gen.GenerateOperatorName("Subtraction"));
            Assert.AreEqual("*", gen.GenerateOperatorName("Multiply"));
            Assert.AreEqual("/", gen.GenerateOperatorName("Division"));
            Assert.AreEqual("%", gen.GenerateOperatorName("Modulus"));
            Assert.AreEqual("&", gen.GenerateOperatorName("BitwiseAnd"));
            Assert.AreEqual("|", gen.GenerateOperatorName("BitwiseOr"));
            Assert.AreEqual("^", gen.GenerateOperatorName("ExclusiveOr"));
            Assert.AreEqual("<<", gen.GenerateOperatorName("LeftShift"));
            Assert.AreEqual(">>", gen.GenerateOperatorName("RightShift"));
            Assert.AreEqual("==", gen.GenerateOperatorName("Equality"));
            Assert.AreEqual("!=", gen.GenerateOperatorName("Inequality"));
            Assert.AreEqual("<", gen.GenerateOperatorName("LessThan"));
            Assert.AreEqual(">", gen.GenerateOperatorName("GreaterThan"));
            Assert.AreEqual("<=", gen.GenerateOperatorName("LessThanOrEqual"));
            Assert.AreEqual(">=", gen.GenerateOperatorName("GreaterThanOrEqual"));
            Assert.AreEqual("Explicit", gen.GenerateOperatorName("Explicit"));
            Assert.AreEqual("Implicit", gen.GenerateOperatorName("Implicit"));
        }
    }
}