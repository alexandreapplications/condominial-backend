using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Exceptions.Application
{
    public class UserAlreadyExistsException : ApplicationException
    {
        public UserAlreadyExistsException(string userLogin)
            : this(userLogin, Guid.Empty)
        {
        }
        public UserAlreadyExistsException(Guid userId)
            : this(string.Empty, userId)
        {
        }
        public UserAlreadyExistsException(string userLogin, Guid userId)
            : base($"User already exists (Login: { userLogin }) (Id: { userId })")
        {
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
