using System;

namespace com.ih.util.generic.InternetPublicAddress.Domain
{
    [Serializable]
    public class IpGeolocationResponseSM
    {
        public string country_name { get; set; }
        public string state_prov { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string isp { get; set; }
    }
}