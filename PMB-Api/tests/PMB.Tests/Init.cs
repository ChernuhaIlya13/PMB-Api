using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMB.Abb.Client;
using PMB.WebApi;

namespace PMB.Tests
{
    [TestClass]
    public class Init
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        private static WebApplicationFactory<Startup> _webApplicationFactory;
        
        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT",
                string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI")) ? "Local" : "Testing");

            Migrations.Program.Main(Array.Empty<string>());
            
            _webApplicationFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services =>
                        {
                            services.UseAbbProvider(x => x.Url = "http://localhost:6002");
                        }
                    ));
            
            ServiceProvider = _webApplicationFactory.Services;
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            _webApplicationFactory?.Dispose();
        } 
    }
}