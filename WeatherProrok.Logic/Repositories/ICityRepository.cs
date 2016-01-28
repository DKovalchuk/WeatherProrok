using System;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Logic.Repositories
{
    public interface ICityRepository
    {
        CityModel GetCity(Guid id);
    }
}