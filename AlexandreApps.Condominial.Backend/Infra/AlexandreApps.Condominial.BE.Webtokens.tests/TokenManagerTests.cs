using AlexandreApps.Condominial.Backend.Webtokens;
using System;
using System.Collections.Generic;
using Xunit;

namespace AlexandreApps.Condominial.BE.Webtokens.tests
{
    public class TokenManagerTests
    {
        private string key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

        public class Test
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        Test[] testClass = new Test[]
            {
                new Test{ id="0", name="Teste" },
                new Test{ id="1", name="Teste 1" },
                new Test{ id="2", name="Teste 2" }
            };




        [Fact(DisplayName = "TokenManager -> CreateToken")]
        public void CreateToken()
        {
            var token = TokenManager.GenerateToken(key, testClass);

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[0-9a-zA-Z]*\.[0-9a-zA-Z]*\.[0-9a-zA-Z-_]*$");

            Assert.NotNull(token);

            Assert.Matches(regex, token);
        }

        [Fact(DisplayName = "TokenManager -> Consume")]
        public void Consume()
        {
            var token = TokenManager.GenerateToken(key, testClass);

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[0-9a-zA-Z]*\.[0-9a-zA-Z]*\.[0-9a-zA-Z-_]*$");

            Assert.NotNull(token);

            Assert.Matches(regex, token);

            var answer = TokenManager.GetToken<Test[]>(token);

            var t1 = Newtonsoft.Json.JsonConvert.SerializeObject(testClass);
            var t2 = Newtonsoft.Json.JsonConvert.SerializeObject(answer);

            Assert.Equal(t1, t2);
        }
    }
}
