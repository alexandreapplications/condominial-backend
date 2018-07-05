using AlexandreApps.Condominial.Backend.Model.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Interfaces.DataService.Security
{
    public interface IUserDataService
    {
        Task<IList<UserModel>> Get();
        void Insert(IEnumerable<UserModel> models);
        void Update(IEnumerable<UserModel> models);
        void Delete(IEnumerable<Guid> ids);
    }
}
