using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherProrok.DAL.Model;

namespace WeatherProrok.Logic.Models
{
    public class CityModel
    {
        public Guid Id { get; set; }
        public string ProviderCityId { get; set; }
        public string Name { get; set; }

        public static CityModel ToCityModel(City model)
        {
            return new CityModel
            {
                Id = model.Id,
                Name = model.Name,
                ProviderCityId = model.CityProviderId
            };
        }
    }
}
