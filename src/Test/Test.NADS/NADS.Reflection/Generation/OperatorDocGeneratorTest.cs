using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NSubstitute;
using NADS.Reflection.Data;
using NADS.TestDoc;

namespace NADS.Reflection.Generation
{
    [TestFixture]
    public class OperatorDocGeneratorTest
    {
        OperatorDocGenerator gen;
        IMethodDocGenerator methodGen;

        [SetUp]
        public void SetUp()
        {

            methodGen = Substitute.For<IMethodDocGenerator>();
            gen = new OperatorDocGenerator(methodGen);
        }

        [Test]
        public void TestGenerateOperatorDoc()
        {
            MethodInfo op = typeof(TestClass).GetMethod("op_Subtraction");
            IReadOnlyList<MemberRef> expectedAttr = new MemberRef[] { new MemberRef(MemberRefType.Class, typeof(STAThreadAttribute).MetadataToken) };
            methodGen.GenerateName(op).Returns("NADS.TestDoc.TestClass.op_Subtraction");
            methodGen.GenerateAttributes(op).Returns(expectedAttr);

            MemberDoc member = gen.GenerateMemberDoc(op);
            MethodDoc method = methodGen.GenerateMethodDoc(op);

            OperatorDoc opDoc = gen.GenerateOperatorDoc(op);
            Assert.AreEqual(member, opDoc.Member);
            Assert.AreEqual(method, opDoc.Method);
            Assert.AreEqual(Operator.Subtraction, opDoc.Operator);
        }

        [Test]
        public void TestGenerateOperatorDocThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateOperatorDoc(null));
        }

        [Test]
        public void TestGenerateMemberDoc()
        {
            MethodInfo opMethod = typeof(TestClass).GetMethod("op_Addition");
            gen.GenerateMemberDoc(opMethod);
            methodGen.Received().GenerateMemberDoc(opMethod);
        }

        [Test]
        public void TestGenerateMemberDocThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateMemberDoc(null));
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