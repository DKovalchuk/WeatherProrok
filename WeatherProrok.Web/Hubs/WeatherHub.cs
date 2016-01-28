using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WeatherProrok.Logic.Processors;
using WeatherProrok.Web.ViewModel;

namespace WeatherProrok.Web.Hubs
{
    [HubName("weather")]
    public class Weather : Hub
    {
        IWeatherProcessorForScheduler processor; 

        public Weather(IWeatherProcessorForScheduler processor)
        {
            this.processor = processor;
        }

        public void Update()
        {
            var model = processor.GetWeather().Select(x => WeatherEntityViewModel.ToWeatherEntityViewModel(x));
            Clients.All.updateCurrentWeather(model);
        }

        public void StartUpdatingWeather()
        {
            Clients.All.startUpdatingWeather();
        }

        public override async Task OnConnected()
        {
            var model = processor.GetWeather().Select(x => WeatherEntityViewModel.ToWeatherEntityViewModel(x));
            Clients.Caller.updateCurrentWeather(model);
            await base.OnConnected();
        }
    }
}
