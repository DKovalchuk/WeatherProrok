using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WeatherProrok.Logic.Models;

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
        public string CurrentDateTime { get; set; }

        public static CurrentWeatherViewModel ToCurrentWeatherViewModel(CurrentWeatherModel model)
        {
            var Cloudity = string.Empty;
            var Precipitations = string.Empty;

            switch (model.Cloudity)
            {
                case Logic.Models.Cloudity.CLOUD:
                    Cloudity = "Clouds";
                    break;
                case Logic.Models.Cloudity.HARD_CLOUD:
                    Cloudity = "Hard clouds";
                    break;
                case Logic.Models.Cloudity.LESS_CLOUD:
                    Cloudity = "Less clouds";
                    break;
                case Logic.Models.Cloudity.NONE:
                    Cloudity = "No clouds";
                    break;
                case Logic.Models.Cloudity.UNKNOWN:
                    Cloudity = "Unknown";
                    break;
            }

            switch (model.Precipitation)
            {
                case Logic.Models.Precipitation.HARD_RAIN:
                    Precipitations = "Hard rain";
                    break;
                case Logic.Models.Precipitation.HARD_SNOW:
                    Precipitations = "Hard snow";
                    break;
                case Logic.Models.Precipitation.LESS_RAIN:
                    Precipitations = "Less rain";
                    break;
                case Logic.Models.Precipitation.LESS_SNOW:
                    Precipitations = "Less snow";
                    break;
                case Logic.Models.Precipitation.NONE:
                    Precipitations = "No precipitations";
                    break;
                case Logic.Models.Precipitation.OTHER:
                    Precipitations = "Other";
                    break;
                case Logic.Models.Precipitation.RAIN:
                    Precipitations = "Rain";
                    break;
                case Logic.Models.Precipitation.SNOW:
                    Precipitations = "Snow";
                    break;
            }

            return new CurrentWeatherViewModel
            {
                CurrentDateTime = model.CurrentDateTime.ToString("yyyy-MM-dd hh:mm"),
                Humidity = model.Humidity,
                Temp = model.Temp,
                Cloudity = Cloudity,
                Precipitation = Precipitations
            };
        }
    }
}
