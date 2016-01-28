using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Web.Store
{
    public interface ISearchCityStore
    {
        void AddToStore(IEnumerable<SearchCityModel> searchcities);
        IEnumerable<SearchCityModel> GetFromStore(Expression<Func<SearchCityModel, bool>> expression);
        void RemoveFromStore(SearchCityModel searchCity);
        void RemoveFromStore(IEnumerable<SearchCityModel> searchCities);
        void ClearStore();
    }
}