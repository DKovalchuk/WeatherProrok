using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherProrok.Logic.Models
{
    public class WeatherModel
    {
        public CityModel City { get; set; }
        public CurrentWeatherModel Current { get; set; }
        public ForecastWeatherModel Forecast { get; set; }
    }
}
