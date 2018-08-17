/*
 * ©Copyright 2018 Dell, Inc., All Rights Reserved.
 * This material is confidential and a trade secret.  Permission to use this
 * work for any purpose must be obtained in writing from Dell, Inc.
 */
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DirectoryServiceAPI.Models;
using System.Threading.Tasks;
using DirectoryServiceAPI.Services;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;

namespace DirectoryServiceAPI
{
    public class Startup
    {
        private DatabaseSettings dbSettings;
        private AppSettings appSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            InitializeConfiguration();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton(dbSettings);
            services.AddSingleton(appSettings);
            //  Injection Config
            services.AddSingleton<IDataAccess, DataAccess>();
            services.AddSingleton<IRequestHandler, RequestHandler>();

            // Code to generate OpenAPI documentation for swagger
            // Comment out this on production
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Core Api", Description = "Swagger Core Api" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();
            app.UseMvc();

            // Code to generate OpenAPI documentation for swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Api");
            }
            );
            app.UseCors("CorsPolicy");

        }

        private void InitializeConfiguration()
        {
            dbSettings = DatabaseSettings.InitializeSettings(Configuration);
            appSettings = AppSettings.InitializeSettings(Configuration);
        }
    }
}
