using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;
using DirectoryServiceAPI.Models;
using DirectoryServiceAPI.Services;

namespace ServiceTest
{
    [SetUpFixture]
    public class Startup
    {
        public static IDataAccess DataAccess;
        [OneTimeSetUp]
        public void Initialize()
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

            DatabaseSettings dbSettings = InitializeDbSettings(configuration);
            AppSettings appSettings = InitializeAppSettings(configuration);
            DataAccess = new DataAccess(dbSettings, appSettings);
        }

        private DatabaseSettings InitializeDbSettings(IConfiguration configuration)
        {
            DatabaseSettings settings = new DatabaseSettings();
            configuration.Bind("Database", settings);

            return settings;
        }

        private AppSettings InitializeAppSettings(IConfiguration configuration)
        {
            AppSettings settings = new AppSettings();
            configuration.Bind("AppSettings", settings);

            return settings;
        }



    }
}
