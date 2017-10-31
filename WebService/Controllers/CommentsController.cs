using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/comments")]
    public class CommentsController : Controller
    {
        private readonly IDataService _dataService;

        public CommentsController(IDataService dataService) {
            this._dataService = dataService;
        }
        // GET api/Comments
        [HttpGet]
        public IEnumerable<Comment> Get(int page = 0, int pageSize = 50)
        {
            return _dataService.GetCommentRepository().Get(page, pageSize, orderBy: x => x.OrderBy(p => p.Id));
        }

        // GET api/Comment/5
        [HttpGet("{id}")]
        public Comment Get(int id)
        {
            return _dataService.GetCommentRepository().GetByID(id);
        }

        public int Count() {
            return _dataService.GetCommentRepository().Count();
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
