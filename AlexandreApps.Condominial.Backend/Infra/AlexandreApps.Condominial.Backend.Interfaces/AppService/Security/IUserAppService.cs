using AlexandreApps.Condominial.Backend.Model.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Interfaces.AppService.Security
{
    public interface IUserAppService
    {
        Task<IEnumerable<UserViewModel>> Get(Guid id);
        Task<IEnumerable<UserViewModel>> GetAll();
        Task<IEnumerable<Guid>> Insert(params UserViewModel[] models);
        Task<IEnumerable<Guid>> Update(params UserViewModel[] models);
        Task<IEnumerable<Guid>> Delete(params Guid[] ids);
        bool Subscribe(SubscribeViewModel model);
        string Login(LoginViewModel model);
    }
}
