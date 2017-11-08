using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/linkpost")]
    public class LinkPostController : GenericReadableController<LinkPost>
    {
        public LinkPostController(IDataService dataService) : base(dataService, dataService.GetLinkPostRepository())
        {
        }
    }
}
