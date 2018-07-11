using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Security.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Login { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}
