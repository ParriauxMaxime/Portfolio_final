
  
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc;
  using DataAccessLayer;
  using WebService.Models;
  
  namespace WebService.Controllers
  {
    [Route("api/queryhistory")]
    public class QueryHistoryController : Controller
    {
        private readonly IDataService _dataService;
        public QueryHistoryController(IDataService dataService) {
            this._dataService = dataService;
        }
        // GET api/queryhistory
        [HttpGet]
        public IEnumerable<QueryHistory> Get(int page = 0, int pageSize = 50)
        {
            return _dataService.GetQueryHistoryRepository().Get(page, pageSize, orderBy: x => x.OrderBy(p => p.Id));
        }

        // GET api/queryhistory/5
        [HttpGet("{id}")]
        public QueryHistory Get(int id)
        {
            return _dataService.GetQueryHistoryRepository().GetByID(id);
        }

        public int Count() {
            return _dataService.GetQueryHistoryRepository().Count();
        }

        
      }
}
