using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Security
{
    public class PasswordModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public byte[] Password { get; set; }
    }
}
