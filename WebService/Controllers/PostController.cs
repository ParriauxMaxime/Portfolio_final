using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/post")]
    public class PostController : Controller
    {
        private readonly IDataService _dataService;

        public PostController(IDataService dataService) {
            this._dataService = dataService;
        }
        // GET api/post
        [HttpGet]
        public IEnumerable<Post> Get(int page = 0, int pageSize = 50)
        {
            return _dataService.GetPostRepository().Get(page, pageSize, x => x.OrderBy(p => p.Id));
        }

        // GET api/post/5
        [HttpGet("{id}")]
        public Post Get(int id)
        {
            return _dataService.GetPostRepository().GetByID(id);
        }

        public int Count() {
            return _dataService.GetPostRepository().Count();
        }

        /**
        * Useless because the interface is GenericReadableRepository
        **/

        /*       
            // POST api/values
            [HttpPost]
            public void Post([FromBody]string value)
            {
            }

            // PUT api/values/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody]string value)
            {
            }

            // DELETE api/values/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
        }*/
    }
}
