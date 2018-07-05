using AlexandreApps.Condominial.Backend.Model.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Interfaces.AppService.Security
{
    public interface IUserAppService
    {
        Task<IEnumerable<UserViewModel>> Get();
        void Insert(IEnumerable<UserViewModel> models);
        void Update(IEnumerable<UserViewModel> models);
        void Delete(IEnumerable<Guid> ids);
        bool Subscribe(SubscribeViewModel model);
        string Login(LoginViewModel model);
    }
}
