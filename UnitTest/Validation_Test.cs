using NUnit.Framework;
using ObjectValidator;
using System.Collections.Generic;

namespace UnitTest
{
    [TestFixture]
    public class Validation_Test
    {
        [TestFixtureSetUp]
        public void SetContainer()
        {
            Container.Init();
        }

        [TestFixtureTearDown]
        public void ClearContainer()
        {
            Container.Clear();
        }

        public class Student
        {
            public int Age { get; set; }

            public string Name { get; set; }

            public List<int> IntList { get; set; }
        }

        public class ADStudent
        {
            public List<Student> StudentList { get; set; }
        }

        [Test]
        public void Test_Validate_Must()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
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
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.IsNotNull(result);
            Assert.True(result.IsValid);
            Assert.True(result.Failures.Count == 0);

            student = new Student() { Age = 23, Name = string.Empty };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.IsNotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Failures.Count == 1);
            Assert.AreEqual(23, result.Failures[0].Value);
            Assert.AreEqual("student age", result.Failures[0].Name);
            Assert.AreEqual("not student", result.Failures[0].Error);

            student = new Student() { Age = 24, Name = string.Empty };
            context = Validation.CreateContext(student, ValidateOption.Continue);
            result = v.Validate(context);
            Assert.IsNotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Failures.Count == 2);
            Assert.AreEqual(24, result.Failures[0].Value);
            Assert.AreEqual("student age", result.Failures[0].Name);
            Assert.AreEqual("not student", result.Failures[0].Error);
            Assert.AreEqual(string.Empty, result.Failures[1].Value);
            Assert.AreEqual("student name", result.Failures[1].Name);
            Assert.AreEqual("no name", result.Failures[1].Error);

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
            context = Validation.CreateContext(student, ValidateOption.StopOnFirstFailure, "b");
            result = v.Validate(context);
            Assert.IsNotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Failures.Count == 2);
            Assert.AreEqual(25, result.Failures[0].Value);
            Assert.AreEqual("student age", result.Failures[0].Name);
            Assert.AreEqual("is student", result.Failures[0].Error);
            Assert.AreEqual("v", result.Failures[1].Value);
            Assert.AreEqual("student name", result.Failures[1].Name);
            Assert.AreEqual("not vf", result.Failures[1].Error);
        }

        [Test]
        public void Test_Validate_IntList()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleSet("A", b =>
            {
                b.RuleFor(i => i.IntList).Each()
                    .Must(i => i >= 0 && i <= 18)
                    .OverrideError("not student");
            });
            var v = builder.Build();

            var student = new Student() { Age = 13, Name = "v", IntList = new List<int> { 0, 2, 4 } };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.IsNotNull(result);
            Assert.True(result.IsValid);
            Assert.True(result.Failures.Count == 0);

            student = new Student() { Age = 13, Name = "v", IntList = new List<int> { 0, 2, 4, 23 } };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.IsNotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Failures.Count == 1);
        }

        [Test]
        public void Test_Validate_List()
        {
            var builder = Validation.NewValidatorBuilder<ADStudent>();
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
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.IsNotNull(result);
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
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.IsNotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Failures.Count == 1);
        }
    }
}