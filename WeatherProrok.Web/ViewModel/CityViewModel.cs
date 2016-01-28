using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Web.ViewModel
{
    public class CityViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "City")]
        public string Name { get; set; }

        public static CityViewModel ToCityViewModel(CityModel model)
        {
            return new CityViewModel
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
