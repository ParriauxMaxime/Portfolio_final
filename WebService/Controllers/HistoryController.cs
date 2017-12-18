

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;

namespace WebService.Controllers
{
    [Route("api/history")]
    public class HistoryController : GenericCRUDController<History>
    {
        public HistoryController(IDataService dataService) : base(dataService, dataService.GetHistoryRepository())
        {
        }

        [HttpGet("searchHistoryForAccount")]
        public IActionResult SearchHistoryForAccount(string user = "", int limitNumber = 10)
        {
            return Ok(_dataService.GetProcedures().SearchHistoryForAccount(user, limitNumber));
        }

        [HttpGet("findPost")]
        public IActionResult findPost(int postId)
        {
            try {
                var result = _dataService.GetHistoryRepository().findPost(postId).Result;
                return Ok(result);
            }
            catch (Exception) {
                return NoContent();
            }
        }

        [HttpDelete("byPost/{id}")]
        public IActionResult byPost(int id)
        {
            try {
                var result = _dataService.GetHistoryRepository().deleteByPostId(id);
                return Ok(result);
            }
            catch (Exception) {
                return NoContent();
            }
        }
    }
}
