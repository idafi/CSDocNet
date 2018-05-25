using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NADS.Collections
{
    [TestFixture]
    public class EmptyTest
    {
        [Test]
        public void TestEmptyEnumerable()
        {
            var empty = Empty<int>.EmptyEnumerable;
            Assert.IsNotNull(empty);
            Assert.That(empty, Is.EquivalentTo(new int[0]));
        }

        [Test]
        public void TestEmptyCollection()
        {
            var empty = Empty<int>.EmptyCollection;
            Assert.IsNotNull(empty);
            Assert.AreEqual(0, empty.Count);
            Assert.That(empty, Is.EquivalentTo(new int[0]));
        }

        [Test]
        public void TestEmptyList()
        {
            var empty = Empty<int>.EmptyList;
            Assert.IsNotNull(empty);
            Assert.AreEqual(0, empty.Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = empty[0]);
            Assert.That(empty, Is.EquivalentTo(new int[0]));
        }

        [Test]
        public void TestEmptyDict()
        {
            var empty = Empty<int, float>.EmptyDict;
            Assert.IsNotNull(empty);
            Assert.AreEqual(0, empty.Count);
        }
    }
}