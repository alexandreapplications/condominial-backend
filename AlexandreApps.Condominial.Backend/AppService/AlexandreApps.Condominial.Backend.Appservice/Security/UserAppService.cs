using AlexandreApps.Condominial.Backend.Exceptions.Application;
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
        private IPasswordAppService _passwordAppService;

        public UserAppService(IUserDataService userDataService, IPasswordAppService passwordAppService)
        {
            this._userDataService = userDataService;
            this._passwordAppService = passwordAppService;
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

        public async Task<Guid> Subscribe(SubscribeViewModel model)
        {
            // Verify if login already exists
            var users = await _userDataService.GetByLogin(model.Email);
            var insertedId = Guid.NewGuid();

            if (users != null && users.Count > 0)
            {
                var user = users.First();

                if (!await this._passwordAppService.HasPassword(user.Id))
                {
                    throw new UserAlreadyExistsException(user.Login, user.Id);
                }
                insertedId = user.Id;
            }

            var now = DateTime.Now;
            var includedUsers = await _userDataService.Insert(new UserModel
            {
                Id = insertedId,
                Login = model.Email,
                Country = model.Country,
                BirthDate = model.BirthDate,
                Name = model.Name,
                Password = null,
                PersonId = null,
                SubscribeDate = now
            });
            if (includedUsers.Count() > 0)
            {
                await this._passwordAppService.SetPassword(new PasswordViewModel
                {
                    Login = model.Email,
                    Password = model.PassWord
                });
            }
            return insertedId;
        }
    }
}
