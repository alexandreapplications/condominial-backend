using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Security
{
    /// <summary>
    /// User Model
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        [Required, EmailAddress]
        public string Login { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [Required, MinLength(6)]
        public string Name { get; set; }
        /// <summary>
        /// Birth Date
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public int Country { get; set; }
        /// <summary>
        /// Person Id
        /// </summary>
        public string PersonId { get; set; }
        /// <summary>
        /// Password hash
        /// </summary>
        [Required]
        public byte[] Password { get; set; }
        /// <summary>
        /// Subscribing Date
        /// </summary>
        [Required]
        public DateTime SubscribeDate { get; set; }
    }
}
