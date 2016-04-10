using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Checkers;
using System;
using System.Collections.Generic;
using static UnitTest.Validation_Test;

namespace UnitTest.Checkers
{
    [TestFixture]
    public class EachChecker_Test
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

        [Test]
        public void Test_EachChecker()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new EachChecker<Student, Student>().SetValidate(null));
            Assert.AreEqual("builder", ex.ParamName);
            Assert.True(ex.Message.Contains("Can't be null"));

            Assert.Throws<NotImplementedException>(() => new EachChecker<Student, Student>().Validate(null, null, null, null));
        }
    }
}