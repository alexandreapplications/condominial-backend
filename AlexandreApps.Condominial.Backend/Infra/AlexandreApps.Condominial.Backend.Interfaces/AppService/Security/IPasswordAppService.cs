using AlexandreApps.Condominial.Backend.Model.Security.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Interfaces.AppService.Security
{
    public interface IPasswordAppService
    {
        Task<IEnumerable<Guid>> SetPassword(PasswordViewModel model);
        Task<UserViewModel> Login(LoginViewModel model);
        Task<bool> HasPassword(Guid id);
        Task<bool> ChangePassword(ChangePasswordViewModel id);
    }
}
