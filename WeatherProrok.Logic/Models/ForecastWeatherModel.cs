using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherProrok.DAL.Model;

namespace WeatherProrok.Logic.Models
{
    public class ForecastWeatherModel
    {
        public Guid Id { get; set; }
        public DateTime ForecastTo { get; set; }
        public int ThreeHourTemp { get; set; }
        public int SixHourTemp { get; set; }
        public int NineHourTemp { get; set; }
        public int ElevenHourTemp { get; set; }

        public static ForecastWeatherModel ToForecastWeatherModel(ForecastWeather model)
        {
            if (model == null)
                return new ForecastWeatherModel();

            return new ForecastWeatherModel
            {
                ElevenHourTemp = model.ElevenHourTemp,
                ForecastTo = model.ForecastTo,
                Id = model.Id,
                NineHourTemp = model.NineHourTemp,
                SixHourTemp = model.SixHourTemp,
                ThreeHourTemp = model.ThreeHourTemp
            };
        }
    }
}
