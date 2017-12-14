using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Models;

namespace DataAccessLayer.Repository
{
    public class UserRepository : GenericReadableRepository<User>
    {
        DataService ds;
        public UserRepository(DatabaseContext context, DataService ds) : base(context)
        {
            this.ds = ds;
        }
    }
}