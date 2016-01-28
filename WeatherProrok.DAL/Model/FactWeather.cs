using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherProrok.DAL.Model
{
    public class FactWeather : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("City")]
        public Guid CityId { get; set; }
        public int Temp { get; set; }
        public int Humidity { get; set; }
        public int Cloudity { get; set; }
        public int Precipitations { get; set; }
        /*[ForeignKey("Cloudity")]
        public Guid CloudityId { get; set; }
        [ForeignKey("Precipitation")]
        public Guid PrecipitationId { get; set; }*/
        public DateTime? Updated { get; set; }

        public virtual City City { get; set; }
        /*public virtual Cloudity Cloudity { get; set; }
        public virtual Precipitation Precipitation { get; set; }*/
    }
}
