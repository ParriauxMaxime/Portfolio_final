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
            return Ok(_dataService.GetProcedures().SearchInPosts(query, questionOnly, numberLimit));
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
        public IActionResult GetAnswersForPost(int parentId)
        {
            return Ok(_dataService.GetPostRepository().GetAnswersToPost(parentId));
        }
    }
}
