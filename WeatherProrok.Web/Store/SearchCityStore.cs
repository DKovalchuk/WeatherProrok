using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Web.Store
{
    public class SearchCityStore : ISearchCityStore
    {
        List<SearchCityModel> _store;

        public SearchCityStore()
        {
            _store = new List<SearchCityModel>();
        }

        public void AddToStore(IEnumerable<SearchCityModel> searchcities)
        {
            _store.AddRange(searchcities);
        }

        public void RemoveFromStore(SearchCityModel searchCity)
        {
            _store.Remove(searchCity);
        }

        public void RemoveFromStore(IEnumerable<SearchCityModel> searchCities)
        {
            foreach (var city in searchCities)
                RemoveFromStore(city);
        }

        public IEnumerable<SearchCityModel> GetFromStore(Expression<Func<SearchCityModel, bool>> expression)
        {
            return _store.AsQueryable().Where(expression);
        }

        public void ClearStore()
        {
            _store.Clear();
            _store = new List<SearchCityModel>();
        }
    }
}
