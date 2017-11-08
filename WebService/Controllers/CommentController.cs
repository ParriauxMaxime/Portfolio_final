using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/comment")]
    public class CommentController : GenericReadableController<Comment>
    {
        public CommentController(IDataService dataService) : base(dataService, dataService.GetCommentRepository())
        {
        }
    }
}
