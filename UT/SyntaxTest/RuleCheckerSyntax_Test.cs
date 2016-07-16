using Microsoft.Extensions.DependencyInjection;
using ObjectValidator;
using ObjectValidator.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;
using static UnitTest.Validation_Test;

namespace UnitTest.SyntaxTest
{
    public class RuleCheckerSyntax_Test
    {
        private Validation _Validation = new ServiceCollection().AddObjectValidator().BuildServiceProvider().GetService<Validation>();

        [Fact]
        public async void Test_Syntax_In()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).In(new List<int> { 13, 14 });
            var v = builder.Build();
            var student = new Student() { Age = 13, Name = "v" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 16, Name = "v" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(16, result.Failures[0].Value);
            Assert.Equal("Age", result.Failures[0].Name);
            Assert.Equal("Not in data array", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_NotNull_NullableNotNullChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age2).NotNull();
            var v = builder.Build();
            var student = new Student() { Age2 = 13, Name = "v" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "v" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("Age2", result.Failures[0].Name);
            Assert.Equal("Can't be null", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_NotNull_NotNullChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).NotNull();
            var v = builder.Build();
            var student = new Student() { Age2 = 13, Name = "" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student();
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("Can't be null", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_NotNullOrEmpty()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).NotNullOrEmpty();
            var v = builder.Build();
            var student = new Student() { Age2 = 13, Name = "s" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = " " };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = string.Empty };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(string.Empty, result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);

            student = new Student() { Name = "" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);

            student = new Student();
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_NotNullOrWhiteSpace()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).NotNullOrWhiteSpace();
            var v = builder.Build();
            var student = new Student() { Age2 = 13, Name = "s" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = " " };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(" ", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("Can't be null or empty or whitespace", result.Failures[0].Error);

            student = new Student() { Name = string.Empty };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(string.Empty, result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("Can't be null or empty or whitespace", result.Failures[0].Error);

            student = new Student() { Name = "" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("Can't be null or empty or whitespace", result.Failures[0].Error);

            student = new Student();
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("Can't be null or empty or whitespace", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_NotNullOrEmpty_NotNullOrEmptyListChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.IntList).NotNullOrEmpty();
            var v = builder.Build();
            var student = new Student() { IntList = new List<int>() { 2, 3 } };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { IntList = new List<int>() };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(new List<int>(), result.Failures[0].Value);
            Assert.Equal("IntList", result.Failures[0].Name);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);

            student = new Student() { IntList = null };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(null, result.Failures[0].Value);
            Assert.Equal("IntList", result.Failures[0].Name);
            Assert.Equal("Can't be null or empty", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThan_GreaterThanDateTimeChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).GreaterThan(new DateTime(1991, 05, 30));
            var v = builder.Build();
            var student = new Student() { Bir = new DateTime(1999, 05, 30) };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Bir = new DateTime(1989, 05, 30) };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(new DateTime(1989, 05, 30), result.Failures[0].Value);
            Assert.Equal("Bir", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than {0}", new DateTime(1991, 05, 30)), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThan_GreaterThanFloatChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).GreaterThan(5f);
            var v = builder.Build();
            var student = new Student() { Float = 8f };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Float = 5f };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(5f, result.Failures[0].Value);
            Assert.Equal("Float", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than {0}", 5f), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThan_GreaterThanIntChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).GreaterThan(15);
            var v = builder.Build();
            var student = new Student() { Age = 18 };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 15 };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15, result.Failures[0].Value);
            Assert.Equal("Age", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than {0}", 15), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThan_GreaterThanDoubleChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).GreaterThan(15d);
            var v = builder.Build();
            var student = new Student() { Double = 18d };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Double = 15d };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15d, result.Failures[0].Value);
            Assert.Equal("Double", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than {0}", 15d), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThan_GreaterThanDecimalChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).GreaterThan(15m);
            var v = builder.Build();
            var student = new Student() { Decimal = 18m };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Decimal = 15m };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15m, result.Failures[0].Value);
            Assert.Equal("Decimal", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than {0}", 15m), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThan_GreaterThanLongChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).GreaterThan(15L);
            var v = builder.Build();
            var student = new Student() { Long = 18L };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Long = 15L };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15L, result.Failures[0].Value);
            Assert.Equal("Long", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than {0}", 15L), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualDateTimeChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).GreaterThanOrEqual(new DateTime(1991, 05, 30));
            var v = builder.Build();
            var student = new Student() { Bir = new DateTime(1991, 05, 30) };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Bir = new DateTime(1989, 05, 30) };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(new DateTime(1989, 05, 30), result.Failures[0].Value);
            Assert.Equal("Bir", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", new DateTime(1991, 05, 30)), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualFloatChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).GreaterThanOrEqual(5f);
            var v = builder.Build();
            var student = new Student() { Float = 5f };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Float = 2f };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(2f, result.Failures[0].Value);
            Assert.Equal("Float", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", 5f), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualIntChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).GreaterThanOrEqual(15);
            var v = builder.Build();
            var student = new Student() { Age = 15 };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 13 };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(13, result.Failures[0].Value);
            Assert.Equal("Age", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", 15), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualDoubleChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).GreaterThanOrEqual(18d);
            var v = builder.Build();
            var student = new Student() { Double = 18d };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Double = 15d };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15d, result.Failures[0].Value);
            Assert.Equal("Double", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", 18d), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualDecimalChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).GreaterThanOrEqual(18m);
            var v = builder.Build();
            var student = new Student() { Decimal = 18m };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Decimal = 15m };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15m, result.Failures[0].Value);
            Assert.Equal("Decimal", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", 18m), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_GreaterThanOrEqual_GreaterThanOrEqualLongChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).GreaterThanOrEqual(18L);
            var v = builder.Build();
            var student = new Student() { Long = 18L };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Long = 15L };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15L, result.Failures[0].Value);
            Assert.Equal("Long", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must greater than or equal {0}", 18L), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThan_LessThanDateTimeChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).LessThan(new DateTime(1991, 05, 30));
            var v = builder.Build();
            var student = new Student() { Bir = new DateTime(1990, 05, 30) };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Bir = new DateTime(1999, 05, 30) };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(new DateTime(1999, 05, 30), result.Failures[0].Value);
            Assert.Equal("Bir", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", new DateTime(1991, 05, 30)), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThan_LessThanFloatChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).LessThan(5f);
            var v = builder.Build();
            var student = new Student() { Float = 3f };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Float = 6f };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(6f, result.Failures[0].Value);
            Assert.Equal("Float", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", 5f), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThan_LessThanIntChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).LessThan(15);
            var v = builder.Build();
            var student = new Student() { Age = 13 };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 16 };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(16, result.Failures[0].Value);
            Assert.Equal("Age", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", 15), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThan_LessThanDoubleChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).LessThan(18d);
            var v = builder.Build();
            var student = new Student() { Double = 13d };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Double = 19d };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(19d, result.Failures[0].Value);
            Assert.Equal("Double", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", 18d), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThan_LessThanDecimalChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).LessThan(18m);
            var v = builder.Build();
            var student = new Student() { Decimal = 15m };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Decimal = 19m };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(19m, result.Failures[0].Value);
            Assert.Equal("Decimal", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", 18m), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThan_LessThanLongChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).LessThan(18L);
            var v = builder.Build();
            var student = new Student() { Long = 15L };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Long = 19L };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(19L, result.Failures[0].Value);
            Assert.Equal("Long", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than {0}", 18L), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThanOrEqual_LessThanOrEqualDateTimeChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).LessThanOrEqual(new DateTime(1991, 05, 30));
            var v = builder.Build();
            var student = new Student() { Bir = new DateTime(1991, 05, 30) };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Bir = new DateTime(1999, 05, 30) };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(new DateTime(1999, 05, 30), result.Failures[0].Value);
            Assert.Equal("Bir", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than or equal {0}", new DateTime(1991, 05, 30)), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThanOrEqual_LessThanOrEqualFloatChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).LessThanOrEqual(5f);
            var v = builder.Build();
            var student = new Student() { Float = 5f };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Float = 6f };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(6f, result.Failures[0].Value);
            Assert.Equal("Float", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than or equal {0}", 5f), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThanOrEqual_LessThanOrEqualIntChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).LessThanOrEqual(15);
            var v = builder.Build();
            var student = new Student() { Age = 15 };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 16 };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(16, result.Failures[0].Value);
            Assert.Equal("Age", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than or equal {0}", 15), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThanOrEqual_LessThanOrEqualDoubleChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).LessThanOrEqual(18d);
            var v = builder.Build();
            var student = new Student() { Double = 18d };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Double = 19d };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(19d, result.Failures[0].Value);
            Assert.Equal("Double", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than or equal {0}", 18d), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThanOrEqual_LessThanOrEqualDecimalChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).LessThanOrEqual(18m);
            var v = builder.Build();
            var student = new Student() { Decimal = 18m };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Decimal = 19m };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(19m, result.Failures[0].Value);
            Assert.Equal("Decimal", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than or equal {0}", 18m), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LessThanOrEqual_LessThanOrEqualLongChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).LessThanOrEqual(18L);
            var v = builder.Build();
            var student = new Student() { Long = 18L };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Long = 19L };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(19L, result.Failures[0].Value);
            Assert.Equal("Long", result.Failures[0].Name);
            Assert.Equal(string.Format("The value must less than or equal {0}", 18L), result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_Must()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => i == 18);
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
            Assert.Equal(null, result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_MustNot()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).MustNot(i => i != 18);
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
            Assert.Equal(null, result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_Equal()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Equal(18);
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
            Assert.Equal("The value is not equal 18", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_NotEqual()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).NotEqual(18);
            var v = builder.Build();
            var student = new Student() { Age = 19 };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 18 };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(18, result.Failures[0].Value);
            Assert.Equal("Age", result.Failures[0].Name);
            Assert.Equal("The value is equal 18", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_Length()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Length(3, 18);
            var v = builder.Build();
            var student = new Student() { Name = "abc" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ac", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The length 2 is not between 3 and 18", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LengthEqual()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).LengthEqual(3);
            var v = builder.Build();
            var student = new Student() { Name = "abc" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ac", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The length 2 is not between 3 and 3", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LengthGreaterThanOrEqual()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).LengthGreaterThanOrEqual(3);
            var v = builder.Build();
            var student = new Student() { Name = "abc4" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ac", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The length 2 is not between 3 and -1", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_LengthLessThanOrEqual()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).LengthLessThanOrEqual(3);
            var v = builder.Build();
            var student = new Student() { Name = "ab3" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac33" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ac33", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The length 4 is not between -1 and 3", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_Email()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Email();
            var v = builder.Build();
            var student = new Student() { Name = "ab3@23.COM" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac33" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ac33", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The value is not email address", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_Regex()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Regex(new Regex(Syntax.EmailRegex));
            var v = builder.Build();
            var student = new Student() { Name = "ab3@23.COM" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac33" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ac33", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The value no match regex", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_RegexOptions()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Regex(Syntax.EmailRegex, RegexOptions.IgnoreCase);
            var v = builder.Build();
            var student = new Student() { Name = "ab3@23.COM" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac33" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ac33", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The value no match regex", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_RegexPattern()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Regex(Syntax.EmailRegex);
            var v = builder.Build();
            var student = new Student() { Name = "ab3@23.COM" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac33" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ac33", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The value no match regex", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_NotRegex()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).NotRegex(new Regex(Syntax.EmailRegex));
            var v = builder.Build();
            var student = new Student() { Name = "ac33" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ab3@23.COM" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ab3@23.COM", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The value must be not match regex", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_NotRegexOptions()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).NotRegex(Syntax.EmailRegex, RegexOptions.IgnoreCase);
            var v = builder.Build();
            var student = new Student() { Name = "ac33" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ab3@23.COM" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ab3@23.COM", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The value must be not match regex", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_NotRegexPattern()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).NotRegex(Syntax.EmailRegex);
            var v = builder.Build();
            var student = new Student() { Name = "ac33" };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ab3@23.COM" };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("ab3@23.COM", result.Failures[0].Value);
            Assert.Equal("Name", result.Failures[0].Name);
            Assert.Equal("The value must be not match regex", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_Between_BetweenFloatChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).Between(3f, 9f);
            var v = builder.Build();
            var student = new Student() { Float = 5f };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Float = 15f };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15f, result.Failures[0].Value);
            Assert.Equal("Float", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 9", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_Between_BetweenIntChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Between(3, 9);
            var v = builder.Build();
            var student = new Student() { Age = 5 };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Age = 15 };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15, result.Failures[0].Value);
            Assert.Equal("Age", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 9", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_Between_BetweenDoubleChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).Between(3d, 9d);
            var v = builder.Build();
            var student = new Student() { Double = 5d };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Double = 15d };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15d, result.Failures[0].Value);
            Assert.Equal("Double", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 9", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_Between_BetweenDecimalChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).Between(3m, 9m);
            var v = builder.Build();
            var student = new Student() { Decimal = 5m };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Decimal = 15m };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15m, result.Failures[0].Value);
            Assert.Equal("Decimal", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 9", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_Between_BetweenLongChecker()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).Between(3L, 9L);
            var v = builder.Build();
            var student = new Student() { Long = 5L };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Long = 15L };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15L, result.Failures[0].Value);
            Assert.Equal("Long", result.Failures[0].Name);
            Assert.Equal("The value is not between 3 and 9", result.Failures[0].Error);
        }

        [Fact]
        public async void Test_Syntax_CustomCheck()
        {
            var builder = _Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).CustomCheck(i => 3L < i && i < 9L ? null : new List<ValidateFailure>() { new ValidateFailure() { Name = "Long", Value = i, Error = "CustomCheck" } });
            var v = builder.Build();
            var student = new Student() { Long = 5L };
            var context = _Validation.CreateContext(student);
            var result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Long = 15L };
            context = _Validation.CreateContext(student);
            result = await v.ValidateAsync(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(15L, result.Failures[0].Value);
            Assert.Equal("Long", result.Failures[0].Name);
            Assert.Equal("CustomCheck", result.Failures[0].Error);
        }
    }
}