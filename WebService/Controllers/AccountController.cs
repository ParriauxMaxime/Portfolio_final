
  
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc;
  using DataAccessLayer;
  using WebService.Models;
  
  namespace WebService.Controllers
  {
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IDataService _dataService;
        public AccountController(IDataService dataService) {
            this._dataService = dataService;
        }
        // GET api/account
        [HttpGet]
        public IEnumerable<Account> Get(int page = 0, int pageSize = 50)
        {
            return _dataService.GetAccountRepository().Get(page, pageSize, orderBy: x => x.OrderBy(p => p.Id));
        }

        // GET api/account/5
        [HttpGet("{id}")]
        public Account Get(int id)
        {
            return _dataService.GetAccountRepository().GetByID(id);
        }

        public int Count() {
            return _dataService.GetAccountRepository().Count();
        }

        
      }
}
