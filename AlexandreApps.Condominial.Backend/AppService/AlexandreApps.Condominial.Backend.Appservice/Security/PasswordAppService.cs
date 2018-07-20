using AlexandreApps.Condominial.Backend.Interfaces.AppService.Security;
using AlexandreApps.Condominial.Backend.Interfaces.DataService.Security;
using AlexandreApps.Condominial.Backend.Model.Security;
using AlexandreApps.Condominial.Backend.Model.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Threading.Tasks;
using AlexandreApps.Condominial.Backend.Exceptions.Application;
using AlexandreApps.Condominial.Backend.Webtokens;
using AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;

namespace AlexandreApps.Condominial.Backend.Appservice.Security
{
    public class PasswordAppService: IPasswordAppService
    {
        private IPasswordDataService _passwordDataService { get; set; }
        private IUserDataService _userDataService { get; set; }
        private ISettingsAppService _settingsAppService { get; set; }
        public PasswordAppService(IPasswordDataService passwordDataService, IUserDataService userDataService, ISettingsAppService settingsAppService)
        {
            this._passwordDataService = passwordDataService;
            this._userDataService = userDataService;
            this._settingsAppService = settingsAppService;
        }

        public async Task<IEnumerable<Guid>> SetPassword(PasswordViewModel model)
        {
            // Verify if the user exists
            var users = await _userDataService.GetByLogin(model.Login);
            if (users == null || users.Count == 0)
            {
                throw new UserDoesntExistsException(model.Login);
            }
            var user = users.First();
            return await _passwordDataService.Insert(new PasswordModel
            {
                UserId = user.Id,
                Id = Guid.Empty,
                Date = DateTime.UtcNow,
                Password = this.DoEncriptPassword(user.Id, model.Password)
            });
        }

        public async Task<UserViewModel> Login(LoginViewModel model)
        {
            var users = await _userDataService.GetByLogin(model.Login);
            if (users == null || users.Count == 0)
            {
                throw new UserDoesntExistsException(model.Login);
            }
            var user = users.First();
            var pwdData = await this._passwordDataService.GetLast(user.Id);

            if (pwdData == null)
                return null;

            var encPwd = DoEncriptPassword(user.Id, model.Password);

            if (encPwd.SequenceEqual(pwdData.Password))
            {
                return new UserViewModel
                {
                    Id = user.Id,
                    Login = user.Login,
                    Name = user.Name,
                    BirthDate = user.BirthDate,
                    Country = user.Country,
                    PersonId = user.PersonId,
                    SubscribeDate = user.SubscribeDate
                };
            }
            return null;
        }

        public async Task<bool> ChangePassword(ChangePasswordViewModel model)
        {
            var userModel = await Login(new LoginViewModel
            {
                Login = model.Login,
                Password = model.Password
            });
            if (userModel != null)
            {
                var info = await SetPassword(new PasswordViewModel
                {
                    Login = model.Login,
                    Password = model.Password
                });
                if (info != null && info.Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> HasPassword(Guid id)
        {
            return await this._passwordDataService.GetLast(id) != null;
        }

        private byte[] DoEncriptPassword(Guid userId, string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(string.Join(password, userId));
            return new SHA256Managed().ComputeHash(data);
        }

    }
}
