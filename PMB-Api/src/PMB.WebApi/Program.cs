using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PMB.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Migrations.Program.Main(new string[] { });

            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        
    }
}

