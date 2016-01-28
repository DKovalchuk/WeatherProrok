using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WeatherProrok.Logic.Models;

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

        public DateTime ForecastDateTime { get; set; }
        public int ThreeHoursTrend { get; set; }
        public int SixHoursTrend { get; set; }
        public int NineHoursTrend { get; set; }
        public int ElevenHoursTrend { get; set; }

        public static WeatherEntityViewModel ToWeatherEntityViewModel(CurrentWeatherModel model)
        {
            var Cloudity = string.Empty;
            var Precipitations = string.Empty;

            switch(model.Cloudity)
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

            switch(model.Precipitation)
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

            return new WeatherEntityViewModel
            {
                CurrentDateTime = model.CurrentDateTime.ToString("dd MMMM yyyy hh:mm"),
                Name = model.City.Name,
                Id = model.City.Id,
                Humidity = model.Humidity,
                Temp = model.Temp,
                Cloudity = Cloudity,
                Precipitation = Precipitations
            };
        }

        public static WeatherEntityViewModel ToWeatherEntityViewModel(WeatherModel model)
        {
            var weather = ToWeatherEntityViewModel(model.Current);
            if (model.Forecast != null)
            {
                weather.ElevenHoursTrend = model.Forecast.ElevenHourTemp;
                weather.ForecastDateTime = model.Forecast.ForecastTo;
                weather.NineHoursTrend = model.Forecast.NineHourTemp;
                weather.SixHoursTrend = model.Forecast.SixHourTemp;
                weather.ThreeHoursTrend = model.Forecast.ThreeHourTemp;
            }

            return weather;
        }
    }
}
