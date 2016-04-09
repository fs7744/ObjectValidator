using NUnit.Framework;
using ObjectValidator.Common;
using System;
using System.Collections.Generic;

namespace UnitTest.Common
{
    [TestFixture]
    public class Comparer_Test
    {
        [Test]
        public void Test_Compare()
        {
            var result = 0;
            Assert.True(Comparer.TryCompare(1, 3, out result));
            Assert.AreEqual(-1, result);
            Assert.True(Comparer.TryCompare(3, 3, out result));
            Assert.AreEqual(0, result);
            Assert.True(Comparer.TryCompare(4, 3, out result));
            Assert.AreEqual(1, result);

            Assert.True(Comparer.TryCompare(1, 3d, out result));
            Assert.AreEqual(-1, result);
            Assert.True(Comparer.TryCompare(3, 3m, out result));
            Assert.AreEqual(0, result);
            Assert.True(Comparer.TryCompare(1f, 3d, out result));
            Assert.AreEqual(-1, result);
            Assert.True(Comparer.TryCompare(4L, 3L, out result));
            Assert.AreEqual(1, result);

            Assert.True(Comparer.TryCompare(4L, 3, out result));
            Assert.AreEqual(1, result);

            Assert.False(Comparer.TryCompare(null, null, out result));
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Test_GetEqualsResult()
        {
            Assert.True(Comparer.GetEqualsResult(1, 1));
            Assert.True(Comparer.GetEqualsResult(1, 1d));
            Assert.True(Comparer.GetEqualsResult(1m, 1d));
            Assert.True(Comparer.GetEqualsResult(1m, 1L));
            Assert.True(Comparer.GetEqualsResult(1f, 1L));
            Assert.True(Comparer.GetEqualsResult(null, null));
            Assert.True(Comparer.GetEqualsResult("a", "a"));
            Assert.False(Comparer.GetEqualsResult("a", "b"));
            Assert.False(Comparer.GetEqualsResult(1f, 2));
        }
    }
}