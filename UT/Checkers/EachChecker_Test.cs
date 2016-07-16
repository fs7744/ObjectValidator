using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Checkers;
using System;
using System.Collections.Generic;
using Xunit;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    public class EachChecker_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_Validate_IntList()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleSet("A", b =>
            {
                b.RuleFor(i => i.IntList).Each()
                    .Must(i => i >= 0 && i <= 18)
                    .OverrideError("not student");
            });
            var v = builder.Build();

            var student = new Student() { Age = 13, Name = "v", IntList = new List<int> { 0, 2, 4 } };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.True(result.Failures.Count == 0);

            student = new Student() { Age = 13, Name = "v", IntList = new List<int> { 0, 2, 4, 23 } };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Failures.Count == 1);
        }

        [Fact]
        public async void Test_Validate_List()
        {
            var builder = _Validation.NewValidatorBuilder<ADStudent>();
            builder.RuleSet("A", b =>
            {
                b.RuleFor(i => i.StudentList).Each().NotNull()
                    .ThenRuleFor(i => i.IntList).Each()
                        .Must(i => i >= 0 && i <= 18)
                        .OverrideError("not student");
            });
            var v = builder.Build();

            var student = new ADStudent()
            {
                StudentList = new List<Student>()
                {
                    new Student()
                    {
                        Age = 13,
                        Name = "v",
                        IntList = new List<int> { 0, 2, 4 }
                    }
                }
            };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.True(result.Failures.Count == 0);

            student = student = new ADStudent()
            {
                StudentList = new List<Student>()
                {
                    new Student()
                    {
                        Age = 13,
                        Name = "v",
                        IntList = new List<int> { 0, 2, 4,23 }
                    }
                }
            };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Failures.Count == 1);
        }

        [Fact]
        public async void Test_EachChecker()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new EachChecker<Student, Student>(_Validation).SetValidate(null));
            Assert.Equal("builder", ex.ParamName);
            Assert.True(ex.Message.Contains("Can't be null"));

            await Assert.ThrowsAsync<NotImplementedException>(() => new EachChecker<Student, Student>(_Validation).ValidateAsync(null, null, null, null));
        }
    }
}