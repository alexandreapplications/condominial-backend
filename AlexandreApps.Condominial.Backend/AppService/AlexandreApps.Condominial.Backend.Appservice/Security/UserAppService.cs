using AlexandreApps.Condominial.Backend.Interfaces.AppService.Security;
using AlexandreApps.Condominial.Backend.Interfaces.DataService.Security;
using AlexandreApps.Condominial.Backend.Model.Security;
using AlexandreApps.Condominial.Backend.Model.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Appservice.Security
{
    public class UserAppService: IUserAppService
    {
        private IUserDataService _userDataService;

        public UserAppService(IUserDataService userDataService)
        {
            this._userDataService = userDataService;
        }

        public async Task<IEnumerable<UserViewModel>> Get()
        {
            var answer = await this._userDataService.Get();

            return await Task.FromResult(answer.Select(x => new UserViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Login = x.Login,
                BirthDate = x.BirthDate,
                Country = x.Country,
                PersonId = x.PersonId,
                SubscribeDate = x.SubscribeDate,
                Password = string.Empty
            }));
        }

        public void Insert(IEnumerable<UserViewModel> models)
        {
            this._userDataService.Insert(models.Select(x => new UserModel
            {
                Id = Guid.NewGuid(),
                Login = x.Login,
                Name = x.Name,
                BirthDate = x.BirthDate,
                Country = x.Country,
                PersonId = x.PersonId,
                SubscribeDate = DateTime.UtcNow,
                Password = System.Text.Encoding.UTF8.GetBytes(x.Password)
            }
            ));
        }
    }
}
