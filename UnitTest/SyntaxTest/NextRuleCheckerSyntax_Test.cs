using NUnit.Framework;
using ObjectValidator;
using ObjectValidator.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static UnitTest.Validation_Test;

namespace UnitTest.SyntaxTest
{
    [TestFixture]
    public class NextRuleCheckerSyntax_Test
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
        public void Test_NextRuleCheckerSyntax_Must()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => true)
                .Must(i => i == 18).OverrideName("18 years");
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
        public void Test_NextRuleCheckerSyntax_MustNot()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => true)
                .MustNot(i => i != 18).OverrideName("18 years");
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
        public void Test_NextRuleCheckerSyntax_Length()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true)
                .Length(3, 8);
            var v = builder.Build();
            var student = new Student() { Name = "1234" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "12" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("12", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The length 2 is not between 3 and 8", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_LengthEqual()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true)
                .LengthEqual(3);
            var v = builder.Build();
            var student = new Student() { Name = "123" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "12" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("12", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The length 2 is not between 3 and 3", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_LengthLessThanOrEqual()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true)
                .LengthLessThanOrEqual(3);
            var v = builder.Build();
            var student = new Student() { Name = "123" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "1244" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("1244", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The length 4 is not between -1 and 3", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_LengthGreaterThanOrEqual()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true).LengthGreaterThanOrEqual(3);
            var v = builder.Build();
            var student = new Student() { Name = "abc4" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("ac", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The length 2 is not between 3 and -1", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_Equal()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => true).Equal(18);
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
        public void Test_NextRuleCheckerSyntax_NotEqual()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => true).NotEqual(18);
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

        [Test]
        public void Test_NextRuleCheckerSyntax_Email()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true).Email();
            var v = builder.Build();
            var student = new Student() { Name = "ab3@23.COM" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac33" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("ac33", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The value is not email address", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_Regex()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true).Regex(new Regex(Syntax.EmailRegex));
            var v = builder.Build();
            var student = new Student() { Name = "ab3@23.COM" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac33" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("ac33", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The value no match regex", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_RegexOptions()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true).Regex(Syntax.EmailRegex, RegexOptions.IgnoreCase);
            var v = builder.Build();
            var student = new Student() { Name = "ab3@23.COM" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac33" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("ac33", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The value no match regex", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_RegexPattern()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true).Regex(Syntax.EmailRegex);
            var v = builder.Build();
            var student = new Student() { Name = "ab3@23.COM" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ac33" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("ac33", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The value no match regex", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_NotRegex()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true).NotRegex(new Regex(Syntax.EmailRegex));
            var v = builder.Build();
            var student = new Student() { Name = "ac33" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ab3@23.COM" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("ab3@23.COM", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The value must be not match regex", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_NotRegexOptions()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true).NotRegex(Syntax.EmailRegex, RegexOptions.IgnoreCase);
            var v = builder.Build();
            var student = new Student() { Name = "ac33" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ab3@23.COM" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("ab3@23.COM", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The value must be not match regex", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_NotRegexPattern()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Name).Must(i => true).NotRegex(Syntax.EmailRegex);
            var v = builder.Build();
            var student = new Student() { Name = "ac33" };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Name = "ab3@23.COM" };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual("ab3@23.COM", result.Failures[0].Value);
            Assert.AreEqual("Name", result.Failures[0].Name);
            Assert.AreEqual("The value must be not match regex", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_Between_BetweenFloatChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).Must(i => true).Between(3f, 9f);
            var v = builder.Build();
            var student = new Student() { Float = 5f };
            var context = Validation.CreateContext(student);
            var result = v.Validate(context);
            Assert.NotNull(result);
            Assert.True(result.IsValid);

            student = new Student() { Float = 15f };
            context = Validation.CreateContext(student);
            result = v.Validate(context);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.AreEqual(15f, result.Failures[0].Value);
            Assert.AreEqual("Float", result.Failures[0].Name);
            Assert.AreEqual("The value is not between 3 and 9", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_Between_BetweenIntChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => true).Between(3, 9);
            var v = builder.Build();
            var student = new Student() { Age = 5 };
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
            Assert.AreEqual("The value is not between 3 and 9", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_Between_BetweenDoubleChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).Must(i => true).Between(3d, 9d);
            var v = builder.Build();
            var student = new Student() { Double = 5d };
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
            Assert.AreEqual("The value is not between 3 and 9", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_Between_BetweenDecimalChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).Must(i => true).Between(3m, 9m);
            var v = builder.Build();
            var student = new Student() { Decimal = 5m };
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
            Assert.AreEqual("The value is not between 3 and 9", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_Between_BetweenLongChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).Must(i => true).Between(3L, 9L);
            var v = builder.Build();
            var student = new Student() { Long = 5L };
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
            Assert.AreEqual("The value is not between 3 and 9", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_GreaterThan_GreaterThanDateTimeChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).Must(i => true).GreaterThan(new DateTime(1991, 05, 30));
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
        public void Test_NextRuleCheckerSyntax_GreaterThan_GreaterThanFloatChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).Must(i => true).GreaterThan(5f);
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
        public void Test_NextRuleCheckerSyntax_GreaterThan_GreaterThanIntChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => true).GreaterThan(15);
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
        public void Test_NextRuleCheckerSyntax_GreaterThan_GreaterThanDoubleChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).Must(i => true).GreaterThan(15d);
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
        public void Test_NextRuleCheckerSyntax_GreaterThan_GreaterThanDecimalChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).Must(i => true).GreaterThan(15m);
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
        public void Test_NextRuleCheckerSyntax_GreaterThan_GreaterThanLongChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).Must(i => true).GreaterThan(15L);
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
        public void Test_NextRuleCheckerSyntax_GreaterThanOrEqual_GreaterThanOrEqualDateTimeChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).Must(i => true).GreaterThanOrEqual(new DateTime(1991, 05, 30));
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
        public void Test_NextRuleCheckerSyntax_GreaterThanOrEqual_GreaterThanOrEqualFloatChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).Must(i => true).GreaterThanOrEqual(5f);
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
        public void Test_NextRuleCheckerSyntax_GreaterThanOrEqual_GreaterThanOrEqualIntChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => true).GreaterThanOrEqual(15);
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
        public void Test_NextRuleCheckerSyntax_GreaterThanOrEqual_GreaterThanOrEqualDoubleChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).Must(i => true).GreaterThanOrEqual(18d);
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
        public void Test_NextRuleCheckerSyntax_GreaterThanOrEqual_GreaterThanOrEqualDecimalChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).Must(i => true).GreaterThanOrEqual(18m);
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
        public void Test_NextRuleCheckerSyntax_GreaterThanOrEqual_GreaterThanOrEqualLongChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).Must(i => true).GreaterThanOrEqual(18L);
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
        public void Test_NextRuleCheckerSyntax_In()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => true).In(new List<int> { 13, 14 });
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
        public void Test_NextRuleCheckerSyntax_CustomCheck()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).Must(i => true).CustomCheck(i => 3L < i && i < 9L ? null : new List<ValidateFailure>() { new ValidateFailure() { Name = "Long", Value = i, Error = "CustomCheck" } });
            var v = builder.Build();
            var student = new Student() { Long = 5L };
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
            Assert.AreEqual("CustomCheck", result.Failures[0].Error);
        }

        [Test]
        public void Test_NextRuleCheckerSyntax_LessThan_LessThanDateTimeChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).Must(i => true).LessThan(new DateTime(1991, 05, 30));
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
        public void Test_NextRuleCheckerSyntax_LessThan_LessThanFloatChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).Must(i => true).LessThan(5f);
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
        public void Test_NextRuleCheckerSyntax_LessThan_LessThanIntChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => true).LessThan(15);
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
        public void Test_NextRuleCheckerSyntax_LessThan_LessThanDoubleChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).Must(i => true).LessThan(18d);
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
        public void Test_NextRuleCheckerSyntax_LessThan_LessThanDecimalChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).Must(i => true).LessThan(18m);
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
        public void Test_NextRuleCheckerSyntax_LessThan_LessThanLongChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).Must(i => true).LessThan(18L);
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
        public void Test_NextRuleCheckerSyntax_LessThanOrEqual_LessThanOrEqualDateTimeChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Bir).Must(i => true).LessThanOrEqual(new DateTime(1991, 05, 30));
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
        public void Test_NextRuleCheckerSyntax_LessThanOrEqual_LessThanOrEqualFloatChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Float).Must(i => true).LessThanOrEqual(5f);
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
        public void Test_NextRuleCheckerSyntax_LessThanOrEqual_LessThanOrEqualIntChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Age).Must(i => true).LessThanOrEqual(15);
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
        public void Test_NextRuleCheckerSyntax_LessThanOrEqual_LessThanOrEqualDoubleChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Double).Must(i => true).LessThanOrEqual(18d);
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
        public void Test_NextRuleCheckerSyntax_LessThanOrEqual_LessThanOrEqualDecimalChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Decimal).Must(i => true).LessThanOrEqual(18m);
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
        public void Test_NextRuleCheckerSyntax_LessThanOrEqual_LessThanOrEqualLongChecker()
        {
            var builder = Validation.NewValidatorBuilder<Student>();
            builder.RuleFor(i => i.Long).Must(i => true).LessThanOrEqual(18L);
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
    }
}