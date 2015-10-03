using NUnit.Framework;
using ObjectValidator.Entities;
using System.Collections.Generic;

namespace UnitTest.Entities
{
    [TestFixture]
    public class ValidateResult_Test
    {
        [Test]
        public void Test_IsVaild()
        {
            Assert.AreEqual(true, new ValidateResult(null).IsValid);
            Assert.AreEqual(true, new ValidateResult(new List<ValidateFailure>()).IsValid);
            var data = new List<ValidateFailure>() { new ValidateFailure() };
            Assert.AreEqual(false, new ValidateResult(data).IsValid);
            Assert.AreEqual(data, new ValidateResult(data).Failures);
            var r = new ValidateResult();
            r.Merge(data);
            Assert.AreEqual(false, r.IsValid);
            Assert.AreEqual(data, r.Failures);
        }
    }
}