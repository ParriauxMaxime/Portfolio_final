using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;

namespace WebService.Controllers
{
    [Route("api/user")]
    public class UserController : GenericReadableController<User>
    {
        public UserController(IDataService dataService) : base(dataService, dataService.GetUserRepository())
        {
        }

        // GET api/[controller]/5
        [HttpGet("{id}.{format?}")]
        public override IActionResult GetByID(int id)
        {
            string url = this.ControllerContext.RouteData?.Values["controller"].ToString();
            User result = _repository.GetByID(id).Result;
            if (result == null)
            {
                return NotFound(new Error(_repository.GetType().ToString(), id));
            }
            return Ok(new Encapsulation { Url = createUrl(result.Id), Data = result });
        }
    }
}
