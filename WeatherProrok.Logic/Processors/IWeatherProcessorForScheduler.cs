using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Logic.Processors
{
    public interface IWeatherProcessorForScheduler
    {
        bool ProcessWeather();
        IEnumerable<WeatherModel> GetWeather();
    }
}
