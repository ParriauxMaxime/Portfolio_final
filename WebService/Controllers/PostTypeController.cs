using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;

namespace WebService.Controllers
{
    [Route("api/posttype")]
    public class PostTypeController : GenericReadableController<PostType>
    {
        public PostTypeController(IDataService dataService) : base(dataService, dataService.GetPostTypeRepository())
        {
        }
    }
}
