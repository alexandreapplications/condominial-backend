using AlexandreApps.Condominial.Backend.Appservice.Domain;
using AlexandreApps.Condominial.Backend.Appservice.Security;
using AlexandreApps.Condominial.Backend.Dataservice.Security;
using AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain;
using AlexandreApps.Condominial.Backend.Interfaces.AppService.Security;
using AlexandreApps.Condominial.Backend.Interfaces.DataService.Security;
using AlexandreApps.Condominial.Backend.Model.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.UI
{
    public class DependencyBuilder
    {
        public static void Build(IServiceCollection services, IConfiguration configuration)
        {
            services = services ?? throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ISettingsAppService, SettingsAppService>(x => new SettingsAppService(configuration));

            services.AddSingleton<IUserDataService, UserDataService>();

            services.AddSingleton<IUserAppService, UserAppService>();
        }
    }
}
