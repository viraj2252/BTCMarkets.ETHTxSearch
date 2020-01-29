using System;
using System.IO;
using BTCMarkets.ETHTxSearch.Core.Interfaces;
using BTCMarkets.ETHTxSearch.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using BTCMarkets.ETHTxSearch.Infrastructure.Api;
using AutoMapper;

namespace BTCMarkets.ETHTxSearch.Web.Helpers
{
    public static class DiConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();

            IConfigurationRoot configuration = GetConfiguration();
            var serviceProvider = services.BuildServiceProvider();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient(typeof(ILogger<>), (typeof(Logger<>)));

            services.AddTransient<IApiProxy>(s => new InfuraApiProxy(serviceProvider.GetService<ILogger<InfuraApiProxy>>(), EthEnvironment.Mainnet, "22b2ebe2940745b3835907b30e8257a4"));
            services.AddTransient<IApiService, InfuraApiService>();
            services.AddTransient<IBlockDataService, BlockDataService>();

            services.AddLogging();

        }

        public static IConfigurationRoot GetConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //Set configurations
            var di = Directory.GetCurrentDirectory();
            var configBuilder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(di)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
            var configuration = configBuilder.Build();
            return configuration;
        }
    }
}
