using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using NADS.Collections;
using NADS.TestDoc;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    [TestFixture]
    public class DocGeneratorUtilityTest
    {
        DocGeneratorUtility utility;

        [SetUp]
        public void SetUp()
        {
            utility = new DocGeneratorUtility();
        }

        [Test]
        public void TestGenerateTypeName()
        {
            Assert.AreEqual("NADS.TestDoc.TestClass", utility.GenerateName(typeof(TestClass)));
        }

        [Test]
        public void TestGenerateNestedTypeName()
        {
            Assert.AreEqual("NADS.TestDoc.TestClass+NestedClass", utility.GenerateName(typeof(TestClass.NestedClass)));
        }

        [Test]
        public void TestGenerateGenericTypeName()
        {
            Assert.AreEqual("NADS.TestDoc.GenericClass`1", utility.GenerateName(typeof(GenericClass<>)));   
        }

        [Test]
        public void TestGenerateEventName()
        {
            EventInfo ev = typeof(TestClass).GetEvent("ActionEvent");
            Assert.AreEqual("NADS.TestDoc.TestClass.ActionEvent", utility.GenerateName(ev));
        }

        [Test]
        public void TestGenerateFieldName()
        {
            FieldInfo field = typeof(TestClass).GetField("IntField");
            Assert.AreEqual("NADS.TestDoc.TestClass.IntField", utility.GenerateName(field));
        }

        [Test]
        public void TestGenerateNestedFieldName()
        {
            FieldInfo field = typeof(TestClass.NestedClass).GetField("NestedField");
            Assert.AreEqual("NADS.TestDoc.TestClass+NestedClass.NestedField", utility.GenerateName(field));
        }

        [Test]
        public void TestGenerateGenericFieldName()
        {
            FieldInfo field = typeof(GenericClass<>).GetField("GenericField");
            Assert.AreEqual("NADS.TestDoc.GenericClass`1.GenericField", utility.GenerateName(field));
        }

        [Test]
        public void TestGeneratePropertyName()
        {
            PropertyInfo property = typeof(TestClass).GetProperty("IntProperty");
            Assert.AreEqual("NADS.TestDoc.TestClass.IntProperty", utility.GenerateName(property));
        }

        [Test]
        public void TestGenerateNestedPropertyName()
        {
            PropertyInfo property = typeof(TestClass.NestedClass).GetProperty("NestedProperty");
            Assert.AreEqual("NADS.TestDoc.TestClass+NestedClass.NestedProperty", utility.GenerateName(property));
        }

        [Test]
        public void TestGenerateGenericPropertyName()
        {
            PropertyInfo property = typeof(GenericClass<>).GetProperty("GenericProperty");
            Assert.AreEqual("NADS.TestDoc.GenericClass`1.GenericProperty", utility.GenerateName(property));
        }

        [Test]
        public void TestGenerateConstructorName()
        {
            ConstructorInfo ctor = typeof(TestClass).GetConstructor(new Type[] { typeof(int) });
            Assert.AreEqual("NADS.TestDoc.TestClass..ctor", utility.GenerateName(ctor));
        }

        [Test]
        public void TestGenerateMethodName()
        {
            MethodInfo method = typeof(TestClass).GetMethod("IntMethod");
            Assert.AreEqual("NADS.TestDoc.TestClass.IntMethod", utility.GenerateName(method));
        }

        [Test]
        public void TestGenerateNestedMethodName()
        {
            MethodInfo method = typeof(TestClass.NestedClass).GetMethod("NestedMethod");
            Assert.AreEqual("NADS.TestDoc.TestClass+NestedClass.NestedMethod", utility.GenerateName(method));
        }

        [Test]
        public void TestGenerateGenericMethodName()
        {
            MethodInfo method = typeof(GenericClass<>).GetMethod("GenericMethod");
            Assert.AreEqual("NADS.TestDoc.GenericClass`1.GenericMethod", utility.GenerateName(method));
        }

        [Test]
        public void TestGenerateAttributes()
        {
            var attributes = utility.GenerateAttributes(typeof(TestClass));
            Assert.AreEqual(2, attributes.Count);
            AssertMemberRef(attributes[0], MemberRefType.Class, "System.SerializableAttribute");
            AssertMemberRef(attributes[1], MemberRefType.Class, "System.Reflection.DefaultMemberAttribute");
            // "what?" classes with indexers get ^this second attribute
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => utility.GenerateAttributes(null));
        }

        [Test]
        public void TestGetRootElementType()
        {
            Type type = typeof(TestClass).GetField("MultiArrayField").FieldType;
            Assert.AreEqual(typeof(int), utility.GetRootElementType(type));
        }

        [Test]
        public void TestGetRootElementTypeOfByref()
        {
            Type type = typeof(TestStruct).GetMethod("Method").ReturnType;
            Assert.AreEqual(typeof(int), utility.GetRootElementType(type));
        }

        [Test]
        public void TestMakeClassRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(TestClass));
            AssertMemberRef(mRef, MemberRefType.Class, "NADS.TestDoc.TestClass");
        }

        [Test]
        public void TestMakeNestedClassRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(TestClass.NestedClass));
            AssertMemberRef(mRef, MemberRefType.Class, "NADS.TestDoc.TestClass+NestedClass");
        }

        [Test]
        public void TestMakeGenericClassRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(GenericClass<>));
            AssertMemberRef(mRef, MemberRefType.Class, "NADS.TestDoc.GenericClass`1");
        }

        [Test]
        public void TestMakeGenericNestedClassRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(GenericClass<>.GenericNestedClass<>));
            AssertMemberRef(mRef, MemberRefType.Class, "NADS.TestDoc.GenericClass`1+GenericNestedClass`1");
        }

        [Test]
        public void TestMakeStructRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(TestStruct));
            AssertMemberRef(mRef, MemberRefType.Struct, "NADS.TestDoc.TestStruct");
        }

        [Test]
        public void TestMakeGenericStructRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(GenericStruct<>));
            AssertMemberRef(mRef, MemberRefType.Struct, "NADS.TestDoc.GenericStruct`1");
        }

        [Test]
        public void TestMakeEnumRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(TestEnum));
            AssertMemberRef(mRef, MemberRefType.Enum, "NADS.TestDoc.TestEnum");
        }

        [Test]
        public void TestMakeInterfaceRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(TestInterface));
            AssertMemberRef(mRef, MemberRefType.Interface, "NADS.TestDoc.TestInterface");
        }

        [Test]
        public void TestMakeGenericInterfaceRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(GenericInterface<,,>));
            AssertMemberRef(mRef, MemberRefType.Interface, "NADS.TestDoc.GenericInterface`3");
        }

        [Test]
        public void TestMakeDelegateRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(TestDoc.TestDelegate));
            AssertMemberRef(mRef, MemberRefType.Delegate, "NADS.TestDoc.TestDelegate");
        }

        [Test]
        public void TestMakeGenericDelegateRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(GenericDelegate<>));
            AssertMemberRef(mRef, MemberRefType.Delegate, "NADS.TestDoc.GenericDelegate`1");
        }

        [Test]
        public void TestMakeMultiArrayTypeRef()
        {
            MemberRef mRef = utility.MakeMemberRef(typeof(TestClass).GetField("MultiArrayField").FieldType);
            AssertMemberRef(mRef, MemberRefType.Struct, "System.Int32", new int[] { 1, 3, 2 });
        }

        [Test]
        public void TestMakeByRefTypeRef()
        {
            MethodInfo method = typeof(TestStruct).GetMethod("Method");
            MemberRef returnRef = utility.MakeMemberRef(method.ReturnType);
            MemberRef paramRef = utility.MakeMemberRef(method.GetParameters()[0].ParameterType);

            AssertMemberRef(returnRef, MemberRefType.Struct, "System.Int32");
            AssertMemberRef(paramRef, MemberRefType.Struct, "NADS.TestDoc.TestStruct");
        }

        [Test]
        public void TestMakeMemberRefThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => utility.MakeMemberRef(null));
        }

        [Test]
        public void TestGetGenericParamModifier()
        {
            Type i = typeof(GenericInterface<,,>);
            var typeParams = i.GetGenericArguments();

            Type inParam = typeParams[0];
            Type outParam = typeParams[1];
            Type normalParam = typeParams[2];

            Assert.AreEqual(ParamModifier.In, utility.GetGenericParamModifier(inParam.GenericParameterAttributes));
            Assert.AreEqual(ParamModifier.Out, utility.GetGenericParamModifier(outParam.GenericParameterAttributes));
            Assert.AreEqual(ParamModifier.None, utility.GetGenericParamModifier(normalParam.GenericParameterAttributes));
        }

        [Test]
        public void TestGetTypeParamConstraints()
        {
            Type gcClass = typeof(GenericConstrainedClass<,,,>);
            Type[] typeparams = gcClass.GetGenericArguments();

            Type tParam = typeparams[0];
            Type uParam = typeparams[1];
            Type vParam = typeparams[2];
            Type wParam = typeparams[3];

            var tExpected = new TypeConstraint[] { TypeConstraint.Struct };
            var uExpected = new TypeConstraint[] { TypeConstraint.Class, TypeConstraint.Ctor };
            var vExpected = new TypeConstraint[] { TypeConstraint.TypeParam(3) };
            var wExpected = new TypeConstraint[] { TypeConstraint.Type(new MemberRef(MemberRefType.Class, "NADS.TestDoc.TestClass")) };

            AssertTypeConstraints(tExpected, utility.GetTypeParamConstraints(tParam));
            AssertTypeConstraints(uExpected, utility.GetTypeParamConstraints(uParam));
            AssertTypeConstraints(vExpected, utility.GetTypeParamConstraints(vParam));
            AssertTypeConstraints(wExpected, utility.GetTypeParamConstraints(wParam));
        }

        [Test]
        public void TestGetTypeParamConstraintsThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => utility.GetTypeParamConstraints(null));
        }

        [Test]
        public void TestGetTypeParamConstraintsThrowsOnNonGenericType()
        {
            Assert.Throws<Exception>(() => utility.GetTypeParamConstraints(typeof(int)));
        }

        void AssertMemberRef(in MemberRef mRef, MemberRefType type, string name, IReadOnlyList<int> arrayDim = null)
        {
            Assert.AreEqual(mRef.Type, type);
            Assert.AreEqual(mRef.Name, name);
            Assert.That(mRef.ArrayDimensions, Is.EquivalentTo(arrayDim ?? Empty<int>.List));
        }

        void AssertTypeConstraints(IReadOnlyList<TypeConstraint> expected, IReadOnlyList<TypeConstraint> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for(int i = 0; i < actual.Count; i++)
            { AssertTypeConstraint(expected[i], actual[i]); }
        }

        void AssertTypeConstraint(in TypeConstraint expected, in TypeConstraint actual)
        {
            Assert.AreEqual(expected.Constraint, actual.Constraint);

            if(expected.Constraint == ConstraintType.Type)
            { AssertMemberRef(expected.ConstrainedType, actual.ConstrainedType.Type, actual.ConstrainedType.Name); }
            else if(expected.Constraint == ConstraintType.TypeParam)
            { Assert.AreEqual(expected.ConstrainedTypeParamPosition, actual.ConstrainedTypeParamPosition); }
        }
    }
}