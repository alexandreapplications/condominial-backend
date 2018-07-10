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

        public async Task<IEnumerable<UserViewModel>> Get(Guid id)
        {
            var answer = await this._userDataService.Get(id);

            return await Task.FromResult(answer.Select(x => new UserViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Login = x.Login,
                BirthDate = x.BirthDate,
                Country = x.Country,
                PersonId = x.PersonId,
                SubscribeDate = x.SubscribeDate
            }));
        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            var answer = await this._userDataService.GetAll();

            return await Task.FromResult(answer.Select(x => new UserViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Login = x.Login,
                BirthDate = x.BirthDate,
                Country = x.Country,
                PersonId = x.PersonId,
                SubscribeDate = x.SubscribeDate
            }));
        }

        public async Task<IEnumerable<Guid>> Insert(params UserViewModel[] models)
        {
            foreach (var item in models)
                item.Id = Guid.NewGuid();

            return await this._userDataService.Insert(models.Select(x => new UserModel
            {
                Id = x.Id.Value,
                Login = x.Login,
                Name = x.Name,
                BirthDate = x.BirthDate,
                Country = x.Country,
                PersonId = x.PersonId,
                SubscribeDate = DateTime.UtcNow
            }
            ));
        }

        public async Task<IEnumerable<Guid>> Update(params UserViewModel[] models)
        {
            return await this._userDataService.Update(models.Select(x => new UserModel
            {
                Id = x.Id.Value,
                Login = x.Login,
                Name = x.Name,
                BirthDate = x.BirthDate,
                Country = x.Country,
                PersonId = x.PersonId,
                SubscribeDate = DateTime.UtcNow
            }));
        }

        public async Task<IEnumerable<Guid>> Delete(params Guid[] ids)
        {
            return await this._userDataService.Delete(ids);
        }

        public bool Subscribe(SubscribeViewModel model)
        {
            throw new NotImplementedException();
        }

        public string Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
