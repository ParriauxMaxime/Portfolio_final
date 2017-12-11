using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Models;

namespace DataAccessLayer.Repository
{
    public class AnswerRepository : GenericReadableRepository<Post>
    {
        DataService ds;
        int count;
        public AnswerRepository(DatabaseContext context, DataService ds) : base(context)
        {
            this.ds = ds;
            this.count = this.dbSet.Distinct().Where(e => e.postTypeId == 1).Count();
        }
        public override IEnumerable<Post> Get(int page = 0, int pageSize = 50,
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null)
        {
            IQueryable<Post> query = this.dbSet;

            var res = (orderBy != null ? orderBy(query) : query)
                        .Distinct()
                        .Where(e => e.postTypeId == 2)
                        .Skip(page * pageSize)
                        .Take(pageSize - 1)
                        .ToList();
            return res;
        }

        public override Post GetByID(object id)
        {
            return dbSet.Find(id);
        }


        public override int Count() {
            return (this.count);
        }
    }
}