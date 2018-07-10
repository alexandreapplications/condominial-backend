using AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain;
using AlexandreApps.Condominial.Backend.Interfaces.AppService.Security;
using AlexandreApps.Condominial.Backend.Model.Security;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Dataservice.Security
{
    public class PasswordDataService : BaseDataService<PasswordModel>, IPasswordDataService
    {
        public PasswordDataService(ISettingsAppService settingsApp)
            : base(settingsApp)
        {
        }

        protected override string CollectionName => "User";

        public async Task<IList<PasswordModel>> Get(Guid id)
        {
            return await base.GetCollection().Find(new FilterDefinitionBuilder<PasswordModel>().Eq(x => x.Id, id)).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> Insert(IEnumerable<PasswordModel> models)
        {
            await GetCollection().InsertManyAsync(models);
            return models.Select(x => x.Id);
        }
    }
}
