using AB.DAL.DbContexts;
using AB.DAL.GenericRepository.Abstract;
using AB.Entities.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AB.DAL.GenericRepository.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        readonly SqlDbContext _db;
        public Repository()
        {
            _db = new SqlDbContext(); 
        }
        public virtual  int Insert(T entity)
        {
           _db.Set<T>().Add(entity);
            return _db.SaveChanges();
        }

        public virtual int Update(T entity)
        {
            _db.Set<T>().Update(entity);
            return _db.SaveChanges();
        }
        public virtual int Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            return _db.SaveChanges();
        }

        public virtual T? GetById(int id)
        {
           return _db.Set<T>().Find(id);
           
        }
        public virtual ICollection<T>? GetAll(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return _db.Set<T>().ToList();
            }
            return _db.Set<T>().Where(filter).ToList();

        }

        public virtual IQueryable<T>? GetAllInclude(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] include)
        {

            // var result = db.GetAllInclude(p=>p.ProductName=="xyz" ,p=>p.Category,p=>Suplier);
            IQueryable<T> query;
            if (filter == null)
            {
                query = _db.Set<T>();
            }
            else
            {
                query = _db.Set<T>().Where(filter);
            }

            return include.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public virtual T? Get(Expression<Func<T, bool>> filter)
        {
           return _db.Set<T>().FirstOrDefault(filter);
        }
    }
}
