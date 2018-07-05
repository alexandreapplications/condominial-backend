using AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain;
using AlexandreApps.Condominial.Backend.Interfaces.DataService.Security;
using AlexandreApps.Condominial.Backend.Model.Security;
using AlexandreApps.Condominial.Backend.Model.Security.ViewModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<IList<UserModel>> Get()
        {
            return await base.GetCollection().Find(FilterDefinition<UserModel>.Empty).ToListAsync();
        }

        public void Insert(IEnumerable<UserModel> models)
        {
            GetCollection().InsertManyAsync(models);
        }

        public void Update(IEnumerable<UserModel> models)
        {
            foreach (var model in models)
            {
                var filterDefinition = new FilterDefinitionBuilder<UserModel>().Eq(x => x.Id, model.Id);
                GetCollection().ReplaceOneAsync(filterDefinition, model);

            }
        }

        public void Delete(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var filterDefinition = new FilterDefinitionBuilder<UserModel>().Eq(x => x.Id, id);
                GetCollection().FindOneAndDeleteAsync<UserModel>(filterDefinition);
            }
        }
    }
}
