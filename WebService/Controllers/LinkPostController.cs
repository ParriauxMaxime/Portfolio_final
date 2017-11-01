
  
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc;
  using DataAccessLayer;
  using WebService.Models;
  
  namespace WebService.Controllers
  {
    [Route("api/linkpost")]
    public class LinkPostController : Controller
    {
        private readonly IDataService _dataService;
        public LinkPostController(IDataService dataService) {
            this._dataService = dataService;
        }
        // GET api/linkpost
        [HttpGet]
        public IEnumerable<LinkPost> Get(int page = 0, int pageSize = 50)
        {
            return _dataService.GetLinkPostRepository().Get(page, pageSize, null);
        }

        // GET api/linkpost/5
        [HttpGet("{id}")]
        public LinkPost Get(int id)
        {
            return _dataService.GetLinkPostRepository().GetByID(id);
        }

        public int Count() {
            return _dataService.GetLinkPostRepository().Count();
        }

        
      }
}
