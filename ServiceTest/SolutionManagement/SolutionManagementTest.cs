using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using ServiceTest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DirectoryServiceAPI.Controllers;
using DirectoryServiceAPI.Models;
using DirectoryServiceAPI.Services;

namespace ServiceTest.SolutionManagement
{
    [TestFixture]
    public class SolutionManagementTest
    {
        TestServer server;
        HttpClient client;

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
              .AddEnvironmentVariables()
              .Build();

        [SetUp]
        public void Initialize()
        {
            server = new TestServer(new WebHostBuilder()
                    .UseStartup<DirectoryServiceAPI.Startup>()
                    .UseConfiguration(Configuration));
            client = server.CreateClient();
        }

        [Test]
        public async Task Valid_GetSolutionProvidersForSKU()
        {
            //string sampleSku = "5487-523";
            //HttpResponseMessage Res = await client.GetAsync("solution?sku=" + sampleSku);
            //Assert.IsTrue(Res.IsSuccessStatusCode);
            //var tntResponse = Res.Content.ReadAsStringAsync().Result;

            //var objSolutionProvider = JsonConvert.DeserializeObject<List<SolutionProvider>>(tntResponse);
            ////ToDo : correct the assert below
            //objSolutionProvider.Should().HaveCountGreaterThan(0);
        }
    }
}
