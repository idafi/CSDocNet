using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CSDocNet.Collections
{
    [TestFixture]
    public class EmptyTest
    {
        [Test]
        public void TestEmptyEnumerable()
        {
            var empty = Empty<int>.Enumerable;
            Assert.IsNotNull(empty);
            Assert.That(empty, Is.EquivalentTo(new int[0]));
        }

        [Test]
        public void TestEmptyCollection()
        {
            var empty = Empty<int>.Collection;
            Assert.IsNotNull(empty);
            Assert.AreEqual(0, empty.Count);
            Assert.That(empty, Is.EquivalentTo(new int[0]));
        }

        [Test]
        public void TestEmptyList()
        {
            var empty = Empty<int>.List;
            Assert.IsNotNull(empty);
            Assert.AreEqual(0, empty.Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = empty[0]);
            Assert.That(empty, Is.EquivalentTo(new int[0]));
        }

        [Test]
        public void TestEmptyDict()
        {
            var empty = Empty<int, float>.Dict;
            Assert.IsNotNull(empty);
            Assert.AreEqual(0, empty.Count);
        }
    }
}