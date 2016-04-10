using NUnit.Framework;
using ObjectValidator;
using static UnitTest.Validation_Test;

namespace UnitTest.SyntaxTest
{
    [TestFixture]
    public class RuleMessageSetterSyntax_Test
    {
        [OneTimeSetUp]
        public void SetContainer()
        {
            Container.Init();
        }

        [OneTimeTearDown]
        public void ClearContainer()
        {
            Container.Clear();
        }

        [Test]
        public void Test_Syntax_OverrideName()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => i == 18).OverrideName("18 years");
            var v = builder.Build();
            var student = new Student() { Age = 18 };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 19 };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(19, result.Failures[0].Value);
            Assert.AreEqual("18 years", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_OverrideError()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => i == 18).OverrideError("18 years");
            var v = builder.Build();
            var student = new Student() { Age = 18 };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 19 };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(19, result.Failures[0].Value);
            Assert.AreEqual("Age", result.Failures[0].Name);
            Assert.AreEqual("18 years", result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_When()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => i == 18).When(i => i > 13);
            var v = builder.Build();
            var student = new Student() { Age = 18 };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 12 };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 19 };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(19, result.Failures[0].Value);
            Assert.AreEqual("Age", result.Failures[0].Name);
            Assert.AreEqual(null, result.Failures[0].Error);
        }
    }
}