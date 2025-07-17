using System;
using Microsoft.Extensions.Configuration;

namespace com.ih.util.generic.Configuration
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration;

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static T Get<T>(string key)
        {
            if (_configuration != null)
            {
                var value = _configuration[key];

                if (value != null)
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }

            return default;
        }
    }
}