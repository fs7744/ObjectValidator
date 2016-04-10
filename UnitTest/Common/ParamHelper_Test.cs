using NUnit.Framework;
using ObjectValidator.Common;
using System;

namespace UnitTest.Common
{
    [TestFixture]
    public class ParamHelper_Test
    {
        [Test]
        public void Test_CheckParamEmptyOrNull()
        {
            Assert.DoesNotThrow(() => ParamHelper.CheckParamEmptyOrNull("param", "test0", "info0"));

            var ex = Assert.Throws<ArgumentException>(() => ParamHelper.CheckParamEmptyOrNull(null, "test", "info"));
            Assert.NotNull(ex);
            Assert.AreEqual("test", ex.ParamName);
            Assert.True(ex.Message.Contains("info"));
            Assert.True(ex.Message.Contains("test"));

            ex = Assert.Throws<ArgumentException>(() => ParamHelper.CheckParamEmptyOrNull(string.Empty, "test2", "info2"));
            Assert.NotNull(ex);
            Assert.True(ex.Message.Contains("info2"));
            Assert.True(ex.Message.Contains("test2"));

            ex = Assert.Throws<ArgumentException>(() => ParamHelper.CheckParamEmptyOrNull("", "test3", "info3"));
            Assert.NotNull(ex);
            Assert.AreEqual("test3", ex.ParamName);
            Assert.True(ex.Message.Contains("info3"));
            Assert.True(ex.Message.Contains("test3"));
        }

        [Test]
        public void Test_CheckParamNull()
        {
            Assert.DoesNotThrow(() => ParamHelper.CheckParamNull("", "test3", "info3"));
            Assert.DoesNotThrow(() => ParamHelper.CheckParamNull(string.Empty, "test3", "info3"));
            Assert.DoesNotThrow(() => ParamHelper.CheckParamNull(1, "test3", "info3"));
            Assert.DoesNotThrow(() => ParamHelper.CheckParamNull(1d, "test3", "info3"));
            Assert.DoesNotThrow(() => ParamHelper.CheckParamNull(1m, "test3", "info3"));
            Assert.DoesNotThrow(() => ParamHelper.CheckParamNull(new ParamHelper(), "test3", "info3"));

            var ex = Assert.Throws<ArgumentNullException>(() => ParamHelper.CheckParamNull(null, "test", "info"));
            Assert.NotNull(ex);
            Assert.AreEqual("test", ex.ParamName);
            Assert.True(ex.Message.Contains("info"));
            Assert.True(ex.Message.Contains("test"));
        }
    }
}