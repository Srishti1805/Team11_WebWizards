using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContosoCrafts.WebSite
{
    /// <summary>
    /// The Startup class contains the configuration of all the services for the web application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Contructor for initializing the web apllication with the required configuration
        /// </summary>
        /// <param name = "configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // property representing the application configuration properties
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// It initialises all the services with its implementations.
        /// services (IServiceCollection): a list of services with service type & its implementation
        /// </summary>
        /// <param name = "services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddControllers();
            services.AddTransient<JsonFileProductService>();
            services.AddHttpContextAccessor();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// It checks for web host environment and configures all required application pipeline mechanisms.
        /// app (IApplicationBuilder): application configuration and mechanism helper
        /// env (IWebHostEnvironment): contains information about the hosting web environment
        /// </summary>
        /// <param name = "app"></param>
        /// <param name = "env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Response.Clear(); // Clear the response to ensure a clean slate
                    context.Response.Headers["Location"] = "/Error"; // Set the location header for the redirect
                    context.Response.StatusCode = 302; // Set status code for redirection
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();

                // endpoints.MapGet("/products", (context) => 
                // {
                //     var products = app.ApplicationServices.GetService<JsonFileProductService>().GetProducts();
                //     var json = JsonSerializer.Serialize<IEnumerable<Product>>(products);
                //     return context.Response.WriteAsync(json);
                // });
            });
        }
    }
}