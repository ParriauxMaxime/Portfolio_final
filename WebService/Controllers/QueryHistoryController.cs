using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;

namespace WebService.Controllers
{
    [Route("api/queryhistory")]
    public class QueryHistoryController : GenericCRUDController<QueryHistory>
    {
        public QueryHistoryController(IDataService dataService) : base(dataService, dataService.GetQueryHistoryRepository())
        {
        }

          // GET api/[controller]
        [HttpGet]
        public override IActionResult Get(int page = 0, int pageSize = 50, int order = 0)
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
            Task<List<QueryHistory>> data = _repository.Get(page, pageSize, e => e.OrderBy(f => f.CreationDate)) as Task<List<QueryHistory>>;
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

        [HttpGet("searchQueryHistoryForAccount")]
        public IActionResult SearchQueryHistoryForAccount(string user = "", int limitNumber = 10)
        {
            return Ok(_dataService.GetProcedures().SearchQueryHistoryForAccount(user, limitNumber));
        }
    }
}
