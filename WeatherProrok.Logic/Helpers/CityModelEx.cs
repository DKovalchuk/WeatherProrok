using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherProrok.DAL.Model;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Logic.Helpers
{
    public static class CityModelEx
    {
        public static CityModel ToCityModel(this City city)
        {
            return new CityModel
            {
                Id = city.Id,
                Name = city.Name,
                ProviderCityId = city.CityProviderId
            };
        }
    }
}
