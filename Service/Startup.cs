﻿using System.IO;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Service.Start;

namespace Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string AllowAllOriginsPolicy = "AllowAll";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //register services
            services.AddMvc();
            MappingConfig.ConfigureServices(services);
            RepositoryConfig.ConfigureServices(services);
            EntityFrameworkConfig.ConfigureServices(services, Configuration);

            services.AddCors(o => o.AddPolicy(AllowAllOriginsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //use default json contract resolver due to prevent lowercase transformation
            services.AddMvc().AddJsonOptions(p=>p.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(AllowAllOriginsPolicy);
            }

            app.UseMvc();
            
            EntityFrameworkConfig.Configure(app, env);
        }
    }
}
