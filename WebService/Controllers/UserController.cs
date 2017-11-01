
  
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
    public class UserController : Controller
    {
        private readonly IDataService _dataService;
        public UserController(IDataService dataService) {
            this._dataService = dataService;
        }
        // GET api/user
        [HttpGet]
        public IEnumerable<User> Get(int page = 0, int pageSize = 50)
        {
            return _dataService.GetUserRepository().Get(page, pageSize, orderBy: x => x.OrderBy(p => p.Id));
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _dataService.GetUserRepository().GetByID(id);
        }

        public int Count() {
            return _dataService.GetUserRepository().Count();
        }

        
      }
}
