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
        private IUserAppService _userDataService;

        public UserController(IUserAppService userDataService)
        {
            this._userDataService = userDataService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await this._userDataService.Get());
        }

        [HttpPut("Insert")]
        public void Insert([FromBody] UserViewModel[] models)
        {
            if (this.ModelState.IsValid)
            {
                this._userDataService.Insert(models);
            }
        }

        [HttpPut("Update")]
        public void Update([FromBody] UserViewModel[] models)
        {
            if (this.ModelState.IsValid)
            {
                this._userDataService.Update(models);
            }
        }

        [HttpPut("Delete")]
        public void Delete([FromBody] Guid[] models)
        {
            this._userDataService.Delete(models);
        }
    }
}