using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Exceptions.Security
{
    public class InvalidTokenException: ApplicationException
    {
        public InvalidTokenException(string token, System.Exception innerException)
            : base ("Invalid token", innerException)
        {
            this.Token = token;
        }

        private readonly string Token;

        public override IDictionary Data
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "Token", Token }
                };
            }
        }
    }
}
