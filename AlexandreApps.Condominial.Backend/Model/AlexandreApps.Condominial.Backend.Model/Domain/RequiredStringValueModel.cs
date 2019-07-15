using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Domain
{
    /// <summary>
    /// Model that represents a single obrigatory string
    /// </summary>
    public class RequiredStringValueModel
    {
        /// <summary>
        /// String value
        /// </summary>
        [Required]
        public string value { get; set; }
    }
}
