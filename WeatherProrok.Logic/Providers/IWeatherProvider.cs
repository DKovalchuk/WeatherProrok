using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Logic.Providers
{
    public interface IWeatherProvider
    {
        IEnumerable<CityModel> SearchCity(string search);
        CurrentWeatherModel GetCurrentWeatherByCity(string city);
        CurrentWeatherModel GetCurrentWeatherByCityID(string cityId);
        CurrentWeatherModel GetCurrentWeatherByCoordinates(double latitude, double longitude);

        Task<IEnumerable<CityModel>> SearchCityAsync(string search);
        Task<CurrentWeatherModel> GetCurrentWeatherByCityAsync(string city);
        Task<CurrentWeatherModel> GetCurrentWeatherByCityIDAsync(string cityId);
        Task<CurrentWeatherModel> GetCurrentWeatherByCoordinatesAsync(double latitude, double longitude);
    }
}
