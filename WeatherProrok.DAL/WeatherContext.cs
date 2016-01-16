using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherProrok.DAL.Model;

namespace WeatherProrok.DAL
{
    public class WeatherContext : DbContext
    {
        public DbSet<Cloudity> CloudityDict { get; set; }
        public DbSet<Precipitation> PrecipitationDict { get; set; }
        public DbSet<FactWeather> FactWeathers { get; set; }
        public DbSet<ForecastWeather> ForecastWeathers { get; set; }
        public DbSet<City> Cities { get; set; }

        public WeatherContext()
            :base("DefaultConnection")
        { }
    }
}
