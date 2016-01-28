using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherProrok.Logic.Models
{
    public class CurrentWeatherModel
    {
        public int Temp { get; set; }
        public int Humidity { get; set; }
        public Cloudity Cloudity { get; set; }
        public Precipitation Precipitation { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public CityModel City { get; set; }
    }
}
