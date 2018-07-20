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
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AlexandreApps.Condominial.Backend.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private IUserAppService _userAppService;
        private IPasswordAppService _passwordAppService;
        private ISettingsAppService _settingsAppService;

        public UserController(IUserAppService userAppService, IPasswordAppService passwordAppService, ISettingsAppService settingsAppService)
        {
            this._userAppService = userAppService;
            this._passwordAppService = passwordAppService;
            this._settingsAppService = settingsAppService;
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
            var user = await this._passwordAppService.Login(model);

            if (user == null)
            {
                return new UnauthorizedResult();
            }

            // Create Security key  using private key above:
            // not that latest version of JWT using Microsoft namespace instead of System
            var securityKey = new Microsoft
                .IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingsAppService.Settings.WebtokenKey));

            // Also note that securityKey length should be >256b
            // so you have to make sure that your private key has a proper length
            //
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim(ClaimTypes.Role, "Administrator"),
            }, "Custom");

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = "http://my.website.com",
                Issuer = "http://my.tokenissuer.com",
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);

            return Accepted(new { token = signedAndEncodedToken });
        }

    }
}