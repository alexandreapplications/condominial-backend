using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Exceptions.Application
{
    public class UserDoesntExistsException : ApplicationException
    {
        public UserDoesntExistsException(string userLogin)
            : this(userLogin, Guid.Empty)
        {
        }
        public UserDoesntExistsException(Guid userId)
            : this(string.Empty, userId)
        {
        }
        public UserDoesntExistsException(string userLogin, Guid userId)
            : base($"User doesn't exists (Login: { userLogin }) (Id: { userId })")
        {
            this.UserLogin = userLogin;
            this.UserId = userId;
        }

        private string UserLogin { get; set; }
        private Guid UserId { get; set; }

        public override IDictionary Data 
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "UserLogin", UserLogin },
                    { "UserId", UserId.ToString() }
                };
            }
        }
    }
}
