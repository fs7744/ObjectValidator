using ObjectValidator.Common;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.Common
{
    public class EnumerableHelper_Test
    {
        [Fact]
        public void Test_IsEmptyOrNull()
        {
            Assert.Equal(true, EnumerableHelper.IsEmptyOrNull(null));
            Assert.Equal(true, EnumerableHelper.IsEmptyOrNull(new List<string>()));
            Assert.Equal(false, EnumerableHelper.IsEmptyOrNull(new List<string>() { "1" }));
            Assert.Equal(false, EnumerableHelper.IsEmptyOrNull(new string[1]));
        }
    }
}