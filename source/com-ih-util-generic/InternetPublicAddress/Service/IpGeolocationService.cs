using System.Threading.Tasks;
using com.ih.util.generic.ConsumeServices.Service;
using com.ih.util.generic.InternetPublicAddress.Domain;

namespace com.ih.util.generic.InternetPublicAddress.Service
{
    public interface IIpGeolocationService
    {
        Task<IpAddressLocationDto> GetLocation(string ipAddress);
    }
    
    public class IpGeolocationService : IIpGeolocationService
    {
        private readonly IpAddressServiceConfigurationDto _iPAddressServiceConfiguration;
        private readonly string _serviceUrl = "https://api.ipgeolocation.io/ipgeo";

        public IpGeolocationService(IpAddressServiceConfigurationDto iPAddressServiceConfiguration)
        {
            _iPAddressServiceConfiguration = iPAddressServiceConfiguration;
        }

        public async Task<IpAddressLocationDto> GetLocation(string ipAddress)
        {
            try
            {
                IpAddressLocationDto response = null;

                using (var consumerRestService = new ConsumeRestService())
                {
                    var serviceResponse = await consumerRestService.Request<IpGeolocationResponseSM>(
                        type: HttpClientRequestMethod.GET,
                        url: _serviceUrl + "?apiKey=" + _iPAddressServiceConfiguration.ApiKey + "&ip=" + ipAddress);

                    if (serviceResponse != null)
                    {
                        response = new IpAddressLocationDto();

                        response.Latitude = serviceResponse.Response.latitude;
                        response.Longitude = serviceResponse.Response.longitude;
                        response.CountryName = serviceResponse.Response.country_name;
                        response.FederationUnitName = serviceResponse.Response.state_prov;
                        response.City = serviceResponse.Response.city;
                        response.ZipCode = serviceResponse.Response.zipcode;
                        response.CompanyProvider = serviceResponse.Response.isp;
                    }
                }

                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}