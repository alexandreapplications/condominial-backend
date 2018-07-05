using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Security.ViewModels
{
    public class SubscribeViewModel
    {
        [Required, MinLength(10)]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string BirthDate { get; set; }
        public int Country { get; set; }
        [Required, MinLength(8)]
        public string PassWord { get; set; }
    }
}
