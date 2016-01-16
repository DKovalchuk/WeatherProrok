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
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class, IEntity
    {
        private WeatherContext context;
        public WeatherContext Context
        {
            get 
            {
                if (context == null)
                    context = new WeatherContext();

                return context;
            }
        }

        private readonly DbSet<T> _set;

        public BaseRepository()
        {
            _set = Context.Set<T>();
        }

        public BaseRepository(WeatherContext context)
        {
            this.context = context;
            _set = context.Set<T>();
        }

        public virtual Guid Add(T entity)
        {
            if (_set.Any(e => e.Id == entity.Id))
            {
                Update(entity);
            }
            else
            {
                entity = _set.Add(entity);
            }
            Context.SaveChanges();
            return entity.Id;
        }

        public virtual IQueryable<T> GetAll(bool asNoTracking = false)
        {
            if (asNoTracking)
                return (_set as IQueryable<T>).AsNoTracking<T>();

            return _set as IQueryable<T>;
        }

        public virtual T GetById(Guid id, bool asNoTracking = false)
        {
            return GetAll(asNoTracking).SingleOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> expression, bool asNoTracking = false)
        {
            return GetAll(asNoTracking).Where(expression);
        }

        public virtual T GetSingleBy(Expression<Func<T, bool>> expression, bool asNoTracking = false)
        {
            return GetAll(asNoTracking).FirstOrDefault(expression);
        }

        public virtual void Delete(T entity)
        {
            try
            {
                _set.Remove(entity);
            }
            catch (InvalidOperationException ex)
            {
                _set.Remove(GetById(entity.Id));
            }
            Context.SaveChanges();
        }

        public virtual void Delete(Guid id)
        {
            var entity = GetById(id);
            Delete(entity);
        }

        public virtual void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        
    }
}
