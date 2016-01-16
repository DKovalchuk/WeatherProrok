using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Logic.Processors
{
    public interface IWeatherProcessor
    {
        Guid AddCity(string cityId, string cityName);
        CurrentWeatherModel GetCurrentWeather(string city);
        CurrentWeatherModel GetCurrentWeather(Guid cityId);
        Task<CurrentWeatherModel> GetCurrentWeatherAsync(string city);
        Task<CurrentWeatherModel> GetCurrentWeatherAsync(Guid cityId);
        IEnumerable<SearchCityModel> SearchCity(string searchString);
        Task<IEnumerable<SearchCityModel>> SearchCityAsync(string searchString);
        bool ProcessWeather();
        Task<bool> ProcessWeatherAsync();
    }
}