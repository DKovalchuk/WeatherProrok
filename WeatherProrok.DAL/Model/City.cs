using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherProrok.DAL.Model
{
    public class City : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string CityProviderId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FactWeather> FactWeathers { get; set; }
        public virtual ICollection<ForecastWeather> ForecastWeathers { get; set; }
    }
}
