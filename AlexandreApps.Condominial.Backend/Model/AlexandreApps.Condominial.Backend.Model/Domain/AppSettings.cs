using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Domain
{
    public class AppSettings
    {
        public string MainConnectionString { get; set; }
        public SecurityTokenSettings SecurityToken { get; set; }
        public class SecurityTokenSettings
        {
            public string MainSslProtocol { get; set; }
            public string WebtokenKey { get; set; }
            public string Audience { get; set; }
            public string Issuer { get; set; }
            private byte[] _webtokeyKeyData;
            public byte[] WebtokeyKeyData
            {
                get
                {
                    if (_webtokeyKeyData == null || _webtokeyKeyData.Length == 0)
                    {
                        _webtokeyKeyData = Encoding.UTF8.GetBytes(this.WebtokenKey);
                    }
                    return _webtokeyKeyData;
                }
            }
        }
    }
}
