using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;

namespace WebService.Controllers
{
    [Route("api/tag")]
    public class TagController : GenericReadableController<Tag>
    {
        public TagController(IDataService dataService) : base(dataService, dataService.GetTagRepository())
        {
        }
        
        [HttpGet("getPostsByTag")]
        public IActionResult GetPostsByTag(string tag = "")
        {
            return Ok(_dataService.GetPostsByTag(tag));
        }
    }
}
