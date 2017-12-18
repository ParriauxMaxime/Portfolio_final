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
            Post randomPost = _dataService.GetQuestionRepository().GetRandom().Result;
            return Ok(randomPost);
        }

        [HttpGet]
          public override IActionResult Get(int page = 0, int pageSize = 50, int byScore = 0)
        {
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
            Task<List<Post>> data;
            if (byScore == 1) {
                data = _repository.Get(page, pageSize, (e) => e.OrderByDescending(f => f.score)) as Task<List<Post>>;
            }
            else {
                data = _repository.Get(page, pageSize, null) as Task<List<Post>>;
                
            }
            List<Encapsulation> tmp = new List<Encapsulation>();
            data.Result.ForEach(e =>
            {
                tmp.Add(new Encapsulation { Url = createUrl(e.Id), Data = e });
            });
            var result = new ListEncapsulation
            {
                Total = count,
                Pages = count / pageSize,
                Page = page,
                Prev = createUrl(null, page - 1, pageSize),
                Next = createUrl(null, page + 1, pageSize),
                Url = createUrl(null, page, pageSize),
                Data = tmp
            };
            return Ok(result);
        }
    }
}
