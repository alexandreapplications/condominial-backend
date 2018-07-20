using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Domain
{
    public class SecuredRequestModel<T>
    {
        [Required]
        public T Data { get; set; }
        [Required, RegularExpression(@"^[0-9a-zA-Z]*\.[0-9a-zA-Z]*\.[0-9a-zA-Z-_]*$")]
        public string Token { get; set; }

    }
}
