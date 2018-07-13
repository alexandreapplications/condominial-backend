using AlexandreApps.Condominial.Backend.Model.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Global
{
    public class PersonModel : IRecordId
    {
        public Guid Id { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string Name { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentType { get; set; }
        public string PreferedLanguage { get; set; }
        public IList<AddressModel> Addresses { get; set; }
        public IList<ContactModel> Contacts { get; set; }
        public IList<NoteModel> Notes { get; set; }
        [Required]
        public DateTime IncludeDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [Required]
        public Guid LastUser { get; set; }
    }
}
