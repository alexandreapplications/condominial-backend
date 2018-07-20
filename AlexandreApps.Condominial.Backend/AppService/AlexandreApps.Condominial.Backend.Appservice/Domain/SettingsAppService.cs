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
            var token = configuration.GetSection("SecurityToken");
            Settings = new AppSettings
            {
                MainConnectionString = configuration.GetConnectionString("main"),
                SecurityToken = new AppSettings.SecurityTokenSettings
                {
                    MainSslProtocol = token.GetSection("ConnectionSslProtocol")["main"],
                    WebtokenKey = token.GetSection("WebtokenKey").Value,
                    Audience = token.GetSection("Audience").Value,
                    Issuer = token.GetSection("Issuer").Value,
                }
            };
        }
        public AppSettings Settings { get; set; }
    }
}
