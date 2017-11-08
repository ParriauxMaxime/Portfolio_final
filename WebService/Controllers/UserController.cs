using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/user")]
    public class UserController : GenericReadableController<User>
    {
        public UserController(IDataService dataService) : base(dataService, dataService.GetUserRepository())
        {
        }
    }
}
