using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Models;

namespace DataAccessLayer.Repository
{
    //Base class for repository creation
    //A repository is an container instance to old a Microsoft.EntityFrameworkCore.DbSet<TEntity>.
    //See documentation of https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    //Never use that, use GenericReadableRepository or GenericWritableRepository
    public abstract class GenericRepository<TEntity> where TEntity : GenericModel
    {
        internal DatabaseContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DatabaseContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public abstract TEntity GetByID(object id);
        public abstract IEnumerable<TEntity> Get(int page, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        public abstract int Count();
    }

    //Class for Readable repository creation
    //Provide Get, GetById and Count function on a Repository;
    public class GenericReadableRepository<TEntity> : GenericRepository<TEntity> where TEntity : GenericModel
    {
        public GenericReadableRepository(DatabaseContext context) : base(context)
        {

        }

        public override IEnumerable<TEntity> Get(int page = 0, int pageSize = 50,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = this.dbSet;
            if (orderBy != null)
            {
                return orderBy(query)
                                    .Skip(page * pageSize)
                                    .Take(pageSize - 1)
                                    .ToList();
            }
            else {
                return query.Skip(page * pageSize).Take(pageSize - 1).ToList();
            }

        }

        public override TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public override int Count()
        {
            return dbSet.Count();
        }
    }

    //Class for Readable repository creation
    //Provide Insert, Update and Delete function on a Repository;
    public class GenericWritableRepository<TEntity> : GenericReadableRepository<TEntity> where TEntity : GenericModel
    {
        public GenericWritableRepository(DatabaseContext context) : base(context)
        {

        }

        public virtual int Insert(TEntity entity)
        {
            int count = dbSet.Count();
            entity.Id = dbSet.Skip(count - 1).Take(1).ToList()[0].Id + 1;
            dbSet.Add(entity);
            this.context.SaveChanges();
            return entity.Id;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            this.context.SaveChanges();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            this.context.SaveChanges();
        }

        public virtual int Update(TEntity entityToUpdate)
        {
            int code = 200;
            if (context.Entry(entityToUpdate).State == EntityState.Detached) {
                dbSet.Attach(entityToUpdate);            
            }

            if (GetByID(entityToUpdate.Id) == null) {
                dbSet.Add(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Added;
                code = 201;
            } 
            else {
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            this.context.SaveChanges();
            context.Entry(entityToUpdate).State = EntityState.Detached;
            return code;            
        }
    }
}