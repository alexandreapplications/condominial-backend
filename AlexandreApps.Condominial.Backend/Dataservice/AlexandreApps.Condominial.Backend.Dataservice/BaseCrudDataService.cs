using AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain;
using AlexandreApps.Condominial.Backend.Model.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Dataservice
{
    public abstract class BaseCrudDataService<T>: BaseDataService<T>
        where T: IRecordId
    {
        public BaseCrudDataService(ISettingsAppService settingsApp)
            : base(settingsApp)
        {
        }

        public async Task<IList<T>> Get(Guid id)
        {
            return await base.GetCollection().Find(new FilterDefinitionBuilder<T>().Eq(x => x.Id, id)).ToListAsync();
        }

        public async Task<IList<T>> GetAll()
        {
            return await base.GetCollection().Find(FilterDefinition<T>.Empty).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> Insert(params T[] models)
        {
            return await this.Insert(models.AsEnumerable());
        }
        public async Task<IEnumerable<Guid>> Insert(IEnumerable<T> models)
        {
            await GetCollection().InsertManyAsync(models);
            return models.Select(x => x.Id);
        }

        public async Task<IEnumerable<Guid>> Update(IEnumerable<T> models)
        {
            foreach (var model in models)
            {
                var filterDefinition = new FilterDefinitionBuilder<T>().Eq(x => x.Id, model.Id);
                await GetCollection().ReplaceOneAsync(filterDefinition, model);
            }
            return models.Select(x => x.Id);
        }

        public async Task<IEnumerable<Guid>> Delete(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var filterDefinition = new FilterDefinitionBuilder<T>().Eq(x => x.Id, id);
                await GetCollection().FindOneAndDeleteAsync<T>(filterDefinition);
            }
            return ids;
        }
    }
}
