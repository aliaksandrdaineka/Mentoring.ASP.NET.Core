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
    }
}