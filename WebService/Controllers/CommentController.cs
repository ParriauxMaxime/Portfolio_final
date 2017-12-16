using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using DataAccessLayer.Repository;
using Models;

namespace WebService.Controllers
{
    [Route("api/comment")]
    public class CommentController : GenericReadableController<Comment>
    {
        protected new readonly CommentRepository _repository;
        public CommentController(IDataService dataService) : base(dataService, dataService.GetCommentRepository())
        {
            this._repository = dataService.GetCommentRepository();
        }

        [HttpGet("byPost/{id}")]
        public IActionResult GetByPost(int id)
        {
            string url = this.ControllerContext.RouteData?.Values["controller"].ToString();
            IEnumerable<Comment> result = _repository.getByPosts(id).Result;
            return Ok(result);
        }
    }
}
