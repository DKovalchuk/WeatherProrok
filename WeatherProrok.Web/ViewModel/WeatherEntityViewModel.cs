using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherProrok.Web.ViewModel
{
    public class WeatherEntityViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "City")]
        public string Name { get; set; }
        [Display(Name = "Temperature")]
        public int Temp { get; set; }
        public int Humidity { get; set; }
        public string Cloudity { get; set; }
        public string Precipitation { get; set; }
        [Display(Name = "Update time")]
        public string CurrentDateTime { get; set; }
    }
}
