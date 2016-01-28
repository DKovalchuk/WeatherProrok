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
    public class FactWeatherProcessor : IFactWeatherProcessor
    {
        public CurrentWeatherModel GetCurrentWeather(Guid cityId)
        {
            using (IBaseRepository<FactWeather> repo = new BaseRepository<FactWeather>())
            {
                try
                {
                    var weather = repo.GetBy(x => x.CityId == cityId).OrderBy(x => x.Updated).ToList().Last();
                    return new CurrentWeatherModel
                    {
                        Cloudity = (Models.Cloudity)weather.Cloudity,
                        CurrentDateTime = weather.Updated.Value,
                        Humidity = weather.Humidity,
                        Precipitation = (Models.Precipitation)weather.Precipitations,
                        Temp = weather.Temp,
                        City = CityModel.ToCityModel(weather.City)
                    };
                }
                catch(InvalidOperationException ex)
                {
                    throw new Exception("No weather found", ex);
                }
            }
        }

        public IEnumerable<CurrentWeatherModel> GetCurrentWeatherForAllCities()
        {
            var weathers = new List<CurrentWeatherModel>();
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                foreach(var city in repo.GetAll())
                {
                    if (city != null && city.FactWeathers != null && city.FactWeathers.Any())
                    {
                        var weather = city.FactWeathers.OrderByDescending(x => x.Updated).First();
                        weathers.Add(new CurrentWeatherModel
                        {
                            Cloudity = (Models.Cloudity)weather.Cloudity,
                            CurrentDateTime = weather.Updated.Value,
                            Humidity = weather.Humidity,
                            Precipitation = (Models.Precipitation)weather.Precipitations,
                            Temp = weather.Temp,
                            City = CityModel.ToCityModel(weather.City)
                        });
                    }
                }
            }

            return weathers;
        }

        public IEnumerable<CurrentWeatherModel> GetWeatherForCity(Guid cityId)
        {
            using (IBaseRepository<FactWeather> repo = new BaseRepository<FactWeather>())
            {
                return repo.GetBy(x => x.CityId == cityId).ToList().Select(x => new CurrentWeatherModel
                {
                    //City = CityModel.ToCityModel(x.City),
                    Cloudity = (Models.Cloudity)x.Cloudity,
                    CurrentDateTime = x.Updated.Value,
                    Humidity = x.Humidity,
                    Precipitation = (Models.Precipitation)x.Precipitations,
                    Temp = x.Temp
                });
            }
        }
    }
}
