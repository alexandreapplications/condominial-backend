using AlexandreApps.Condominial.Backend.Model.Domain;
using AlexandreApps.Condominial.Backend.Model.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Interfaces.AppService
{
    public interface ICrudAppService<T>
        where T: IViewModel
    {
        Task<IEnumerable<T>> Get(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<Guid>> Insert(UserModel user, params T[] models);
        Task<IEnumerable<Guid>> Update(UserModel user, params T[] models);
        Task<IEnumerable<Guid>> Delete(UserModel user, params Guid[] ids);
    }
}
