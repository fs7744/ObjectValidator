# ObjectValidator
C# Object Validator, learn from FluentValidation

## simple example :

```Csharp

Container.Init(); // Only need init in your app once

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
```
## Nuget

```
Install-Package ObjectValidator
```

## 剖析实现思路 (analyse how to implementation Code)

http://www.cnblogs.com/fs7744/p/4892126.html 
