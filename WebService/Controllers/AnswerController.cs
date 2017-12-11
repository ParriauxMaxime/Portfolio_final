using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;

namespace WebService.Controllers
{
    [Route("api/answer")]
    public class AnswerController : GenericReadableController<Post>
    {
        public AnswerController(IDataService dataService) : base(dataService, dataService.GetAnswerRepository()) {
        }
    }
}
