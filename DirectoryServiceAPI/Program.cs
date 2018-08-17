/*
 * ©Copyright 2018 Dell, Inc., All Rights Reserved.
 * This material is confidential and a trade secret.  Permission to use this
 * work for any purpose must be obtained in writing from Dell, Inc.
 */
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace DirectoryServiceAPI
{
    public class Program
    {
        // Setting up a custom configuration object for this service.
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            // Setup the Serilog logging system.
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("==== Solution Management API starting up! ====");
                BuildWebHost(args).Run();
                Log.Information("==== Solution Management API is exiting normally! ====");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "**** Solution Management API terminated unexpectedly! ****");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)    // Use the custom configuration
                .UseSerilog()                       // Use Serilog as our logging system.
                .Build();
    }
}
