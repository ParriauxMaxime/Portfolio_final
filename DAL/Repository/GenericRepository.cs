using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;

namespace DataAccessLayer.Repository
{
    //Base class for repositoryCreation
    //Use GenericReadableRepository or GenericWritableRepository
    public abstract class GenericRepository<TEntity> where TEntity : class
    {
        internal DatabaseContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DatabaseContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
    }
    //Useless inheritance abstraction because C#
    public class GenericReadableRepository<TEntity> : GenericRepository<TEntity> where TEntity : class
    {

        public GenericReadableRepository(DatabaseContext context) : base(context)
        {

        }

        public virtual IEnumerable<TEntity> Get(int page = 0, int pageSize = 50,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = this.dbSet;
            return orderBy(query)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual int Count() {
            return dbSet.Count();
        }
    }

    //Useless inheritance abstraction because C#
    public class GenericWritableRepository<TEntity> : GenericReadableRepository<TEntity> where TEntity : class
    {
        public GenericWritableRepository(DatabaseContext context) : base(context)
        {

        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}