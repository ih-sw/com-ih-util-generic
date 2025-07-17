using System;

namespace com.ih.util.generic.InternetPublicAddress.Domain
{
    [Serializable]
    public class IpAddressLocationDto
    {
        public string CountryName { get; set; }
        public string FederationUnitName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CompanyProvider { get; set; }
    }
}