using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public AnswerRepository(DataService ds)
        {
            this.ds = ds;
            this.count = this.dbSet.Distinct().Count(e => e.postTypeId == 2);
        }
        public override Task<List<Post>> Get(int page = 0, int pageSize = 50,
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null)
        {
            IQueryable<Post> query = this.dbSet;

            var res = (orderBy != null ? orderBy(query) : query)
                        .Distinct()
                        .Where(e => e.postTypeId == 2)
                        .Skip(page * pageSize)
                        .Take(pageSize - 1)
                        .ToListAsync();
            return res;
        }

        public Task<List<Post>> GetByUser(int userId,
                    Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null)
        {
            IQueryable<Post> query = this.dbSet;

            var res = (orderBy != null ? orderBy(query) : query)
                        .Distinct()
                        .Where(e => e.postTypeId == 2 && e.userId == userId)
                        .ToListAsync();
            return res;
        }
        
        public new int Count() {
            return (this.count);
        }
    }
}