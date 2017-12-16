﻿using System;
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

        public Task<List<int>> GetAnswersToPost(int id)
        {
            var query = this.dbSet;

            var res = query
                .Distinct()
                .Where(e => e.parentId == id)
                .OrderByDescending(e => e.score)
                .Select(e => e.Id)
                .ToListAsync();
            return res;
        }
    }
}