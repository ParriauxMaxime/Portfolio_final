using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Models;

namespace DataAccessLayer.Repository
{
    public class CommentRepository : GenericReadableRepository<Comment>
    {
        DataService ds;
        public CommentRepository(DataService ds)
        {
            this.ds = ds;
        }
        public Task<List<Comment>> getByPosts(int id)
        {
            IQueryable<Comment> query = this.dbSet;

            var res = query
                        .Distinct()
                        .Where(e => e.PostId == id)
                        .ToListAsync();
                return res;
        }

        public Task<List<Comment>> GetByUser(int userId,
                    Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null)
        {
            IQueryable<Comment> query = this.dbSet;

            var res = (orderBy != null ? orderBy(query) : query)
                        .Distinct()
                        .Where(e => e.userId == userId)
                        .ToListAsync();
            return res;
        }
    }
}