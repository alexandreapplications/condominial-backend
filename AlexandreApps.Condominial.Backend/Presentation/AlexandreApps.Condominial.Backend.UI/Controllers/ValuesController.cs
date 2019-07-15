using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlexandreApps.Condominial.Backend.Model.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AlexandreApps.Condominial.Backend.UI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await Task.FromResult<RequiredStringValueModel[]>(new RequiredStringValueModel[] 
            {
                new RequiredStringValueModel() { value = "value1" },
                new RequiredStringValueModel() { value = "value2" }
            });
            return new OkObjectResult(model);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await Task.FromResult<RequiredStringValueModel>( new RequiredStringValueModel { value = $"value{id}" });
            return new OkObjectResult(model);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RequiredStringValueModel value)
        {
            if (ModelState.IsValid)
            {
                var returnModel = await Task.FromResult(value);
                if (value != null && value.value != null && value.value.StartsWith("value"))
                {
                    return new OkObjectResult(value);
                }
                else
                {
                    ModelState.AddModelError("InvalidString", "Invalid string");
                }
            }
            return new BadRequestObjectResult(ModelState);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]RequiredStringValueModel value)
        {
            if (ModelState.IsValid)
            {
                var returnModel = await Task.FromResult(value);
                if (value != null && value.value != null && value.value == $"value{id}")
                {
                    return new OkObjectResult(value);
                }
                else
                {
                    ModelState.AddModelError("InvalidString", "Invalid string");
                }
            }
            return new BadRequestObjectResult(ModelState);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var returnModel = await Task.FromResult(id);

                if (id > 10)
                {
                    throw new ApplicationException("Invalid Key");
                }
                else
                {
                    return new OkObjectResult(id);
                }
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
