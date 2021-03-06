﻿using AlexandreApps.Condominial.Backend.Model.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Security.ViewModels
{
    public class PasswordViewModel : IViewModel
    {
        [Required, EmailAddress]
        public string Login { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
