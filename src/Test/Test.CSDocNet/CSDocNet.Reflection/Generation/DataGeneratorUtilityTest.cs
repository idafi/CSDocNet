using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using NSubstitute;
using CSDocNet.Collections;
using CSDocNet.TestDoc;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    [TestFixture]
    public class DataGeneratorUtilityTest
    {
        DataGeneratorUtility utility;
        INameGenerator nameGen;

        [SetUp]
        public void SetUp()
        {
            nameGen = Substitute.For<INameGenerator>();
            utility = new DataGeneratorUtility(nameGen);
        }

        [Test]
        public void TestGenerateAttributes()
        {
            var attributes = utility.GenerateAttributes(typeof(TestClass));
            Assert.AreEqual(2, attributes.Count);
            AssertMemberRef(attributes[0], MemberRefType.Class, typeof(SerializableAttribute).MetadataToken);
            AssertMemberRef(attributes[1], MemberRefType.Class, typeof(DefaultMemberAttribute).MetadataToken);
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
            Type t = typeof(TestClass);
            MemberRef mRef = utility.MakeMemberRef(t);
            AssertMemberRef(mRef, MemberRefType.Class, t.MetadataToken);
        }

        [Test]
        public void TestMakeNestedClassRef()
        {
            Type t = typeof(TestClass.NestedClass);
            MemberRef mRef = utility.MakeMemberRef(typeof(TestClass.NestedClass));
            AssertMemberRef(mRef, MemberRefType.Class, t.MetadataToken);
        }

        [Test]
        public void TestMakeGenericClassRef()
        {
            Type t = typeof(GenericClass<>);
            MemberRef mRef = utility.MakeMemberRef(t);
            AssertMemberRef(mRef, MemberRefType.Class, t.MetadataToken,
                typeParams: new MemberRef[] { new MemberRef(MemberRefType.TypeParam, 0) });
        }

        [Test]
        public void TestMakeGenericNestedClassRef()
        {
            Type t = typeof(GenericClass<>.GenericNestedClass<>);
            MemberRef mRef = utility.MakeMemberRef(t);
            AssertMemberRef(mRef, MemberRefType.Class, t.MetadataToken,
                typeParams: new MemberRef[]
                {
                    new MemberRef(MemberRefType.TypeParam, 0),
                    new MemberRef(MemberRefType.TypeParam, 1)
                }
            );
        }

        [Test]
        public void TestMakeStructRef()
        {
            Type t = typeof(TestStruct);
            MemberRef mRef = utility.MakeMemberRef(t);
            AssertMemberRef(mRef, MemberRefType.Struct, t.MetadataToken);
        }

        [Test]
        public void TestMakeGenericStructRef()
        {
            Type t = typeof(GenericStruct<>);
            MemberRef mRef = utility.MakeMemberRef(t);
            AssertMemberRef(mRef, MemberRefType.Struct, t.MetadataToken,
                typeParams: new MemberRef[] { new MemberRef(MemberRefType.TypeParam, 0)});
        }

        [Test]
        public void TestMakeEnumRef()
        {
            Type t = typeof(TestEnum);
            MemberRef mRef = utility.MakeMemberRef(t);
            AssertMemberRef(mRef, MemberRefType.Enum, t.MetadataToken);
        }

        [Test]
        public void TestMakeInterfaceRef()
        {
            Type t = typeof(TestInterface);
            MemberRef mRef = utility.MakeMemberRef(t);
            AssertMemberRef(mRef, MemberRefType.Interface, t.MetadataToken);
        }

        [Test]
        public void TestMakeGenericInterfaceRef()
        {
            Type t = typeof(GenericInterface<,,>);
            MemberRef mRef = utility.MakeMemberRef(t);
            AssertMemberRef(mRef, MemberRefType.Interface, t.MetadataToken,
                typeParams: new MemberRef[]
                {
                    new MemberRef(MemberRefType.TypeParam, 0),
                    new MemberRef(MemberRefType.TypeParam, 1),
                    new MemberRef(MemberRefType.TypeParam, 2),
                }
            );
        }

        [Test]
        public void TestMakeDelegateRef()
        {
            Type t = typeof(TestDoc.TestDelegate);
            MemberRef mRef = utility.MakeMemberRef(t);
            AssertMemberRef(mRef, MemberRefType.Delegate, t.MetadataToken);
        }

        [Test]
        public void TestMakeGenericDelegateRef()
        {
            Type t = typeof(GenericDelegate<>);
            MemberRef mRef = utility.MakeMemberRef(t);
            AssertMemberRef(mRef, MemberRefType.Delegate, t.MetadataToken,
                typeParams: new MemberRef[] { new MemberRef(MemberRefType.TypeParam, 0)});
        }

        [Test]
        public void TestMakeMultiArrayTypeRef()
        {
            Type t = typeof(TestClass);
            MemberRef mRef = utility.MakeMemberRef(t.GetField("MultiArrayField").FieldType);
            AssertMemberRef(mRef, MemberRefType.Struct, typeof(int).MetadataToken, new int[] { 1, 3, 2 });
        }

        [Test]
        public void TestMakeByRefTypeRef()
        {
            Type t = typeof(TestStruct);
            MethodInfo method = t.GetMethod("Method");
            MemberRef returnRef = utility.MakeMemberRef(method.ReturnType);
            MemberRef paramRef = utility.MakeMemberRef(method.GetParameters()[0].ParameterType);

            AssertMemberRef(returnRef, MemberRefType.Struct, typeof(int).MetadataToken);
            AssertMemberRef(paramRef, MemberRefType.Struct, t.MetadataToken);
        }
        
        [Test]
        public void TestMakeTypeParamRef()
        {
            Type t = typeof(GenericClass<>);
            Type arg = t.GetGenericArguments()[0];

            MemberRef mRef = utility.MakeMemberRef(arg);
            AssertMemberRef(mRef, MemberRefType.TypeParam, 0);        
        }

        [Test]
        public void TestMakeMethodTypeParamRef()
        {
            MethodInfo method = typeof(GenericClass<>).GetMethod("GenericMethod");
            Type arg = method.GetGenericArguments()[0];

            MemberRef mRef = utility.MakeMemberRef(arg);
            AssertMemberRef(mRef, MemberRefType.MethodTypeParam, 0);
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
            var wExpected = new TypeConstraint[] { TypeConstraint.Type(new MemberRef(MemberRefType.Class, typeof(TestClass).MetadataToken)) };

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

        [Test]
        public void TestIsReadOnlyField()
        {
            FieldInfo field = typeof(TestClass).GetField("IntField");
            Assert.AreEqual(true, utility.IsReadOnly(field));
        }

        [Test]
        public void TestIsReadOnlyType()
        {
            Type type = typeof(TestStruct);
            Assert.AreEqual(true, utility.IsReadOnly(type));
        }

        [Test]
        public void TestIsReadOnlyReturnValue()
        {
            ParameterInfo val = typeof(TestStruct).GetMethod("Method").ReturnParameter;
            Assert.AreEqual(true, utility.IsReadOnly(val));
        }

        [Test]
        public void TestIsNotReadOnlyField()
        {
            FieldInfo field = typeof(TestClass).GetField("MutableField");
            Assert.AreEqual(false, utility.IsReadOnly(field));
        }

        [Test]
        public void TestIsNotReadOnlyType()
        {
            Type type = typeof(UnsafeStruct);
            Assert.AreEqual(false, utility.IsReadOnly(type));
        }

        [Test]
        public void TestIsNotReadOnlyReturnValue()
        {
            ParameterInfo val = typeof(TestClass).GetMethod("IntMethod").ReturnParameter;
            Assert.AreEqual(false, utility.IsReadOnly(val));
        }

        [Test]
        public void TestIsReadOnlyThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => utility.IsReadOnly(null));
        }

        void AssertMemberRef(MemberRef mRef, MemberRefType type, int id,
            IReadOnlyList<int> arrayDim = null, IReadOnlyList<MemberRef> typeParams = null)
        {
            Assert.AreEqual(mRef.Type, type);
            Assert.AreEqual(mRef.ID, id);
            Assert.That(mRef.ArrayDimensions, Is.EquivalentTo(arrayDim ?? Empty<int>.List));
            
            Assert.AreEqual(typeParams?.Count ?? 0, mRef.TypeParams.Count);
            for(int i = 0; i < mRef.TypeParams.Count; i++)
            { AssertMemberRef(mRef.TypeParams[i], typeParams[i].Type, typeParams[i].ID); }
        }

        void AssertTypeConstraints(IReadOnlyList<TypeConstraint> expected, IReadOnlyList<TypeConstraint> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for(int i = 0; i < actual.Count; i++)
            { AssertTypeConstraint(expected[i], actual[i]); }
        }

        void AssertTypeConstraint(TypeConstraint expected, TypeConstraint actual)
        {
            Assert.AreEqual(expected.Constraint, actual.Constraint);

            if(expected.Constraint == ConstraintType.Type)
            { AssertMemberRef(expected.ConstrainedType, actual.ConstrainedType.Type, actual.ConstrainedType.ID); }
            else if(expected.Constraint == ConstraintType.TypeParam)
            { Assert.AreEqual(expected.ConstrainedTypeParamPosition, actual.ConstrainedTypeParamPosition); }
        }
    }
}