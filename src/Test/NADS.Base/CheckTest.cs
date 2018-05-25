using System;
using System.Collections.Generic;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace NADS
{
    [TestFixture]
    public class CheckTest
    {
        class Outer { };
        class Inner : Outer { };

        [Test]
        public void TestCond()
        {
            Throws<Exception>(() => Check.Cond(false, ""));
            DoesNotThrow(() => Check.Cond(true, ""));
        }

        [Test]
        public void TestRef()
        {
            Throws<ArgumentNullException>(() => Check.Ref(null));
            Throws<ArgumentNullException>(() => Check.Ref(new object[] { null, null }));

            DoesNotThrow(() => Check.Ref(666));
        }

        [Test]
        public void TestType()
        {
            int i = 9;
            DoesNotThrow(() => Check.CanCastTo<int>(i));
            Throws<InvalidCastException>(() => Check.CanCastTo<double>(i));
        }

        [Test]
        public void TestTypeCovariant()
        {
            var inner = new Inner();
            DoesNotThrow(() => Check.CanCastTo<Outer>(inner));
        }

        [Test]
        public void TestTypeContravariant()
        {
            IEnumerable<double> d = new double[0];
            DoesNotThrow(() => Check.CanCastTo<double[]>((IEnumerable<double>)(d)));

            var outer = new Outer();
            Throws<InvalidCastException>(() => Check.CanCastTo<Inner>(outer));
        }

        [Test]
        public void TestIndex()
        {
            Throws<IndexOutOfRangeException>(() => Check.Index(-5, 5));
            Throws<IndexOutOfRangeException>(() => Check.Index(666, 5));
            Throws<IndexOutOfRangeException>(() => Check.Index(666, 666));

            DoesNotThrow(() => Check.Index(5, 666));
            DoesNotThrow(() => Check.Index(-666, 5, true));
        }

        [Test]
        public void TestCount()
        {
            Throws<ArgumentOutOfRangeException>(() => Check.Count(-5, 5));
            Throws<ArgumentOutOfRangeException>(() => Check.Count(666, 5));

            DoesNotThrow(() => Check.Count(5, 666));
            DoesNotThrow(() => Check.Count(666, 666));
        }

        [Test]
        public void TestSign()
        {
            Throws<ArgumentOutOfRangeException>(() => Check.Sign(-666));
            DoesNotThrow(() => Check.Sign(0));
            DoesNotThrow(() => Check.Sign(666));

            Throws<ArgumentOutOfRangeException>(() => Check.Sign(-6.66));
            DoesNotThrow(() => Check.Sign(6.66));
        }
    };
}