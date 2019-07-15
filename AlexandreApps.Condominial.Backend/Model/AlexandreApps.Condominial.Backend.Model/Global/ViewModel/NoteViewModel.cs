using AlexandreApps.Condominial.Backend.Model.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Global.ViewModel
{
    public class NoteViewModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
