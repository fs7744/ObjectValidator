using ObjectValidator.Entities;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.Entities
{
    public class ValidateResult_Test
    {
        [Fact]
        public void Test_IsVaild()
        {
            Assert.Equal(true, new ValidateResult(null).IsValid);
            Assert.Equal(true, new ValidateResult(new List<ValidateFailure>()).IsValid);
            var data = new List<ValidateFailure>() { new ValidateFailure() };
            Assert.Equal(false, new ValidateResult(data).IsValid);
            Assert.Equal(data, new ValidateResult(data).Failures);
            var r = new ValidateResult();
            r.Merge(data);
            Assert.Equal(false, r.IsValid);
            Assert.Equal(data, r.Failures);
        }
    }
}