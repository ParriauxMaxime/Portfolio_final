using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;

namespace WebService.Controllers
{
    [Route("api/post")]
    public class PostController : GenericReadableController<Post>
    {
        public PostController(IDataService dataService) : base(dataService, dataService.GetPostRepository()) {
        }

        [HttpGet("searchInPosts")]
        public IActionResult SearchInPosts(string query = "", int questionOnly = 0, int numberLimit = 10)
        {
            return Ok(_dataService.SearchInPosts(query, questionOnly, numberLimit));
        }
        
        [HttpGet("getPostsByUser")]
        public IActionResult GetPostsByUser(string user = "")
        {
            return Ok(_dataService.GetPostsByUser(user));
        }
        
        [HttpGet("getPostsByTag")]
        public IActionResult GetPostsByTag(string tag = "")
        {
            return Ok(_dataService.GetPostsByTag(tag));
        }
    /* 
        [HttpGet("questions")]
        public IActionResult questions(int page = 0, int pageSize = 50)
        {
            List<Post> data = _dataService.getQuestions((uint)page, (uint)(pageSize)) as List<Post>;
            const int count = 0;
            const string appendixURL = "questions";
            if (pageSize > 200 || pageSize <= 0)
            {
                pageSize = 50;
            }
            if (page > count / pageSize)
            {
                page = count / pageSize;
            }
            else if (page < 0)
            {
                page = 0;
            }
            List<Encapsulation> tmp = new List<Encapsulation>(); 
            data.ForEach(e => {
                tmp.Add(new Encapsulation { Url = createUrl(e.Id), Data = e });
            });
            var result = new ListEncapsulation
            {
                Total = count,
                Pages = count / pageSize,
                Page = page,
                Prev = createUrl(null, page - 1, pageSize, appendixURL),
                Next = createUrl(null, page + 1, pageSize, appendixURL),
                Url = createUrl(null, page, pageSize, appendixURL),
                Data = tmp
            };
            return Ok(result);
        }
        */
    }
}
