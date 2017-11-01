
  
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc;
  using DataAccessLayer;
  using WebService.Models;
  
  namespace WebService.Controllers
  {
    [Route("api/tag")]
    public class TagController : Controller
    {
        private readonly IDataService _dataService;
        public TagController(IDataService dataService) {
            this._dataService = dataService;
        }
        // GET api/tag
        [HttpGet]
        public IEnumerable<Tag> Get(int page = 0, int pageSize = 50)
        {
            return _dataService.GetTagRepository().Get(page, pageSize, orderBy: x => x.OrderBy(p => p.Id));
        }

        // GET api/tag/5
        [HttpGet("{id}")]
        public Tag Get(int id)
        {
            return _dataService.GetTagRepository().GetByID(id);
        }

        public int Count() {
            return _dataService.GetTagRepository().Count();
        }

        
      }
}
