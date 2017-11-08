using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/ltagspost")]
    public class LTagsPostController : GenericReadableController<LTagsPost>
    {
        public LTagsPostController(IDataService dataService) : base(dataService, dataService.GetLTagsPostRepository())
        {
        }
    }
}
