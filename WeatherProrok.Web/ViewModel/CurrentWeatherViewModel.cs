using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherProrok.Web.ViewModel
{
    public class CurrentWeatherViewModel
    {
        [Display(Name = "Temperature")]
        public int Temp { get; set; }
        public int Humidity { get; set; }
        public string Cloudity { get; set; }
        public string Precipitation { get; set; }
        [Display(Name = "Update time")]
        public DateTime CurrentDateTime { get; set; }
    }
}
