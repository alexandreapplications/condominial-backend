using AlexandreApps.Condominial.Backend.Dataservice.Security;
using AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain;
using AlexandreApps.Condominial.Backend.Interfaces.DataService.Global;
using AlexandreApps.Condominial.Backend.Model.Global;
using AlexandreApps.Condominial.Backend.Model.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Dataservice.Global
{
    public class PersonDataService : BaseCrudDataService<PersonModel>, IPersonDataService
    {
        public PersonDataService(ISettingsAppService settingsApp)
            : base(settingsApp)
        {
        }

        protected override string CollectionName => "Person";
    }
}
