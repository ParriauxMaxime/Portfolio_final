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
        public QuestionController(IDataService dataService) : base(dataService, dataService.GetQuestionRepository()) {
        }
    }
}
