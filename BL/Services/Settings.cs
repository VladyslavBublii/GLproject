using BL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BL.Services
{
    public class Settings : ISettings
    {
        private readonly IServiceProvider _serviceProvider;

        public Settings(IServiceProvider serviceProvider) 
        { 
            _serviceProvider = serviceProvider;
        }

        public string GetUrl()
        {
            string stringUrl;
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();

            //ConfigurationBinder.Bind(configuration.GetValue("ScopeUrls:GoogleDrive");

            return configuration.GetValue<string>("ScopeUrls:GoogleDrive");
        }
    }
}
