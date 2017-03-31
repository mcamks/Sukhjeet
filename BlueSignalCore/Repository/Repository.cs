using BlueSignalCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlueSignalCore.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BlueSignalContext context;
        private DbSet<T> entities;

        public Repository(BlueSignalContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        //public T Get(Guid id)
        //{
        //    return entities.SingleOrDefault(s => s.ID == id);
        //}

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }
    }
}
