using NUnit.Framework;
using ObjectValidator;
using System;
using System.Collections.Generic;
using static UnitTest.Validation_Test;

namespace UnitTest.SyntaxTest
{
    [TestFixture]
    public class RuleCheckerSyntax_Test
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
        public void Test_Syntax_In()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).In(new List<int> { 13, 14 });
            var v = builder.Build();
            var student = new Student() { Age = 13, Name = "v" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 16, Name = "v" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(16, result.Failures[0].Value);
            Assert.AreEqual("Age", result.Failures[0].Name);
            Assert.AreEqual("Not in data array", result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_NotNull_NullableNotNullChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age2).NotNull();
            var v = builder.Build();
            var student = new Student() { Age2 = 13, Name = "v" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "v" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("Age2", result.Failures[0].Name);
            Assert.AreEqual("Can't be null", result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_NotNull_NotNullChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).NotNull();
            var v = builder.Build();
            var student = new Student() { Age2 = 13, Name = "" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student();
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("Can't be null", result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_NotNullOrEmpty()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).NotNullOrEmpty();
            var v = builder.Build();
            var student = new Student() { Age2 = 13, Name = "s" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = " " };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = string.Empty };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(string.Empty, result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);

            student = new Student() { Name = "" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);

            student = new Student();
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_NotNullOrWhiteSpace()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).NotNullOrWhiteSpace();
            var v = builder.Build();
            var student = new Student() { Age2 = 13, Name = "s" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = " " };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(" ", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("Can't be null or empty or whitespace", result.Failures[0].Error);

            student = new Student() { Name = string.Empty };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(string.Empty, result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("Can't be null or empty or whitespace", result.Failures[0].Error);

            student = new Student() { Name = "" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("Can't be null or empty or whitespace", result.Failures[0].Error);

            student = new Student();
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("Can't be null or empty or whitespace", result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_NotNullOrEmpty_NotNullOrEmptyListChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.IntList).NotNullOrEmpty();
            var v = builder.Build();
            var student = new Student() { IntList = new List<int>() { 2, 3 } };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { IntList = new List<int>() };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(new List<int>(), result.Failures[0].Value);
            Assert.AreEqual("IntList", result.Failures[0].Name);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);

            student = new Student() { IntList = null };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(null, result.Failures[0].Value);
            Assert.AreEqual("IntList", result.Failures[0].Name);
            Assert.AreEqual("Can't be null or empty", result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThan_GreaterThanDateTimeChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).GreaterThan(new DateTime(1991, 05, 30));
            var v = builder.Build();
            var student = new Student() { Bir = new DateTime(1999, 05, 30) };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Bir = new DateTime(1989, 05, 30) };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(new DateTime(1989, 05, 30), result.Failures[0].Value);
            Assert.AreEqual("Bir", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", new DateTime(1991, 05, 30)), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThan_GreaterThanFloatChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).GreaterThan(5f);
            var v = builder.Build();
            var student = new Student() { Float = 8f };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Float = 5f };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(5f, result.Failures[0].Value);
            Assert.AreEqual("Float", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", 5f), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThan_GreaterThanIntChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).GreaterThan(15);
            var v = builder.Build();
            var student = new Student() { Age = 18 };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 15 };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(15, result.Failures[0].Value);
            Assert.AreEqual("Age", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", 15), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThan_GreaterThanDoubleChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).GreaterThan(15d);
            var v = builder.Build();
            var student = new Student() { Double = 18d };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Double = 15d };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(15d, result.Failures[0].Value);
            Assert.AreEqual("Double", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", 15d), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThan_GreaterThanDecimalChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).GreaterThan(15m);
            var v = builder.Build();
            var student = new Student() { Decimal = 18m };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Decimal = 15m };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(15m, result.Failures[0].Value);
            Assert.AreEqual("Decimal", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", 15m), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThan_GreaterThanLongChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).GreaterThan(15L);
            var v = builder.Build();
            var student = new Student() { Long = 18L };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Long = 15L };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(15L, result.Failures[0].Value);
            Assert.AreEqual("Long", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than {0}", 15L), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualDateTimeChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).GreaterThanOrEqual(new DateTime(1991, 05, 30));
            var v = builder.Build();
            var student = new Student() { Bir = new DateTime(1991, 05, 30) };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Bir = new DateTime(1989, 05, 30) };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(new DateTime(1989, 05, 30), result.Failures[0].Value);
            Assert.AreEqual("Bir", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than or equal {0}", new DateTime(1991, 05, 30)), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualFloatChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).GreaterThanOrEqual(5f);
            var v = builder.Build();
            var student = new Student() { Float = 5f };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Float = 2f };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(2f, result.Failures[0].Value);
            Assert.AreEqual("Float", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than or equal {0}", 5f), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualIntChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).GreaterThanOrEqual(15);
            var v = builder.Build();
            var student = new Student() { Age = 15 };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 13 };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(13, result.Failures[0].Value);
            Assert.AreEqual("Age", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than or equal {0}", 15), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualDoubleChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).GreaterThanOrEqual(18d);
            var v = builder.Build();
            var student = new Student() { Double = 18d };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Double = 15d };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(15d, result.Failures[0].Value);
            Assert.AreEqual("Double", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than or equal {0}", 18d), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualDecimalChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).GreaterThanOrEqual(18m);
            var v = builder.Build();
            var student = new Student() { Decimal = 18m };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Decimal = 15m };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(15m, result.Failures[0].Value);
            Assert.AreEqual("Decimal", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than or equal {0}", 18m), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualLongChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).GreaterThanOrEqual(18L);
            var v = builder.Build();
            var student = new Student() { Long = 18L };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Long = 15L };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(15L, result.Failures[0].Value);
            Assert.AreEqual("Long", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must greater than or equal {0}", 18L), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThan_LessThanDateTimeChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).LessThan(new DateTime(1991, 05, 30));
            var v = builder.Build();
            var student = new Student() { Bir = new DateTime(1990, 05, 30) };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Bir = new DateTime(1999, 05, 30) };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(new DateTime(1999, 05, 30), result.Failures[0].Value);
            Assert.AreEqual("Bir", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", new DateTime(1991, 05, 30)), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThan_LessThanFloatChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).LessThan(5f);
            var v = builder.Build();
            var student = new Student() { Float = 3f };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Float = 6f };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(6f, result.Failures[0].Value);
            Assert.AreEqual("Float", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", 5f), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThan_LessThanIntChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).LessThan(15);
            var v = builder.Build();
            var student = new Student() { Age = 13 };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 16 };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(16, result.Failures[0].Value);
            Assert.AreEqual("Age", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", 15), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThan_LessThanDoubleChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).LessThan(18d);
            var v = builder.Build();
            var student = new Student() { Double = 13d };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Double = 19d };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(19d, result.Failures[0].Value);
            Assert.AreEqual("Double", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", 18d), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThan_LessThanDecimalChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).LessThan(18m);
            var v = builder.Build();
            var student = new Student() { Decimal = 15m };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Decimal = 19m };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(19m, result.Failures[0].Value);
            Assert.AreEqual("Decimal", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", 18m), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThan_LessThanLongChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).LessThan(18L);
            var v = builder.Build();
            var student = new Student() { Long = 15L };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Long = 19L };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(19L, result.Failures[0].Value);
            Assert.AreEqual("Long", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than {0}", 18L), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThanOrEqual_LessThanOrEqualDateTimeChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).LessThanOrEqual(new DateTime(1991, 05, 30));
            var v = builder.Build();
            var student = new Student() { Bir = new DateTime(1991, 05, 30) };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Bir = new DateTime(1999, 05, 30) };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(new DateTime(1999, 05, 30), result.Failures[0].Value);
            Assert.AreEqual("Bir", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than or equal {0}", new DateTime(1991, 05, 30)), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThanOrEqual_LessThanOrEqualFloatChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).LessThanOrEqual(5f);
            var v = builder.Build();
            var student = new Student() { Float = 5f };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Float = 6f };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(6f, result.Failures[0].Value);
            Assert.AreEqual("Float", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than or equal {0}", 5f), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThanOrEqual_LessThanOrEqualIntChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).LessThanOrEqual(15);
            var v = builder.Build();
            var student = new Student() { Age = 15 };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 16 };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(16, result.Failures[0].Value);
            Assert.AreEqual("Age", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than or equal {0}", 15), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThanOrEqual_LessThanOrEqualDoubleChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).LessThanOrEqual(18d);
            var v = builder.Build();
            var student = new Student() { Double = 18d };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Double = 19d };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(19d, result.Failures[0].Value);
            Assert.AreEqual("Double", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than or equal {0}", 18d), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThanOrEqual_LessThanOrEqualDecimalChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).LessThanOrEqual(18m);
            var v = builder.Build();
            var student = new Student() { Decimal = 18m };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Decimal = 19m };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(19m, result.Failures[0].Value);
            Assert.AreEqual("Decimal", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than or equal {0}", 18m), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_LessThanOrEqual_LessThanOrEqualLongChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).LessThanOrEqual(18L);
            var v = builder.Build();
            var student = new Student() { Long = 18L };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Long = 19L };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(19L, result.Failures[0].Value);
            Assert.AreEqual("Long", result.Failures[0].Name);
            Assert.AreEqual(string.Format("The value must less than or equal {0}", 18L), result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_Must()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i=> i == 18);
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
            Assert.AreEqual(null, result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_MustNot()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).MustNot(i => i != 18);
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
            Assert.AreEqual(null, result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_Equal()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Equal(18);
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
            Assert.AreEqual("The value is not equal 18", result.Failures[0].Error);
        }

        [Test]
        public void Test_Syntax_NotEqual()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).NotEqual(18);
            var v = builder.Build();
            var student = new Student() { Age = 19 };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 18 };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(18, result.Failures[0].Value);
            Assert.AreEqual("Age", result.Failures[0].Name);
            Assert.AreEqual("The value is equal 18", result.Failures[0].Error);
        }
    }
}