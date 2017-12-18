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
        public IActionResult SearchInPosts(string query = "", int page = 0, int pageSize = 50)
        {
            var data = _dataService.GetProcedures().SearchInPosts(query);
            
            if (pageSize > 200 || pageSize <= 0)
            {
                pageSize = 50;
            }
            if (page > data.Count / pageSize)
            {
                page = data.Count / pageSize;
            }
            else if (page < 0)
            {
                page = 0;
            }

            List<Encapsulation> tmp = data
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(e => new Encapsulation { Url = "", Data = new Post {Id = e} })
                .ToList();
            
            var result = new ListEncapsulation
            {
                Total = data.Count,
                Pages = data.Count / pageSize,
                Page = page,
                Prev = makeURL(null, page - 1, pageSize, $"searchInPosts/{query}", data.Count),
                Next = makeURL(null, page + 1, pageSize, $"searchInPosts/{query}", data.Count),
                Url = makeURL(null, page, pageSize, $"searchInPosts/{query}", data.Count),
                Data = tmp
            };
            return Ok(result);
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

        private string makeURL(int? id, int page, int pageSize, string appendix, int count) {
            var host = "";
            if (Request == null) // It's null while Unit Testing...
            {
                //WTF
                host = "localhost:5001";
            }
            else
            {
                host = Request.Host.ToUriComponent();
            }
            var controller = ControllerContext.RouteData?.Values["controller"].ToString().ToLower();
            var url = "http://" + host + "/api/" + controller + (appendix != "" ? "/" : "") + appendix;
            if (id == null)
            {
                if (page < 0)
                {
                    return "";
                }
                else if (page > (count / pageSize))
                {
                    return "";
                }
                else
                    return url + "?page=" + page + "&pageSize=" + pageSize;
            }
            else
                return url + "/" + id + "?page=" + page + "&pageSize=" + pageSize;
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
                Prev = makeURL(null, page - 1, pageSize, "parentId/" + parentId.ToString(), count),
                Next = makeURL(null, page + 1, pageSize, "parentId/" + parentId.ToString(), count),
                Url = makeURL(null, page, pageSize, "parentId/" + parentId.ToString(), count),
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
