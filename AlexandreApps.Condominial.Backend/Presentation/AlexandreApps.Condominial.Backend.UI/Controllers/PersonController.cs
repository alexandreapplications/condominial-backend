using System;
using System.Linq;
using System.Threading.Tasks;
using AlexandreApps.Condominial.Backend.Exceptions.Security;
using AlexandreApps.Condominial.Backend.Interfaces.AppService.Global;
using AlexandreApps.Condominial.Backend.Model.Domain;
using AlexandreApps.Condominial.Backend.Model.Global.ViewModel;
using AlexandreApps.Condominial.Backend.Model.Security;
using AlexandreApps.Condominial.Backend.Webtokens;
using Microsoft.AspNetCore.Mvc;

namespace AlexandreApps.Condominial.Backend.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/Person")]
    public class PersonController : Controller
    {
        private readonly IPersonAppService _personDataService;
        private readonly UserModel userModel = new UserModel
        {
            Id = Guid.Empty,
            Login = "teste@email.com.br",
            Name = "Teste Silva",
            BirthDate = new DateTime (2000, 1, 1),
            Country = 55,
            PersonId = "",
            SubscribeDate = new DateTime(2018, 1, 1)
        };

        public PersonController(IPersonAppService personDataService)
        {
            this._personDataService = personDataService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid? id)
        {
            if (id.HasValue)
                return new OkObjectResult(await this._personDataService.Get(id.Value));
            return new OkObjectResult(await this._personDataService.GetAll());
        }

        [HttpPut("Insert")]
        public async Task<IActionResult> Create([FromBody] PersonViewModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                    return BadRequest(ModelState);

                var answer = await this._personDataService.Insert(userModel, model);

                return Created($"api/person/{ answer.First() }", answer);
            }
            catch (InvalidTokenException)
            {
                return new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] PersonViewModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                    return BadRequest(ModelState);

                var answer = await this._personDataService.Update(userModel, model);

                return Accepted($"api/person/{ answer.First() }", answer);
            }
            catch (InvalidTokenException)
            {
                return new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] Guid model)
        {
            await this._personDataService.Delete(null, model);

            return Accepted();
        }
    }
}