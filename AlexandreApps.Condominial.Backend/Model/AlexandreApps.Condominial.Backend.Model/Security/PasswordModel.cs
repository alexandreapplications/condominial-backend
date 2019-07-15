using AlexandreApps.Condominial.Backend.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Security
{
    public class PasswordModel : IRecordId
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public byte[] Password { get; set; }
    }
}
