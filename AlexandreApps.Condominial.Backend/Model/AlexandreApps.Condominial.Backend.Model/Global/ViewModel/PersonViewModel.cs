using AlexandreApps.Condominial.Backend.Model.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Global.ViewModel
{
    public class PersonViewModel : IViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string Name { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentType { get; set; }
        public string PreferedLanguage { get; set; }
        public IList<AddressViewModel> Addresses { get; set; }
        public IList<ContactViewModel> Contacts { get; set; }
        public IList<NoteViewModel> Notes { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
