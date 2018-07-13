using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Interfaces.DataService
{
    public interface ICrudDataService<T>
    {
        Task<IList<T>> Get(Guid id);
        Task<IList<T>> GetAll();
        Task<IEnumerable<Guid>> Insert(IEnumerable<T> models);
        Task<IEnumerable<Guid>> Insert(params T[] models);
        Task<IEnumerable<Guid>> Update(IEnumerable<T> models);
        Task<IEnumerable<Guid>> Delete(IEnumerable<Guid> ids);
    }
}
