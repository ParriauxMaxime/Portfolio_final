using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Models;

namespace DataAccessLayer.Repository
{
    public class QuestionRepository : GenericReadableRepository<Post>
    {
        DataService ds;
        int count;
        public QuestionRepository(DataService ds)
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
                        .Where(e => e.postTypeId == 1)
                        .Skip(page * pageSize)
                        .Take(pageSize - 1)
                        .ToList();
            return res;
        }

        public Task<Post> GetRandom()
        {
            Random md = new Random();
            int rand = md.Next(0, this.count);
            return this.dbSet.Distinct()
                        .Where(e => e.postTypeId == 1)            
                        .OrderBy(o => o.Id)
                        .Skip(rand)
                        .Take(1)
                        .FirstAsync();
        }
        public new int Count()
        {
            return (this.count);
        }
    }
}