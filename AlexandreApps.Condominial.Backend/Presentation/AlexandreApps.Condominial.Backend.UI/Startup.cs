﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexandreApps.Condominial.Backend.Appservice.Domain;
using AlexandreApps.Condominial.Backend.Model.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace AlexandreApps.Condominial.Backend.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string ApplicationVersion { get; set; } = "0.0.0 alpha";
        public string ApplicationName { get; set; } = "Condominial Backend";


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c => c.SwaggerDoc(ApplicationVersion, new Info { Title = ApplicationName, Version = ApplicationVersion }));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var settingsAppService = new SettingsAppService(Configuration);

            //DependencyInjection _dependency = new DependencyInjection();

            //_dependency.Injection(services);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = settingsAppService.Settings.SecurityToken.Issuer,
                    ValidAudience = settingsAppService.Settings.SecurityToken.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(settingsAppService.Settings.SecurityToken.WebtokeyKeyData)
                };

            });

            DependencyBuilder.Build(services, Configuration, settingsAppService);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{ApplicationVersion}/swagger.json", ApplicationName));

            app.UseAuthentication();
        }
    }
}
