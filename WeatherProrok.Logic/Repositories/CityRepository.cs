using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherProrok.DAL.Model;
using WeatherProrok.DAL.Repository;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Logic.Repositories
{
    public class CityRepository : ICityRepository
    {
        IBaseRepository<City> repo = null;

        public CityRepository(IBaseRepository<City> repository)
        {
            repo = repository;
        }

        public CityRepository()
        {
            repo = new BaseRepository<City>();
        }

        public CityModel GetCity(Guid id)
        {
            var city = repo.GetById(id);
            if (city == null)
                throw new Exception("City not found");
            return CityModel.ToCityModel(city);
        }
    }
}
