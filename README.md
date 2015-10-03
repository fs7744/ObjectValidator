# ObjectValidator
C# Object Validator, learn from FluentValidation

simple example :

```Csharp
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
```
