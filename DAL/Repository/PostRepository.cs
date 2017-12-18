using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;

namespace DataAccessLayer.Repository
{
    public class PostRepository : GenericReadableRepository<Post>
    {
        DataService ds;

        public PostRepository(DataService ds)
        {
            this.ds = ds;
        }

        public Task<List<int>> GetAnswersToPost(int id, int page = 0, int pageSize = 50)
        {
            var query = this.dbSet;

            var res = query
                .Distinct()
                .Where(e => e.parentId == id)
                .OrderByDescending(e => e.score)
                .Select(e => e.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return res;
        }

        public Task<int> GetNumberAnswerToPost(int id) {
            var res = this.dbSet
                .Distinct()
                .Where(e => e.parentId == id)
                .CountAsync();
            return res;
        }

        public Task<List<Post>> GetByUser(int userId,
                    Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null)
        {
            IQueryable<Post> query = this.dbSet;

            var res = (orderBy != null ? orderBy(query) : query)
                        .Distinct()
                        .Where(e => e.userId == userId)
                        .ToListAsync();
            return res;
        }
    }
}