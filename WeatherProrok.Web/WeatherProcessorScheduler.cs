using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
//using WeatherProrok.Logic.Processors;
using System.Threading;

namespace WeatherProrok.Web
{
    public class WeatherProcessorScheduler
    {
        /*Timer timer = null;
        IWeatherProcessorForScheduler processor = null;

        public WeatherProcessorScheduler(IWeatherProcessorForScheduler processor)
        {
            this.processor = processor;

            timer = new Timer(60 * 60 * 1000, ,,); // one hour
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Task.Run(() => {
                var isUpdated = processor.ProcessWeather();
                if(isUpdated)
                {
                    //var weatherHub = GlobalHost
                }
            });
        }*/
    }
}
