using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.IO;
using DirectoryServiceAPI.Services;
using System.Net.Http;
using Serilog;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;

namespace ServiceTest
{
    [SetUpFixture]
    public class Startup
    {
        public static HttpClient Client;
        private static IConfiguration Configuration;

        [OneTimeSetUp]
        public void Initialize()
        {

            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.Console()
                .CreateLogger();

            Client = InitializeServer();
        }

        public static HttpClient InitializeServer()
        {
            TestServer server = new TestServer(new WebHostBuilder()
                   .ConfigureTestServices(s => s.AddSingleton<IADFactory, ADFactory>())
                   .ConfigureTestServices(s => s.AddSingleton<IMicrosoftGraphClient, MicrosoftGraphClient>())
                   .ConfigureTestServices(s => s.AddSingleton<IMicrosoftGraphService, MicrosoftGraphService>())
                   .UseStartup<DirectoryServiceAPI.Startup>()
                   .UseConfiguration(Configuration));
            return server.CreateClient();
        }
    }
}
