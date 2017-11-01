
  
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc;
  using DataAccessLayer;
  using WebService.Models;
  
  namespace WebService.Controllers
  {
    [Route("api/history")]
    public class HistoryController : Controller
    {
        private readonly IDataService _dataService;
        public HistoryController(IDataService dataService) {
            this._dataService = dataService;
        }
        // GET api/history
        [HttpGet]
        public IEnumerable<History> Get(int page = 0, int pageSize = 50)
        {
            return _dataService.GetHistoryRepository().Get(page, pageSize, orderBy: x => x.OrderBy(p => p.Id));
        }

        // GET api/history/5
        [HttpGet("{id}")]
        public History Get(int id)
        {
            return _dataService.GetHistoryRepository().GetByID(id);
        }

        public int Count() {
            return _dataService.GetHistoryRepository().Count();
        }

        
      }
}
