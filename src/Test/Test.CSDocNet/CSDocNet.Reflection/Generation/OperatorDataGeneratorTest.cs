using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NSubstitute;
using CSDocNet.Reflection.Data;
using CSDocNet.TestDoc;

namespace CSDocNet.Reflection.Generation
{
    [TestFixture]
    public class OperatorDataGeneratorTest
    {
        OperatorDataGenerator gen;
        IMethodDataGenerator methodGen;

        [SetUp]
        public void SetUp()
        {

            methodGen = Substitute.For<IMethodDataGenerator>();
            gen = new OperatorDataGenerator(methodGen);
        }

        [Test]
        public void TestGenerateOperatorData()
        {
            MethodInfo op = typeof(TestClass).GetMethod("op_Subtraction");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[]
            { new MemberRef("STAThreadAttribute", MemberRefType.Class, typeof(STAThreadAttribute).MetadataToken) };
            methodGen.GenerateName(op).Returns("CSDocNet.TestDoc.TestClass.op_Subtraction");
            methodGen.GenerateAttributes(op).Returns(expectedAttr);

            MemberData member = gen.GenerateMemberData(op);
            MethodData method = methodGen.GenerateMethodData(op);

            OperatorData opData = gen.GenerateOperatorData(op);
            Assert.AreEqual(member, opData.Member);
            Assert.AreEqual(method, opData.Method);
            Assert.AreEqual(Operator.Subtraction, opData.Operator);
        }

        [Test]
        public void TestGenerateOperatorDataThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateOperatorData(null));
        }

        [Test]
        public void TestGenerateMemberData()
        {
            MethodInfo opMethod = typeof(TestClass).GetMethod("op_Addition");
            gen.GenerateMemberData(opMethod);
            methodGen.Received().GenerateMemberData(opMethod);
        }

        [Test]
        public void TestGenerateMemberDataThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateMemberData(null));
        }

        [Test]
        public void TestGenerateName()
        {
            MethodInfo opMethod = typeof(TestClass).GetMethod("op_Addition");
            gen.GenerateName(opMethod);
            methodGen.Received().GenerateName(opMethod);
        }

        [Test]
        public void TestGenerateNameThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateName(null));
        }

        [Test]
        public void TestGenerateCommentID()
        {
            MethodInfo opMethod = typeof(TestClass).GetMethod("op_Addition");
            gen.GenerateCommentID(opMethod);
            methodGen.Received().GenerateCommentID(opMethod);
        }

        [Test]
        public void TestGenerateCommentIDThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateCommentID(null));
        }

        [Test]
        public void TestGenerateAccess()
        {
            MethodInfo opMethod = typeof(TestClass).GetMethod("op_Addition");
            gen.GenerateAccess(opMethod);
            methodGen.Received().GenerateAccess(opMethod);
        }

        [Test]
        public void TestGenerateAccessThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAccess(null));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            MethodInfo opMethod = typeof(TestClass).GetMethod("op_Addition");
            gen.GenerateModifiers(opMethod);
            methodGen.Received().GenerateModifiers(opMethod);
        }

        [Test]
        public void TestGenerateModifiersThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateModifiers(null));
        }

        [Test]
        public void TestGenerateAttributes()
        {
            MethodInfo opMethod = typeof(TestClass).GetMethod("op_Addition");
            gen.GenerateAttributes(opMethod);
            methodGen.Received().GenerateAttributes(opMethod);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
        }

        [Test]
        public void TestGenerateOperator()
        {
            var opMethods =
                from MethodInfo m in typeof(TestClass).GetMethods()
                where m.Name.StartsWith("op_")
                select m;
            
            foreach(MethodInfo opMethod in opMethods)
            {
                string opName = opMethod.Name.Substring(3);
                Assert.IsTrue(Enum.TryParse(opName, out Operator op));
                Assert.AreEqual(op, gen.GenerateOperator(opMethod));
            }
        }

        [Test]
        public void TestGenerateOperatorThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateOperator(null));
        }

        [Test]
        public void TestGenerateOperatorThrowsOnNonOperator()
        {
            MethodInfo method = typeof(TestClass).GetMethod("IntMethod");
            Assert.Throws<Exception>(() => gen.GenerateOperator(method));
        }
    }
}