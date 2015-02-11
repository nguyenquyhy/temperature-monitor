using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using Microsoft.AspNet.Diagnostics;
using Microsoft.Framework.ConfigurationModel;

namespace TemperatureMonitor.WebService
{
    public class Startup
    {
        public static Configuration Configuration { get; private set; }

        public Startup()
        {
            Configuration = new Configuration();
            Configuration
                .AddJsonFile("Config.json")
                .AddEnvironmentVariables();
        }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add EF services to the services container.
            //services.AddEntityFramework(Configuration);
            //.AddSqlServer()
            //.AddDbContext<ApplicationDbContext>();

            // Add Identity services to the services container.
            //services.AddDefaultIdentity<ApplicationDbContext, ApplicationUser, IdentityRole>(Configuration);

            services.AddMvc(Configuration);
            //    .Configure<MvcOptions>(options =>
            //{
            //    int position = options.OutputFormatters.FindIndex(f =>
            //                          f.Instance is JsonOutputFormatter);

            //    var settings = new JsonSerializerSettings()
            //    {
            //        ContractResolver = new CamelCasePropertyNamesContractResolver()
            //    };
            //    var formatter = new JsonOutputFormatter();
            //    formatter.SerializerSettings = settings;

            //    options.OutputFormatters.Insert(position, formatter);
            //});

            services.AddSignalR();

            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You need to add Microsoft.AspNet.Mvc.WebApiCompatShim package to project.json
            // services.AddWebApiConventions();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            // Configure the HTTP request pipeline.
            // Add the console logger.
            loggerfactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
            {
                app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                //app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorPage();
                //app.UseErrorHandler("/Home/Error");
            }

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });

            app.UseSignalR();
        }
    }
}
