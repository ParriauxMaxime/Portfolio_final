using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;

namespace WebService.Controllers
{
    [Route("api/question")]
    public class QuestionController : GenericReadableController<Post>
    {
        public QuestionController(IDataService dataService) : base(dataService, dataService.GetQuestionRepository())
        {
        }
        [HttpGet("random")]
        public IActionResult getRandom()
        {
            int count = _dataService.GetQuestionRepository().Count();
            Random md = new Random();
            int rand = md.Next(0, count);
            Post randomPost = _dataService.GetQuestionRepository().Get(0, count).ToList()[rand];
            return Ok(randomPost);
        }
    }
}
