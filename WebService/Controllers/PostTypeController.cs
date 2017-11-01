
  
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc;
  using DataAccessLayer;
  using WebService.Models;
  
  namespace WebService.Controllers
  {
    [Route("api/posttype")]
    public class PostTypeController : Controller
    {
        private readonly IDataService _dataService;
        public PostTypeController(IDataService dataService) {
            this._dataService = dataService;
        }
        // GET api/posttype
        [HttpGet]
        public IEnumerable<PostType> Get(int page = 0, int pageSize = 50)
        {
            return _dataService.GetPostTypeRepository().Get(page, pageSize, null);
        }

        // GET api/posttype/5
        [HttpGet("{id}")]
        public PostType Get(int id)
        {
            return _dataService.GetPostTypeRepository().GetByID(id);
        }

        public int Count() {
            return _dataService.GetPostTypeRepository().Count();
        }

        
      }
}
