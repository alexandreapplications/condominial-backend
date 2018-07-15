using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandreApps.Condominial.Backend.Model.Global.ViewModel
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ComplementaryInformation { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
