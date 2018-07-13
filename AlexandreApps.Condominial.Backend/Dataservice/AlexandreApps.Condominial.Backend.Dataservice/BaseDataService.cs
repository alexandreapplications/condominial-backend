using AlexandreApps.Condominial.Backend.Interfaces.AppService.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Dataservice
{
    public abstract class BaseDataService<T>
    {
        private ISettingsAppService _settingsAppService;
        private MongoClientSettings _clientSettings;

        public BaseDataService(ISettingsAppService settingsApp)
        {
            this._settingsAppService = settingsApp;
        }

        private MongoClientSettings clientSettings {
            get
            {
                if (this._clientSettings == null)
                {
                    this._clientSettings = MongoClientSettings.FromUrl(new MongoUrl(this._settingsAppService.Settings.MainConnectionString));

                    SslProtocols sslProtocols = SslProtocols.None;

                    if (!string.IsNullOrWhiteSpace(this._settingsAppService.Settings.MainSslProtocol) && Enum.TryParse<SslProtocols>(this._settingsAppService.Settings.MainSslProtocol, out sslProtocols))
                    {
                        this._clientSettings.SslSettings = new SslSettings() { EnabledSslProtocols = sslProtocols };
                    }
                }
                return this._clientSettings;
            }
        }

        protected IMongoDatabase GetMongoDatabase()
        {
            var mongoClient = new MongoClient(this.clientSettings);
            return mongoClient.GetDatabase("Condominial");
        }

        protected IMongoCollection<T> GetCollection()
        {
            return this.GetMongoDatabase().GetCollection<T>(CollectionName);
        }

        protected abstract string CollectionName { get; }
    }
}
