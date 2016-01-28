using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using WeatherProrok.Logic.Processors;
using System.Timers;
using WeatherProrok.Web.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using WeatherProrok.Web.ViewModel;

namespace WeatherProrok.Web
{
    public class WeatherProcessorScheduler
    {
        Timer timer = null;
        IWeatherProcessorForScheduler processor;
        IFactWeatherProcessor factProcessor;

        public WeatherProcessorScheduler(IWeatherProcessorForScheduler processor, IForecastProcessor forecastProcessor)
        {
            this.processor = processor;

            timer = new Timer(60 * 60 * 1000); // one hour
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            processor.ProcessWeather();
            forecastProcessor.ProcessForecastForAllCities();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Task.Run(() => {
                var connectionManager = SignalRConnectionManager.ConnectionManager;
                if (connectionManager != null)
                {
                    var weatherHub = connectionManager.GetHubContext<Weather>();
                    weatherHub.Clients.All.startUpdatingWeather();
                }
                var isUpdated = processor.ProcessWeather();
                if (isUpdated)
                {
                    if (connectionManager != null)
                    {
                        var weatherHub = connectionManager.GetHubContext<Weather>();

                        var model = processor.GetWeather().Select(x => WeatherEntityViewModel.ToWeatherEntityViewModel(x));
                        weatherHub.Clients.All.updateCurrentWeather(model);
                    }
                }
            });
        }
    }
}
