using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ObjectValidator;
using ObjectValidator.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace WebTest.Controllers
{
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

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public class Student
        {
            public int ID { get; set; }
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

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}