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
        public IActionResult SearchInPosts(string query = "")
        {
            return Ok(_dataService.GetProcedures().SearchInPosts(query));
        }
        
        [HttpGet("getPostsByUser")]
        public IActionResult GetPostsByUser(string user = "")
        {
            return Ok(_dataService.GetProcedures().GetPostsByUser(user));
        }
        
        [HttpGet("getPostsByTag")]
        public IActionResult GetPostsByTag(string tag = "")
        {
            return Ok(_dataService.GetProcedures().GetPostsByTag(tag));
        }
        
        [HttpGet("getTagsForPost")]
        public IActionResult GetTagsForPost(int postId)
        {
            return Ok(_dataService.GetProcedures().getTagsForPost(postId));
        }

        [HttpGet("parentId/{parentId}")]
        public IActionResult GetAnswersForPost(int parentId, int page = 0, int pageSize = 50)
        {
            int count = this._dataService.GetPostRepository().GetNumberAnswerToPost(parentId).Result;
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
            List<int> data = _dataService.GetPostRepository().GetAnswersToPost(parentId, page, pageSize).Result;
            List<Encapsulation> tmp = new List<Encapsulation>();
            data.ForEach(e =>
            {
                tmp.Add(new Encapsulation { Url = "", Data = new Post {Id = e} });
            });
            var result = new ListEncapsulation
            {
                Total = count,
                Pages = count / pageSize,
                Page = page,
                Prev = createUrl(null, page - 1, pageSize, "parentId/" + parentId.ToString()),
                Next = createUrl(null, page + 1, pageSize, "parentId/" + parentId.ToString()),
                Url = createUrl(null, page, pageSize, "/parentId/" + parentId.ToString()),
                Data = tmp
            };
            return Ok(result);
        }
        
        [HttpGet("termNetwork")]
        public IActionResult TermNetwork(string query = "")
        {
            return Ok(_dataService.GetProcedures().TermNetwork(query));
        }
        
        [HttpGet("wordCloud")]
        public IActionResult WordCloud(string query = "")
        {
            return Ok(_dataService.GetProcedures().WeightedListTFIDF(query));
        }
    }
}
