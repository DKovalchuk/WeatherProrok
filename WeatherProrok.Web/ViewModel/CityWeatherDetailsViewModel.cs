using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherProrok.Web.ViewModel
{
    public class CityWeatherDetailsViewModel
    {
        public CityViewModel City { get; set; }
        public IEnumerable<CurrentWeatherViewModel> Weather { get; set; }
    }
}
