using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ContosoCrafts.WebSite
{
    /// <summary>
    /// The Program class contains the entry point of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main method is the entry point for the application.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Build and run the host.
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// CreateHostBuilder is used to configure and build the application host.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // UseStartup configures the Startup class for the web application.
                    webBuilder.UseStartup<Startup>();
                });
    }
}