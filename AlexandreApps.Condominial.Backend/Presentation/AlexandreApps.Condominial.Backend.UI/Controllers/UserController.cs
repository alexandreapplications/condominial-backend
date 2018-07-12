using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlexandreApps.Condominial.Backend.Appservice.Security;
using AlexandreApps.Condominial.Backend.Interfaces.AppService.Security;
using AlexandreApps.Condominial.Backend.Interfaces.DataService.Security;
using AlexandreApps.Condominial.Backend.Model.Security.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlexandreApps.Condominial.Backend.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private IUserAppService _userAppService;
        private IPasswordAppService _passwordAppService;

        public UserController(IUserAppService userAppService, IPasswordAppService passwordAppService)
        {
            this._userAppService = userAppService;
            this._passwordAppService = passwordAppService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid? id)
        {
            if (id.HasValue)
                return new OkObjectResult(await this._userAppService.Get(id.Value));
            return new OkObjectResult(await this._userAppService.GetAll());
        }

        [HttpPut("Insert")]
        public async Task<IActionResult> Insert([FromBody] UserViewModel model)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(ModelState);

            var answer = await this._userAppService.Insert(model);

            return Created($"api/user/{ answer.First() }", answer);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UserViewModel model)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(ModelState);

            var answer = await this._userAppService.Update(model);

            return Accepted($"api/user/{ answer.First() }", answer);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] Guid model)
        {
            await this._userAppService.Delete(model);

            return Accepted();
        }

        [HttpPut("Subscribe")]
        public async Task<IActionResult> Subscribe(SubscribeViewModel model)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(ModelState);
            var answer = await this._userAppService.Subscribe(model);
            return Created($"api/user/{ answer }", answer);
        }

        [HttpPut("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(ModelState);
            var answer = await this._passwordAppService.Login(model);
            return Accepted(answer);
        }

    }
}