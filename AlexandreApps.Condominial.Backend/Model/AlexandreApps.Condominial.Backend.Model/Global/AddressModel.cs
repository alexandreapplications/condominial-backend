using AlexandreApps.Condominial.Backend.Model.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Global
{
    public class AddressModel: IRecordId
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ComplementaryInformation { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        [Required]
        public DateTime IncludeDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [Required]
        public Guid LastUser { get; set; }
    }
}
