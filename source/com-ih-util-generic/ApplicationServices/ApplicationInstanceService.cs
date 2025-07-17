using Microsoft.Extensions.DependencyInjection;

namespace com.ih.util.generic.ApplicationServices
{
    public static class ApplicationInstanceService
    {
        public static IServiceCollection AddApplicationInstanceService(this IServiceCollection services, ApplicationInstanceServiceModel options)
        {
            return services;
        }
    }

    public class ApplicationInstanceServiceModel
    {
        public string ConnectionString { get; set; }
    }
}