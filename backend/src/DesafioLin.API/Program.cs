using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace DesafioLin.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //var host = new Host()
            //    .UseKestrel()
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .Build();

            //host.Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.ConfigureKestrel((_, kestrelServerOptions) =>
                     {
                         // Set properties and call methods on options
                     });
                     webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                     webBuilder.UseIISIntegration();
                     //webBuilder.UseKestrel();
                     webBuilder.UseStartup<Startup>();
                 });
    }
}