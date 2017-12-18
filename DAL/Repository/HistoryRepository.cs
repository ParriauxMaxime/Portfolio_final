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
    public class HistoryRepository : GenericWritableRepository<History>
    {
        DataService ds;
        public HistoryRepository(DataService ds)
        {
            this.ds = ds;
        }

        public Task<History> findPost(int postId)
        {
            return this.dbSet.Distinct().Where(x => x.PostId == postId).FirstAsync();
        }

        public History deleteByPostId(int postId)
        {
            try
            {
                History entityToDelete = dbSet.Where(x => x.PostId == postId).First();
                Delete(entityToDelete);
                this.context.SaveChanges();
                return entityToDelete;
            }
            catch (Exception) {
                return null;
            }
        }
    }
}