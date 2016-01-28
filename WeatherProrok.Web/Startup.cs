using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherProrok.Web.Store;
using WeatherProrok.Logic.Processors;
using Microsoft.AspNet.SignalR;
using WeatherProrok.Logic.Repositories;
using WeatherProrok.Logic.Providers;

namespace WeatherProrok.Web
{
    public class Startup
    {
        WeatherProcessorScheduler scheduler = null;

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddSingleton<ISearchCityStore, SearchCityStore>();
            services.AddSingleton<IWeatherProcessor, WeatherProcessor>();
            services.AddSingleton<IFactWeatherProcessor, FactWeatherProcessor>();
            services.AddSingleton<IForecastProcessor, ForecastProcessor>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddSingleton<IWeatherProcessorForScheduler, WeatherProcessor>();
            services.AddSingleton<IWeatherProvider, GismeteoProvider>();

            services.AddSignalR();

            services.AddMvc(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseWebSockets();

            app.UseSignalR(); 

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            scheduler = new WeatherProcessorScheduler(new Logic.Processors.WeatherProcessor(new Logic.Providers.GismeteoProvider(), new Logic.Processors.ForecastProcessor(), new FactWeatherProcessor()), new ForecastProcessor());
        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }
    }
}
