using NUnit.Framework;
using ObjectValidator.Common;
using System.Collections.Generic;

namespace UnitTest.Common
{
    [TestFixture]
    public class EnumerableHelper_Test
    {
        [Test]
        public void Test_IsEmptyOrNull()
        {
            Assert.AreEqual(true, EnumerableHelper.IsEmptyOrNull(null));
            Assert.AreEqual(true, EnumerableHelper.IsEmptyOrNull(new List<string>()));
            Assert.AreEqual(false, EnumerableHelper.IsEmptyOrNull(new List<string>() { "1" }));
            Assert.AreEqual(false, EnumerableHelper.IsEmptyOrNull(new string[1]));
        }
    }
}