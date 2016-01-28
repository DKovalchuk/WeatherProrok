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
        public int ThreeHourTemp { get; set; }
        public int SixHourTemp { get; set; }
        public int NineHourTemp { get; set; }
        public int ElevenHourTemp { get; set; }
        public int Humidity { get; set; }
        public DateTime ForecastTo { get; set; }

        public virtual City City { get; set; }
    }
}
