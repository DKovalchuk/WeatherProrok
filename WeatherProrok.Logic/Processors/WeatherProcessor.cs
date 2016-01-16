using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherProrok.DAL.Model;
using WeatherProrok.DAL.Repository;
using WeatherProrok.Logic.Models;
using WeatherProrok.Logic.Providers;
using WeatherProrok.Logic.Helpers;

namespace WeatherProrok.Logic.Processors
{
    public class WeatherProcessor : IWeatherProcessor, IWeatherProcessorForScheduler
    {
        IWeatherProvider provider = null;
        IForecastProcessor forecastProcessor = null;

        public WeatherProcessor(IWeatherProvider provider, IForecastProcessor forecastProcessor)
        {
            this.provider = provider;
            this.forecastProcessor = forecastProcessor;
        }

        public IEnumerable<SearchCityModel> SearchCity(string searchString)
        {
            return provider.SearchCity(searchString);
        }

        public async Task<IEnumerable<SearchCityModel>> SearchCityAsync(string searchString)
        {
            return await provider.SearchCityAsync(searchString);
        }

        public Guid AddCity(string cityId, string cityName)
        {
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                return repo.Add(new City { Id = Guid.NewGuid(), CityProviderId = cityId, Name = cityName });
            }
        }

        public CurrentWeatherModel GetCurrentWeather(Guid cityId)
        {
            var cityProviderId = string.Empty;
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                var city = repo.GetById(cityId);
                if (city == null)
                    throw new Exception("City not found");

                if (string.IsNullOrEmpty(city.CityProviderId))
                    throw new Exception(string.Format("CityProviderId not found for city '{0}'", city.Name));

                cityProviderId = city.CityProviderId;
            }

            return provider.GetCurrentWeatherByCityID(cityProviderId);
        }

        public CurrentWeatherModel GetCurrentWeather(string city)
        {
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                var cityEntity = repo.GetSingleBy(c => c.Name == city);
                if (cityEntity == null)
                    throw new Exception("City not found");

                return GetCurrentWeather(cityEntity.Id);
            }
        }

        public async Task<CurrentWeatherModel> GetCurrentWeatherAsync(Guid cityId)
        {
            var cityProviderId = string.Empty;
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                var city = repo.GetById(cityId);
                if (city == null)
                    throw new Exception("City not found");

                if (string.IsNullOrEmpty(city.CityProviderId))
                    throw new Exception(string.Format("CityProviderId not found for city '{0}'", city.Name));

                cityProviderId = city.CityProviderId;
            }

            return await provider.GetCurrentWeatherByCityIDAsync(cityProviderId);
        }

        public async Task<CurrentWeatherModel> GetCurrentWeatherAsync(string city)
        {
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                var cityEntity = repo.GetSingleBy(c => c.Name == city);
                if (cityEntity == null)
                    throw new Exception("City not found");

                return await GetCurrentWeatherAsync(cityEntity.Id);
            }
        }

        private void ProcessForecastForCity(Guid cityId)
        {

        }

        public bool ProcessWeather()
        {
            bool isUpdated = false;
            var cities = GetCities();
            foreach(var city in cities)
            {
                try
                {
                    var currentWeather = GetCurrentWeather(city.ProviderCityId);
                    if(currentWeather != null)
                    {
                        var lastWeatherUpdateTime = DateTime.MinValue;
                        using (IBaseRepository<FactWeather> weatherRepo = new BaseRepository<FactWeather>())
                        {
                            var lastWeather = weatherRepo.GetAll().OrderBy(x => x.Updated).LastOrDefault();
                            if (lastWeather != null)
                                lastWeatherUpdateTime = lastWeather.Updated;

                            if(currentWeather.CurrentDateTime > lastWeatherUpdateTime)
                            {
                                weatherRepo.Add(new FactWeather
                                {
                                    Id = Guid.NewGuid(),
                                    CityId = city.Id,
                                    Cloudity = (int)currentWeather.Cloudity,
                                    Precipitations = (int)currentWeather.Precipitation,
                                    Humidity = currentWeather.Humidity,
                                    Temp = currentWeather.Temp,
                                    Updated = currentWeather.CurrentDateTime
                                });

                                isUpdated = true;

                                ProcessForecastForCity(city.Id);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    // TODO: Logging it
                }
            }
            return isUpdated;
        }

        public async Task<bool> ProcessWeatherAsync()
        {
            bool isUpdated = false;
            var cities = GetCities();
            foreach (var city in cities)
            {
                try
                {
                    var currentWeather = await GetCurrentWeatherAsync(city.ProviderCityId);
                    if (currentWeather != null)
                    {
                        var lastWeatherUpdateTime = DateTime.MinValue;
                        using (IBaseRepository<FactWeather> weatherRepo = new BaseRepository<FactWeather>())
                        {
                            var lastWeather = weatherRepo.GetAll().OrderBy(x => x.Updated).LastOrDefault();
                            if (lastWeather != null)
                                lastWeatherUpdateTime = lastWeather.Updated;

                            if (currentWeather.CurrentDateTime > lastWeatherUpdateTime)
                            {
                                weatherRepo.Add(new FactWeather
                                {
                                    Id = Guid.NewGuid(),
                                    CityId = city.Id,
                                    Cloudity = (int)currentWeather.Cloudity,
                                    Precipitations = (int)currentWeather.Precipitation,
                                    Humidity = currentWeather.Humidity,
                                    Temp = currentWeather.Temp,
                                    Updated = currentWeather.CurrentDateTime
                                });

                                isUpdated = true;

                                ProcessForecastForCity(city.Id);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Logging it
                }
            }
            return isUpdated;
        }

        private IEnumerable<CityModel> GetCities()
        {
            using (IBaseRepository<City> cityRepo = new BaseRepository<City>())
            {
                return cityRepo.GetAll().Select(x => x.ToCityModel()).ToList();
            }
        }
    }
}
