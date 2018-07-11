using AlexandreApps.Condominial.Backend.Model.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Interfaces.AppService.Security
{
    public interface IPasswordDataService
    {
        Task<IList<PasswordModel>> Get(Guid id);
        Task<IEnumerable<Guid>> Insert(IEnumerable<PasswordModel> models);
        Task<IEnumerable<Guid>> Insert(params PasswordModel[] models);
    }
}
