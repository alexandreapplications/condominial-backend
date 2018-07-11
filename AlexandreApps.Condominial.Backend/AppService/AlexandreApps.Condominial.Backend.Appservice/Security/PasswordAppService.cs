using AlexandreApps.Condominial.Backend.Exceptions.Application;
using AlexandreApps.Condominial.Backend.Interfaces.AppService.Security;
using AlexandreApps.Condominial.Backend.Interfaces.DataService.Security;
using AlexandreApps.Condominial.Backend.Model.Security;
using AlexandreApps.Condominial.Backend.Model.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Appservice.Security
{
    public class PasswordAppService
    {
        private IPasswordDataService PasswordDataService { get; set; }
        private IUserDataService UserDataService { get; set; }
        public PasswordAppService(IPasswordDataService passwordDataService, IUserDataService userDataService)
        {
            this.PasswordDataService = passwordDataService;
            this.UserDataService = userDataService;
        }

        public async Task<IEnumerable<Guid>> SetPassword(PasswordViewModel model)
        {
            // Verify if the user exists
            var users = await UserDataService.GetByLogin(model.Login);
            if (users == null || users.Count > 0)
            {
                throw new UserDoesntExistsException(model.Login);
            }
            var user = users.First();
            return await PasswordDataService.Insert(new PasswordModel
            {
                UserId = user.Id,
                Id = Guid.Empty,
                Date = DateTime.UtcNow,
                Password = this.DoEncriptPassword(user.Id, model.Password)
            });
        }

        private byte[] DoEncriptPassword(Guid userId, string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(string.Join(password, userId));
            return new SHA256Managed().ComputeHash(data);
        }

    }
}
