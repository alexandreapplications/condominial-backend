using AlexandreApps.Condominial.Backend.Model.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Interfaces.DataService.Security
{
    public interface IUserDataService
    {
        Task<IList<UserModel>> Get(Guid id);
        Task<IList<UserModel>> GetAll();
        Task<IEnumerable<Guid>> Insert(IEnumerable<UserModel> models);
        Task<IEnumerable<Guid>> Update(IEnumerable<UserModel> models);
        Task<IEnumerable<Guid>> Delete(IEnumerable<Guid> ids);
    }
}
