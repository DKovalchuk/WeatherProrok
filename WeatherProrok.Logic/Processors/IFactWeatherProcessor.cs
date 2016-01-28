using System;
using System.Collections.Generic;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Logic.Processors
{
    public interface IFactWeatherProcessor
    {
        CurrentWeatherModel GetCurrentWeather(Guid cityId);
        IEnumerable<CurrentWeatherModel> GetCurrentWeatherForAllCities();
        IEnumerable<CurrentWeatherModel> GetWeatherForCity(Guid cityId);
    }
}