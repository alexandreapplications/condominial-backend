using AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain;
using AlexandreApps.Condominial.Backend.Model.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Appservice.Domain
{
    public class SettingsAppService: ISettingsAppService
    {
        public SettingsAppService(IConfiguration configuration)
        {
            Settings = new AppSettings
            {
                MainConnectionString = configuration.GetConnectionString("main"),
                MainSslProtocol = configuration.GetSection("ConnectionSslProtocol")["main"],
                WebtokenKey = configuration.GetSection("WebtokenKey").Value
            };
        }
        public AppSettings Settings { get; set; }
    }
}
