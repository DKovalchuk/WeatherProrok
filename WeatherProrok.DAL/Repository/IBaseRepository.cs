using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeatherProrok.DAL.Model;

namespace WeatherProrok.DAL.Repository
{
    public interface IBaseRepository<T> : IDisposable
     where T : class, IEntity
    {
        WeatherContext Context { get; }
        Guid Add(T entity);
        void Delete(Guid id);
        void Delete(T entity);
        IQueryable<T> GetAll(bool asNoTracking = false);
        T GetById(Guid id, bool asNoTracking = false);
        IQueryable<T> GetBy(Expression<Func<T, bool>> expression, bool asNoTracking = false);
        T GetSingleBy(Expression<Func<T, bool>> expression, bool asNoTracking = false);
        void Update(T entity);
        void Dispose();
    }
}
