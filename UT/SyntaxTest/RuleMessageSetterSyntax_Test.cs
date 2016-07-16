using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using Xunit;
using static UnitTest.Validation_Test;

namespace UnitTest.SyntaxTest
{
    public class RuleMessageSetterSyntax_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_Syntax_OverrideName()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => i == 18).OverrideName("18 years");
            var v = builder.Build();
            var student = new Student() { Age = 18 };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 19 };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(19, result.Failures[0].Value);
            Assert.Equal("18 years", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_OverrideError()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => i == 18).OverrideError("18 years");
            var v = builder.Build();
            var student = new Student() { Age = 18 };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 19 };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(19, result.Failures[0].Value);
            Assert.Equal("Age", result.Failures[0].Name);
            Assert.Equal("18 years", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_When()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => i == 18).When(i => i > 13);
            var v = builder.Build();
            var student = new Student() { Age = 18 };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 12 };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 19 };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(19, result.Failures[0].Value);
            Assert.Equal("Age", result.Failures[0].Name);
            Assert.Equal(null, result.Failures[0].Error);
        }
    }
}