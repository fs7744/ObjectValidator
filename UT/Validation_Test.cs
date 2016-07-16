using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest
{
    public class Validation_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        public class Student
        {
            public int Age { get; set; }

            public int? Age2 { get; set; }

            public string Name { get; set; }

            public List<int> IntList { get; set; }

            public DateTime Bir { get; set; }

            public float Float { get; set; }

            public double Double { get; set; }

            public decimal Decimal { get; set; }

            public long Long { get; set; }
        }

        public class ADStudent
        {
            public List<Student> StudentList { get; set; }
        }

        [Fact]
        public async void Test_Validate_Must()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleSet("A", b =>
            {
                b.RuleFor(i => i.Age)
                        .Must(i => i >= 0 && i <= 18)
                        .OverrideName("student age")
                        .OverrideError("not student")
                    .ThenRuleFor(i => i.Name)
                        .Must(i => !string.IsNullOrWhiteSpace(i))
                        .OverrideName("student name")
                        .OverrideError("no name");
            });
            var v = builder.Build();

            var student = new Student() { Age = 13, Name = "v" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.True(result.Failures.Count == 0);

            student = new Student() { Age = 23, Name = string.Empty };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Failures.Count == 1);
            Assert.Equal(23, result.Failures[0].Value);
            Assert.Equal("student age", result.Failures[0].Name);
            Assert.Equal("not student", result.Failures[0].Error);

            student = new Student() { Age = 24, Name = string.Empty };
            context = _Validation.CreateContext(student, ValidateOption.Continue);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Failures.Count == 2);
            Assert.Equal(24, result.Failures[0].Value);
            Assert.Equal("student age", result.Failures[0].Name);
            Assert.Equal("not student", result.Failures[0].Error);
            Assert.Equal(string.Empty, result.Failures[1].Value);
            Assert.Equal("student name", result.Failures[1].Name);
            Assert.Equal("no name", result.Failures[1].Error);

            student.Age = 25;
            student.Name = "v";
            builder.RuleSet("B", b =>
            {
                b.RuleFor(i => i.Age)
                    .Must(i => i <= 18)
                    .OverrideName("student age")
                    .OverrideError("is student");

                b.RuleFor(i => i.Name)
                     .Must(i => i == "vf")
                     .OverrideName("student name")
                     .OverrideError("not vf");
            });
            v = builder.Build();
            context = _Validation.CreateContext(student, ValidateOption.StopOnFirstFailure, "b");
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Failures.Count == 2);
            Assert.Equal(25, result.Failures[0].Value);
            Assert.Equal("student age", result.Failures[0].Name);
            Assert.Equal("is student", result.Failures[0].Error);
            Assert.Equal("v", result.Failures[1].Value);
            Assert.Equal("student name", result.Failures[1].Name);
            Assert.Equal("not vf", result.Failures[1].Error);
        }

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
    }
}