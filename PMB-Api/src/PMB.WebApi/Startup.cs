using System;
using System.Net;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PMB.Abb.Models;
using PMB.Admin.Application;
using PMB.Dal;
using PMB.Dal.Bll;
using PMB.Dal.Bll.Authorization;
using PMB.Dal.Bll.Hubs;
using PMB.Dal.Exceptions;
using PMB.Integration.AllBestBets;
using PMB.Integration.Currencies;
using PMB.Integration.PositiveBet;
using PMB.Jobs;

namespace PMB.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ForkHub>();
            services.AddMemoryCache();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            services
                .AddDal(Configuration.GetConnectionString("ForksDb"))
                .AddBll()
                .AddPositiveIntegration()
                .AddCurrenciesIntegration(Configuration)
                //.AddTelegramIntegration(Configuration)
                .AddAbbIntegration(Configuration)
                .AddJobs(Configuration)
                .AddSwaggerGen()
                .AddMemoryCache()
                .UseAdminApi()
                .AddValidatorsFromAssembly(typeof(Startup).Assembly)
                .AddControllers()
                .AddFluentValidation(x => x.AutomaticValidationEnabled = true)
                .AddNewtonsoftJson();


            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddSignalR(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromMinutes(5);
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(5);
            });
            services.AddHealthChecks();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            PostgresConfig.ConfigureTypeMapping();
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/health");
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PMB Api"));

            app.UseExceptionHandler(err =>
            {
                err.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is NoDbParametersException e)
                    {
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new {Error = e.Message}));
                    }

                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                });
            });

            app.UseRouting();
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ForkHub>("/fork-hub", options =>
                {
                    options.Transports = HttpTransportType.WebSockets;
                });

                endpoints.MapHub<AbbForksHub>(Consts.AbbHubEndpoint, options =>
                {
                    options.Transports = HttpTransportType.WebSockets;
                });

                endpoints.MapControllers();
            });
        }
    }
}
