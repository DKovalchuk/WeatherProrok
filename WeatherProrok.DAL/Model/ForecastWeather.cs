using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherProrok.DAL.Model
{
    public class ForecastWeather : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("City")]
        public Guid CityId { get; set; }
        public int Temp { get; set; }
        public int Humidity { get; set; }
        public DateTime ForecastTo { get; set; }

        public virtual City City { get; set; }
    }
}
