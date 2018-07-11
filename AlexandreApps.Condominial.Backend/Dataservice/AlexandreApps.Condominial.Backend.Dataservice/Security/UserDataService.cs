using AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain;
using AlexandreApps.Condominial.Backend.Interfaces.DataService.Security;
using AlexandreApps.Condominial.Backend.Model.Security;
using AlexandreApps.Condominial.Backend.Model.Security.ViewModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Dataservice.Security
{
    public class UserDataService : BaseDataService<UserModel>, IUserDataService
    {
        public UserDataService(ISettingsAppService settingsApp)
            : base(settingsApp)
        {
        }

        protected override string CollectionName => "User";

        public async Task<IList<UserModel>> Get(Guid id)
        {
            return await base.GetCollection().Find(new FilterDefinitionBuilder<UserModel>().Eq(x => x.Id, id)).ToListAsync();
        }

        public async Task<IList<UserModel>> GetAll()
        {
            return await base.GetCollection().Find(FilterDefinition<UserModel>.Empty).ToListAsync();
        }

        public async Task<IList<UserModel>> GetByLogin(string login)
        {
            return await base.GetCollection().Find(new FilterDefinitionBuilder<UserModel>().Eq(x => x.Login, login)).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> Insert(params UserModel[] models)
        {
            return await this.Insert(models.AsEnumerable());
        }
        public async Task<IEnumerable<Guid>> Insert(IEnumerable<UserModel> models)
        {
            await GetCollection().InsertManyAsync(models);
            return models.Select(x => x.Id);
        }

        public async Task<IEnumerable<Guid>> Update(IEnumerable<UserModel> models)
        {
            foreach (var model in models)
            {
                var filterDefinition = new FilterDefinitionBuilder<UserModel>().Eq(x => x.Id, model.Id);
                await GetCollection().ReplaceOneAsync(filterDefinition, model);
            }
            return models.Select(x => x.Id);
        }

        public async Task<IEnumerable<Guid>> Delete(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var filterDefinition = new FilterDefinitionBuilder<UserModel>().Eq(x => x.Id, id);
                await GetCollection().FindOneAndDeleteAsync<UserModel>(filterDefinition);
            }
            return ids;
        }
    }
}
