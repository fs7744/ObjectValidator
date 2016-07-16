# ObjectValidator
C# Object Validator, learn from FluentValidation

## simple example :

```Csharp

public void ConfigureServices(IServiceCollection services)
{
	// Add framework services.
	services.AddMemoryCache();
	services.AddMvc();
	services.AddObjectValidator();
}

[Route("api/[controller]")]
public class ValuesController : Controller
{
	private Validation _Validation;
	private IMemoryCache _Cache;
	public ValuesController(Validation validation, IMemoryCache cache)
	{
		_Cache = cache;
		_Validation = validation;
	}

	// GET api/values/5
	[HttpGet("{id}")]
	public async Task<IValidateResult> Get(int id)
	{
		var validator = _Cache.GetOrCreate("test", j =>
		{
			var builder = _Validation.NewValidatorBuilder<Student>();
			builder.RuleFor(i => i.ID).GreaterThan(0);
			return builder.Build();
		});
		
		return await validator.ValidateAsync(_Validation.CreateContext(new Student() { ID = id }));
	}
}
```
## Nuget

```
Install-Package ObjectValidator
```

## 剖析实现思路 (analyse how to implementation Code)

http://www.cnblogs.com/fs7744/p/4892126.html 
