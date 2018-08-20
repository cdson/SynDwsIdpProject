using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceTest.DirectoryManagement
{
    //[TestFixture]
    //public class DirectoryManagementTest
    //{
    //    TestServer server;
    //    HttpClient client;

    //    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
    //          .SetBasePath(Directory.GetCurrentDirectory())
    //          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //          .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    //          .AddEnvironmentVariables()
    //          .Build();

    //    [SetUp]
    //    public void Initialize()
    //    {
    //        server = new TestServer(new WebHostBuilder()
    //                .UseStartup<DirectoryServiceAPI.Startup>()
    //                .UseConfiguration(Configuration));
    //        client = server.CreateClient();
    //    }

    //    [Test]
    //    public async Task Valid_GetSolutionProvidersForSKU()
    //    {
    //        //string sampleSku = "5487-523";
    //        //HttpResponseMessage Res = await client.GetAsync("solution?sku=" + sampleSku);
    //        //Assert.IsTrue(Res.IsSuccessStatusCode);
    //        //var tntResponse = Res.Content.ReadAsStringAsync().Result;

    //        //var objSolutionProvider = JsonConvert.DeserializeObject<List<SolutionProvider>>(tntResponse);
    //        ////ToDo : correct the assert below
    //        //objSolutionProvider.Should().HaveCountGreaterThan(0);
    //    }
    //}
}
