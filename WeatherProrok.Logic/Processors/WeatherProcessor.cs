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
        IWeatherProvider provider;
        IForecastProcessor forecastProcessor;
        IFactWeatherProcessor factProcessor;

        public WeatherProcessor(IWeatherProvider provider, IForecastProcessor forecastProcessor, IFactWeatherProcessor factProcessor)
        {
            this.provider = provider;
            this.forecastProcessor = forecastProcessor;
            this.factProcessor = factProcessor;
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
            Guid id;
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                id = repo.Add(new City { Id = Guid.NewGuid(), CityProviderId = cityId, Name = cityName });
            }

            ProcessWeatherForCity(id);

            return id;
        }

        private CurrentWeatherModel GetCurrentWeather(Guid cityId)
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

        private CurrentWeatherModel GetCurrentWeather(string city)
        {
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                var cityEntity = repo.GetSingleBy(c => c.Name == city);
                if (cityEntity == null)
                    throw new Exception("City not found");

                return GetCurrentWeather(cityEntity.Id);
            }
        }

        private async Task<CurrentWeatherModel> GetCurrentWeatherAsync(Guid cityId)
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

        private async Task<CurrentWeatherModel> GetCurrentWeatherAsync(string city)
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
            forecastProcessor.ProcessForecastForCity(cityId);
        }

        public bool ProcessWeather()
        {
            bool isUpdated = false;
            var cities = GetCities();
            foreach(var city in cities)
            {
                try
                {
                    if (ProcessWeatherForCity(city))
                        isUpdated = true;
                }
                catch(Exception ex)
                {
                    // TODO: Logging it
                    throw;
                }
            }
            return isUpdated;
        }

        private bool ProcessWeatherForCity(Guid cityId)
        {
            using (IBaseRepository<City> repo = new BaseRepository<City>())
            {
                var city = repo.GetById(cityId);
                if (city == null)
                    throw new Exception("City not found in database");

                return ProcessWeatherForCity(CityModel.ToCityModel(city));
            }
        }

        private bool ProcessWeatherForCity(CityModel city)
        {
            bool isUpdated = false;
            var currentWeather = GetCurrentWeather(city.Id);
            if (currentWeather != null)
            {
                var lastWeatherUpdateTime = DateTime.MinValue;
                using (IBaseRepository<FactWeather> weatherRepo = new BaseRepository<FactWeather>())
                {
                    var lastWeather = weatherRepo.GetBy(x => x.CityId == city.Id).Max(x => x.Updated);
                    if (lastWeather != null)
                        lastWeatherUpdateTime = lastWeather.Value;

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
                            var lastWeather = weatherRepo.GetAll().Max(x => x.Updated);
                            if (lastWeather != null)
                                lastWeatherUpdateTime = lastWeather.Value;

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

        public IEnumerable<WeatherModel> GetWeather()
        {
            var list = new List<WeatherModel>();
            foreach(var city in GetCities())
            {
                var current = factProcessor.GetCurrentWeather(city.Id);
                var forecast = forecastProcessor.GetForecastForCity(city.Id);

                list.Add(new WeatherModel
                {
                    City = city,
                    Current = current,
                    Forecast = forecast
                });
            }

            return list;
        }

        private IEnumerable<CityModel> GetCities()
        {
            using (IBaseRepository<City> cityRepo = new BaseRepository<City>())
            {
                return cityRepo.GetAll().ToList().Select(x => x.ToCityModel());
            }
        }
    }
}
