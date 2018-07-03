using AlexandreApps.Condominial.Backend.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain
{
    public interface ISettingsAppService
    {
        AppSettings Settings { get; set; }
    }
}
