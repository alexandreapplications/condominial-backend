using AlexandreApps.Condominial.Backend.Model.Domain;
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
        Task<IEnumerable<Guid>> Insert(params T[] models);
        Task<IEnumerable<Guid>> Update(params T[] models);
        Task<IEnumerable<Guid>> Delete(params Guid[] ids);
    }
}
