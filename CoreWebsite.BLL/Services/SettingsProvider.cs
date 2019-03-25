using CoreWebsite.BLL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CoreWebsite.BLL.Services
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly IConfiguration _configuration;

        public SettingsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int GetMaximumProductsCount => int.Parse(_configuration["MaximumProductsCount"]);

        public bool IsParametersLoggingEnabled => bool.Parse(_configuration["IsParametersLoggingEnabled"]);

        public string CacheDirectoryName => _configuration["CacheDirectoryName"];

        public int MaxCachedImagesCount => int.Parse(_configuration["MaxCachedImagesCount"]);

        public int CacheExpirationTime => int.Parse(_configuration["CacheExpirationTimeMilliseconds"]);
    }
}