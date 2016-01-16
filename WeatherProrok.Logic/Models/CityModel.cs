using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherProrok.Logic.Models
{
    public class CityModel
    {
        public Guid Id { get; set; }
        public string ProviderCityId { get; set; }
        public string Name { get; set; }
    }
}
