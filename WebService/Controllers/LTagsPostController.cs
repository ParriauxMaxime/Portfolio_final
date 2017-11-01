
  
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc;
  using DataAccessLayer;
  using WebService.Models;
  
  namespace WebService.Controllers
  {
    [Route("api/ltagspost")]
    public class LTagsPostController : Controller
    {
        private readonly IDataService _dataService;
        public LTagsPostController(IDataService dataService) {
            this._dataService = dataService;
        }
        // GET api/ltagspost
        [HttpGet]
        public IEnumerable<LTagsPost> Get(int page = 0, int pageSize = 50)
        {
            return _dataService.GetLTagsPostRepository().Get(page, pageSize);
        }

        // GET api/ltagspost/5
        [HttpGet("{id}")]
        public LTagsPost Get(int id)
        {
            return _dataService.GetLTagsPostRepository().GetByID(id);
        }

        public int Count() {
            return _dataService.GetLTagsPostRepository().Count();
        }

        
      }
}
