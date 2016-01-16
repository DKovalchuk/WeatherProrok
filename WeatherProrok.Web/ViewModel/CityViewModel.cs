using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherProrok.Web.ViewModel
{
    public class CityViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "City")]
        public string Name { get; set; }
    }
}
