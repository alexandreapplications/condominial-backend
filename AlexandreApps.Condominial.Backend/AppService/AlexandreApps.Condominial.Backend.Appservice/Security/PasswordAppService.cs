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

namespace AlexandreApps.Condominial.Backend.Appservice.Security
{
    public class PasswordAppService: IPasswordAppService
    {
        private IPasswordDataService _passwordDataService { get; set; }
        private IUserDataService _userDataService { get; set; }
        public PasswordAppService(IPasswordDataService passwordDataService, IUserDataService userDataService)
        {
            this._passwordDataService = passwordDataService;
            this._userDataService = userDataService;
        }

        public async Task<IEnumerable<Guid>> SetPassword(PasswordViewModel model)
        {
            // Verify if the user exists
            var users = await _userDataService.GetByLogin(model.Login);
            if (users == null || users.Count > 0)
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

        public async Task<bool> Login(LoginViewModel model)
        {
            var users = await _userDataService.GetByLogin(model.Login);
            if (users == null || users.Count > 0)
            {
                throw new UserDoesntExistsException(model.Login);
            }
            var user = users.First();
            var pwdData = await this._passwordDataService.GetLast(user.Id);

            var encPwd = DoEncriptPassword(user.Id, model.Password);

            return encPwd.SequenceEqual(pwdData.Password);
        }

        public async Task<bool> ChangePassword(ChangePasswordViewModel model)
        {
            var isValid = await Login(new LoginViewModel
            {
                Login = model.Login,
                Password = model.Password
            });
            if (isValid)
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
