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
    public class EnumDataGeneratorTest
    {
        EnumDataGenerator gen;
        IDataGeneratorUtility docUtility;
        ITypeDataUtility typeUtility;
        ICommentIDGenerator idGen;

        [SetUp]
        public void SetUp()
        {
            docUtility = Substitute.For<IDataGeneratorUtility>();
            typeUtility = Substitute.For<ITypeDataUtility>();
            idGen = Substitute.For<ICommentIDGenerator>();
            gen = new EnumDataGenerator(docUtility, typeUtility, idGen);
        }

        [Test]
        public void TestGenerateName()
        {
            Type e = typeof(TestEnum);
            gen.GenerateName(e);
            docUtility.Received().GenerateName(e);
        }

        [Test]
        public void TestGenerateNameThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateName(null));
        }

        [Test]
        public void TestGenerateCommentID()
        {
            Type e = typeof(TestEnum);
            gen.GenerateCommentID(e);
            idGen.Received().GenerateMemberID(e);
        }

        [Test]
        public void TestGenerateCommentIDThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateCommentID(null));
        }

        [Test]
        public void TestGenerateAccess()
        {
            Type e = typeof(TestEnum);
            gen.GenerateAccess(e);
            typeUtility.Received().GenerateAccess(e);
        }

        [Test]
        public void TestGenerateAccessThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAccess(null));
        }

        [Test]
        public void TestGenerateModifiers()
        {
            Type e = typeof(TestEnum);
            gen.GenerateModifiers(e);
            typeUtility.Received().GenerateModifiers(e);
        }

        [Test]
        public void TestGenerateModifiersThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateModifiers(null));
        }

        [Test]
        public void TestGenerateAttributes()
        {
            Type e = typeof(TestEnum);
            gen.GenerateAttributes(e);
            docUtility.Received().GenerateAttributes(e);
        }

        [Test]
        public void TestGenerateAttributesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateAttributes(null));
        }

        [Test]
        public void TestGenerateUnderlyingType()
        {
            var expected = new MemberRef(MemberRefType.Struct, typeof(short).MetadataToken);
            docUtility.MakeMemberRef(typeof(short)).Returns(expected);
            Assert.AreEqual(expected, gen.GenerateUnderlyingType(typeof(TestEnum)));
        }

        [Test]
        public void TestGenerateUnderlyingTypeThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateUnderlyingType(null));
        }

        [Test]
        public void TestGenerateUnderlyingTypeThrowsOnNotEnum()
        {
            Assert.Throws<Exception>(() => gen.GenerateUnderlyingType(typeof(TestClass)));
        }

        [Test]
        public void TestGenerateEnumValues()
        {
            var expected = new EnumValue[] { new EnumValue("ValueA", 69), new EnumValue("ValueB", 666) };
            var actual = gen.GenerateEnumValues(typeof(TestEnum));

            Assert.AreEqual(expected.Length, actual.Count);
            for(int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].Name, actual[i].Name);
                Assert.AreEqual(expected[i].Value, actual[i].Value);
            }
        }

        [Test]
        public void TestGenerateEnumValuesThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => gen.GenerateEnumValues(null));
        }

        [Test]
        public void TestGenerateEnumValuesThrowsOnNotEnum()
        {
            Assert.Throws<Exception>(() => gen.GenerateEnumValues(typeof(TestClass)));
        }
    }
}