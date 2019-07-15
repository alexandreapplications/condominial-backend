using AlexandreApps.Condominial.Backend.Model.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Global
{
    public class NoteModel : IRecordId
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        [Required]
        public DateTime IncludeDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [Required]
        public Guid LastUser { get; set; }
    }
}
