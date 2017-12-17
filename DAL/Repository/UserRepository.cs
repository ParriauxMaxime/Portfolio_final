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
    public class UserRepository : GenericReadableRepository<User>
    {
        DataService ds;
        public UserRepository(DataService ds)
        {
            this.ds = ds;
        }
    }
}