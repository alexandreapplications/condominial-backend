using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Webtokens
{
    public class TokenManager
    {
        public static string GenerateToken<T>(string key, T information)
        {
            // Create Security key  using private key above:
            // not that latest version of JWT using Microsoft namespace instead of System
            var securityKey = new Microsoft
                .IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // Also note that securityKey length should be >256b
            // so you have to make sure that your private key has a proper length
            //
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256Signature);

            //  Finally create a Token
            var header = new JwtHeader(credentials);

            //Some PayLoad that contain information about the  customer
            var payload = new JwtPayload{
                { information.GetType().Name, Newtonsoft.Json.JsonConvert.SerializeObject(information) }
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token to String so you can use it in your client
            return handler.WriteToken(secToken);
        }

        public static T GetToken<T>(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var encToken = handler.ReadJwtToken(token).Payload.First().Value.ToString();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(encToken);
        }
    }
}
