using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherProrok.DAL.Model;
using WeatherProrok.DAL.Repository;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Logic.Processors
{
    public class ForecastProcessor : IForecastProcessor
    {
        IFactWeatherProcessor factProcessor;

        public ForecastProcessor(IFactWeatherProcessor factProcessor)
        {
            this.factProcessor = factProcessor;
        }

        public ForecastProcessor()
        {
            factProcessor = new FactWeatherProcessor();
        }

        public void ProcessForecastForAllCities()
        {
            IEnumerable<Guid> cities;
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                cities = repo.GetAll().Select(x => x.Id).ToList();
            }

            foreach (var city in cities)
                ProcessForecastForCity(city);
        }

        public void ProcessForecastForCity(Guid cityId)
        {
            var factWeather = factProcessor.GetWeatherForCity(cityId);
            if(factWeather != null && factWeather.Any())
            {
                var lastWeather = factWeather.OrderByDescending(x => x.CurrentDateTime);
                try
                {
                    var lastWeatherItem = lastWeather.First();
                    var lastDateTime = lastWeatherItem.CurrentDateTime;

                    var threeHoursTrend = ThreeHoursTrend(lastWeather);
                    var sixHoursTrend = SixHoursTrend(lastWeather);

                    using (IBaseRepository<ForecastWeather> repo = new BaseRepository<ForecastWeather>())
                    {
                        repo.Add(new ForecastWeather
                        {
                            CityId = cityId,
                            ForecastTo = DateTime.Now,
                            Humidity = 0,
                            Id = Guid.NewGuid(),
                            ThreeHourTemp = lastWeatherItem.Temp + (int)threeHoursTrend,
                            SixHourTemp = lastWeatherItem.Temp + (int)sixHoursTrend
                        });
                    } 
                }
                catch(Exception)
                { }
            }
        }

        private decimal ThreeHoursTrend(IEnumerable<Models.CurrentWeatherModel> model)
        {
            if (model.Count() < 2 || (DateTime.Now.Hour - model.First().CurrentDateTime.Hour) > 3)
                throw new Exception("No weather data");

            return (decimal)(model.First().Temp - model.Skip(1).First().Temp);
        }

        private decimal SixHoursTrend(IEnumerable<Models.CurrentWeatherModel> model)
        {
            /*if (model.Count() < 3 || (DateTime.Now.Hour - model.First().CurrentDateTime.Hour) > 3)
                throw new Exception("No weather data");*/

            return ThreeHoursTrend(model) * 2;
        }

        /*private decimal NineHoursTrend(IEnumerable<Models.CurrentWeatherModel> model)
        {
            if (model.Count() < 4 || (DateTime.Now.Hour - model.First().CurrentDateTime.Hour) > 3)
                throw new Exception("No weather data");
        }

        private decimal ElevenHoursTrend(IEnumerable<Models.CurrentWeatherModel> model)
        {
            if (model.Count() < 5 || (DateTime.Now.Hour - model.First().CurrentDateTime.Hour) > 3)
                throw new Exception("No weather data");
        }*/

        public ForecastWeatherModel GetForecastForCity(Guid cityId)
        {
            using (IBaseRepository<ForecastWeather> repo = new BaseRepository<ForecastWeather>())
            {
                return ForecastWeatherModel.ToForecastWeatherModel(repo.GetBy(x => x.CityId == cityId).OrderByDescending(x => x.ForecastTo).FirstOrDefault());
            }
        }

        public IEnumerable<ForecastWeatherModel> GetForecastForAllCities()
        {
            var forecasts = new List<ForecastWeatherModel>();
            IEnumerable<Guid> cities;
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                cities = repo.GetAll().Select(x => x.Id);
            }

            foreach (var city in cities)
                forecasts.Add(GetForecastForCity(city));

            return forecasts;
        }
    }
}
